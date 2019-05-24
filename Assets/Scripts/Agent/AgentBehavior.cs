using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentBehavior : MonoBehaviour
{
    [SerializeField] AgentData m_agentData;

    // Player Stuff
    [Header("Movement")]
    [SerializeField] float m_maxSpeed = 1f;
    [SerializeField] float gravityModifier = 1f;
    [HideInInspector] public float minGroundNormalY = .65f;

    [SerializeField] Transform m_rightCheck = null;
    [SerializeField] Transform m_leftCheck = null;
    [SerializeField] LayerMask m_checkLayer = 0;
    bool m_movingRight = true;

    const float checkRaidus = .2f;

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

    public AgentData AgentData { get => m_agentData; set => m_agentData = value; }

    void Start()
    {
        contactFilter.useTriggers = false;
        contactFilter.SetLayerMask(Physics2D.GetLayerCollisionMask(gameObject.layer));
        contactFilter.useLayerMask = true;
        m_agentData = Instantiate(m_agentData);
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
                PlatformEffector2D platform = hitBuffer[i].collider.GetComponent<PlatformEffector2D>();
                if (!platform || (hitBuffer[i].normal == Vector2.up && velocity.y < 0 && yMovement)) //only when going down
                {
                    hitBufferList.Add(hitBuffer[i]); // get the colliding objects
                }
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
            m_movingRight = (m_movingRight) ? (Physics2D.OverlapCircleAll(m_rightCheck.position, checkRaidus, m_checkLayer).Length > 0) ? false : true : (Physics2D.OverlapCircleAll(m_leftCheck.position, checkRaidus, m_checkLayer).Length > 0) ? true : false;


            move.x = (m_movingRight) ? 1 : -1;
        }

        //animator.SetBool("grounded", grounded);
        //animator.SetFloat("velocityX", velocity.x / maxSpeed);

        targetVelocity = move * m_maxSpeed;
    }
}
