using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] [Range(1.0f, 50.0f)] float m_speed = 1.0f;
    [SerializeField] [Range(1.0f, 360.0f)] float m_rotateSpeed = 1.0f;
    public float money = 0.0f;
    public List<GameObject> stolenItems = new List<GameObject>();
    Rigidbody m_rb = null;
    Vector3 m_velocity = Vector3.zero;
    bool collided = false;
    Collider collider = null;
    void Start()
    {
        m_rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        m_velocity.x = Input.GetAxis("Horizontal") * m_speed;
        m_rb.velocity = m_velocity;

        if (Input.GetAxis("Interact")>0 && collided)
        {
            Debug.Log("Steal");
            //money += collider.gameObject.GetComponent<AgentData>().Value;
            //stolenItems.AddRange(collider.gameObject.GetComponent<AgentData>().Items);
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
}
