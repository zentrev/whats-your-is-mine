using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentBehavior : MonoBehaviour
{
    [SerializeField] AgentData m_agentData;

    void Start()
    {
        
    }

    public void Init(AgentData agentData)
    {
        m_agentData = agentData;
        // do starting things
    }

    void Update()
    {
        // AI and behavior things

    }
}
