using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    // Player Stuff
    [SerializeField] float m_maxSpeed = 7;
    [SerializeField] float m_jumpTakeOffSpeed = 7;
    [SerializeField] float gravityModifier = 1f;
    public float minGroundNormalY = .65f;

    [SerializeField] PlayerData m_playerData;
    public PlayerData PlayerData { get => m_playerData; set => m_playerData = value; }
    public bool inControl = true;

    // Stolen Stuff
    protected Vector2 targetVelocity;
    protected bool grounded;
    protected Vector2 groundNormal;
    protected Rigidbody2D rb2d;
    protected Vector2 velocity;
    protected ContactFilter2D contactFilter;
    protected RaycastHit2D[] hitBuffer = new RaycastHit2D[16];
    protected List<RaycastHit2D> hitBufferList = new List<RaycastHit2D>(16);
    protected const float minMoveDistance = 0.001f;
    protected const float shellRadius = 0.01f;

    private SpriteRenderer spriteRenderer;
    private Animator animator;

    void Start()
    {
        contactFilter.useTriggers = false;
        contactFilter.SetLayerMask(Physics2D.GetLayerCollisionMask(gameObject.layer));
        contactFilter.useLayerMask = true;
    }

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>(); 
    }

    void Update()
    {
        targetVelocity = Vector2.zero;
        ComputeVelocity();
    }

    void FixedUpdate()
    {
        velocity += gravityModifier * Physics2D.gravity * Time.deltaTime;
        velocity.x = targetVelocity.x;

        grounded = false;

        Vector2 deltaPosition = velocity * Time.deltaTime;

        Vector2 moveAlongGround = new Vector2(groundNormal.y, -groundNormal.x);

        Vector2 move = moveAlongGround * deltaPosition.x;

        Movement(move, false);

        move = Vector2.up * deltaPosition.y;

        Movement(move, true);
    }

    void Movement(Vector2 move, bool yMovement)
    {
        float distance = move.magnitude;

        if (distance > minMoveDistance)
        {
            int count = rb2d.Cast(move, contactFilter, hitBuffer, distance + shellRadius);
            hitBufferList.Clear();
            for (int i = 0; i < count; i++)
            {
                hitBufferList.Add(hitBuffer[i]);
            }

            for (int i = 0; i < hitBufferList.Count; i++)
            {
                Vector2 currentNormal = hitBufferList[i].normal;
                if (currentNormal.y > minGroundNormalY)
                {
                    grounded = true;
                    if (yMovement)
                    {
                        groundNormal = currentNormal;
                        currentNormal.x = 0;
                    }
                }

                float projection = Vector2.Dot(velocity, currentNormal);
                if (projection < 0)
                {
                    velocity = velocity - projection * currentNormal;
                }

                float modifiedDistance = hitBufferList[i].distance - shellRadius;
                distance = modifiedDistance < distance ? modifiedDistance : distance;
            }
        }

        rb2d.position = rb2d.position + move.normalized * distance;
    }

    protected void ComputeVelocity()
    {
        Vector2 move = Vector2.zero;

        if (inControl)
        {
            move.x = Input.GetAxis("Horizontal");
            Debug.Log(move.x);

            if (Input.GetButtonDown("Jump") && grounded)
            {
                velocity.y = m_jumpTakeOffSpeed;
            }

            else if (Input.GetButtonUp("Jump"))
            {
                if (velocity.y > 0)
                {
                    velocity.y = velocity.y * 0.5f;
                }
            }
        }

        //animator.SetBool("grounded", grounded);
        //animator.SetFloat("velocityX", velocity.x / maxSpeed);

        targetVelocity = move * m_maxSpeed;
    }

    private void OnTriggerStay(Collider other)
    {
        if(Input.GetKey(KeyCode.LeftShift) && other.tag == "Agent" && inControl)
        {
            AgentData agentData = other.gameObject.GetComponent<AgentBehavior>().AgentData;
            Debug.Log("Steal");
            PlayerData.money += agentData.Value;
            Debug.Log("$" + PlayerData.money);
            PlayerData.itemsStolen.AddRange(agentData.Items);
            MiniGameController.Instance.m_agentData = agentData;
            MiniGameController.Instance.OpenMiniGame();
        }
    }
}