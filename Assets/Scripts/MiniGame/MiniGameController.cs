using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGameController : Singleton<MiniGameController>
{
    [SerializeField] Player m_player = null;
    AgentData m_agentData = null;

    [SerializeField] GameObject m_miniGamePanle = null;

    void Start()
    {
        CloseMiniGame();
    }

    void Update()
    {
        
    }

    public void OpenMiniGame()
    {
        m_miniGamePanle.SetActive(true);
    }

    public void CloseMiniGame()
    {
        m_miniGamePanle.SetActive(false);
    }
}
