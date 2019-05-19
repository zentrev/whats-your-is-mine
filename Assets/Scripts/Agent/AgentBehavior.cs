using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentBehavior : MonoBehaviour
{
    [SerializeField] AgentData m_agentData;

    private bool wondering = true;


    public AgentData AgentData { get => m_agentData; set => m_agentData = value; }

    void Start()
    {
        
    }

    void Update()
    {

    }
}
