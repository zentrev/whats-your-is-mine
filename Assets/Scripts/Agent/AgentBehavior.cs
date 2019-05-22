using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentBehavior : MonoBehaviour
{
    [SerializeField] AgentData m_agentData;

    private bool wondering = true;
    Rigidbody2D m_rb;
    SpriteRenderer m_spriteRenderer;
    Animator m_animator;

    public AgentData AgentData { get => m_agentData; set => m_agentData = value; }

    void Start()
    {
        m_rb = GetComponent<Rigidbody2D>();
        m_spriteRenderer = GetComponent<SpriteRenderer>();
        m_animator = GetComponent<Animator>();
    }

    void Update()
    {
        bool walk = Mathf.Abs(m_rb.velocity.x) > 0.001f;
        m_animator.SetBool("Walk", walk);
        if (OnEdge())
        {
            m_spriteRenderer.flipX = m_spriteRenderer.flipX ? false : true;
        }
        if (!m_spriteRenderer.flipX)
        {
            m_rb.velocity = Vector2.right;            
        }
        else
        {
            m_rb.velocity = Vector2.left;
        }
    }

    private bool OnEdge()
    {
        bool onEdge = false;
        Vector2 direction = !m_spriteRenderer.flipX ? Vector2.right : Vector2.left;
        if (Physics2D.Raycast(transform.position, direction.normalized, 0.1f).distance>0.1f) onEdge = true;
        return onEdge;
    }
}
