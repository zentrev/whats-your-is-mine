using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentManager : Singleton<AgentManager>
{
    [System.Serializable]
    public struct LevelAgents
    {
        public AgentData agentData;
        public bool forceAgent;
        [Range(0.0f, 1.0f)] public float agentProbobiltiy;
        public bool singleton;
        [HideInInspector] public bool inLevel;
    }

    [System.Serializable]
    public struct AgentSpawner
    {
        public Transform transform;
        public int count;
    }

    [System.Serializable]
    public struct FixedAgents
    {
        public AgentSpawner agentSpawner;
        public AgentData agentData;
    }

    [SerializeField] GameObject m_agentBase = null;
    [SerializeField] List<FixedAgents> m_fixedAgents = new List<FixedAgents>();
    [SerializeField] List<LevelAgents> m_posibleAgents = new List<LevelAgents>();
    [SerializeField] List<AgentSpawner> m_agentSpawners = new List<AgentSpawner>();
    List<AgentBehavior> m_levelAgents = new List<AgentBehavior>();

    public void Start()
    {
        SpwanAgents();
    }

    public void SpwanAgents()
    {
        foreach(FixedAgents fixedAgent in m_fixedAgents)
        {
            GameObject agent = GameObject.Instantiate(m_agentBase);
            agent.transform.position = fixedAgent.agentSpawner.transform.position;
            if(!agent.GetComponent<AgentBehavior>()) agent.AddComponent<AgentBehavior>();
            agent.GetComponent<AgentBehavior>().Init(fixedAgent.agentData);
            m_levelAgents.Add(agent.GetComponent<AgentBehavior>());
        }

        float agentWheights = 0.0f;
        foreach(LevelAgents posibleAgent in m_posibleAgents)
        {
            LevelAgents pAgent = posibleAgent;

            if (pAgent.forceAgent)
            {
                int spwanerIndex = Random.Range(0, m_agentSpawners.Count);
                AgentSpawner spawn = m_agentSpawners[spwanerIndex];

                GameObject agent = GameObject.Instantiate(m_agentBase);
                agent.transform.position = spawn.transform.position;

                if (!agent.GetComponent<AgentBehavior>()) agent.AddComponent<AgentBehavior>();
                agent.GetComponent<AgentBehavior>().Init(pAgent.agentData);
                m_levelAgents.Add(agent.GetComponent<AgentBehavior>());
                pAgent.inLevel = true;

                spawn.count--;
                m_agentSpawners[spwanerIndex] = spawn;
            }
            agentWheights += posibleAgent.agentProbobiltiy;
        }

        for(int i = 0; i < m_agentSpawners.Count; i++)
        {
            AgentSpawner spawn = m_agentSpawners[i];
            while(spawn.count > 0)
            {
                float wheight = Random.Range(0.0f, agentWheights);
                for(int j = 0; j < m_posibleAgents.Count; j++)
                {
                    LevelAgents pAgent = m_posibleAgents[j];

                    if (!pAgent.singleton || (pAgent.singleton && !pAgent.inLevel))
                    {
                        wheight -= pAgent.agentProbobiltiy;
                        if (wheight <= 0)
                        {
                            GameObject agent = GameObject.Instantiate(m_agentBase);
                            agent.transform.position = spawn.transform.position;

                            if (!agent.GetComponent<AgentBehavior>()) agent.AddComponent<AgentBehavior>();
                            agent.GetComponent<AgentBehavior>().Init(pAgent.agentData);
                            m_levelAgents.Add(agent.GetComponent<AgentBehavior>());
                            pAgent.inLevel = true;
                            m_posibleAgents[j] = pAgent;
                            spawn.count--;
                            break;
                        }
                    }
                }
            }
            m_agentSpawners[i] = spawn;

        }
    }

    void Update()
    {

    }
}
