using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] [Range(1.0f, 50.0f)] float m_speed = 1.0f;
    [SerializeField] [Range(1.0f, 360.0f)] float m_rotateSpeed = 1.0f;
    [SerializeField] [Range(0.0f, 5.0f)] float m_groundDistance = 0.2f;
    [SerializeField] [Range(0.0f, 50.0f)] float m_jumpForce = 1.0f;
    [SerializeField] LayerMask m_layerMask;



    public float money = 0.0f;
    public List<GameObject> stolenItems = new List<GameObject>();

    Rigidbody m_rb = null;
    Vector3 m_velocity = Vector3.zero;
    bool collided = false;
    bool m_onGround = true;
    Vector3 m_groundNormal;
    float m_jumpTimer = 0.0f;
    Collider collider = null;
    void Start()
    {
        m_rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        RaycastHit hit;
        m_onGround = OnGround(m_groundDistance, out hit);
        if (m_onGround)
        {
            m_velocity.y = 0.0f;
        }
        m_velocity.x = Input.GetAxis("Horizontal") * m_speed;
        m_rb.velocity = m_velocity;
        m_jumpTimer -= Time.deltaTime;
        if (m_onGround && m_jumpTimer <= 0.0f)
        {
            if (Input.GetButtonDown("Jump") && m_onGround)
            {
                m_velocity = m_velocity + Vector3.up * m_jumpForce;
                m_rb.velocity = m_velocity;
                m_jumpTimer = 0.2f;
            }
        }
        else
        {
            m_velocity.y -=  .50f;
        }
        

        if (Input.GetKeyDown(KeyCode.LeftShift) && collided)
        {
            Debug.Log("Steal");
            money += collider.gameObject.GetComponent<AgentBehavior>().AgentData.Value;
            Debug.Log("$"+money);
            stolenItems.AddRange(collider.gameObject.GetComponent<AgentBehavior>().AgentData.Items);
        }
       
    }

        private void OnTriggerEnter(Collider other)
    {
        collider = other;
        collided = true;
    }

    private void OnTriggerExit(Collider other)
    {
        collider = null;
        collided = false;
    }

    bool OnGround(float distance, out RaycastHit hit)
    {
        Debug.DrawRay(transform.position + (Vector3.up * 0.1f), Vector3.down * distance, Color.red);

        return (Physics.Raycast(transform.position + (Vector3.up * 0.1f), Vector3.down, out hit, distance, m_layerMask));
    }
}
