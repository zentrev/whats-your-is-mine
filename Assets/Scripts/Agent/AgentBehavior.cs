using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentBehavior : MonoBehaviour
{
    [SerializeField] AgentData m_agentData;

    public AgentData AgentData { get => m_agentData; set => m_agentData = value; }

    void Start()
    {
        
    }

    public void Init(AgentData agentData)
    {
        AgentData = Instantiate(agentData);
    }

    void Update()
    {
        // AI and behavior things

    }
}
