using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class MiniGameController : Singleton<MiniGameController>
{
    [SerializeField] Player m_player = null;
    [SerializeField] GameObject m_miniGamePanle = null;
    public AgentData m_agentData = null;

    float timeSpent = 10.0f;
    bool inGame = false;


    void Start()
    {
        CloseMiniGame();
    }

    void Update()
    {
        if (inGame)
        {
            UnityEngine.Debug.Log("Time remaining: "+ timeSpent +" seconds");//replace the logging with the bar
            if (timeSpent <= 0.1f)
            {
                UnityEngine.Debug.Log("Stopped");
            }
            else
            {
                timeSpent -= Time.deltaTime;
            }
        }
    }

    public void OpenMiniGame()
    {
        m_miniGamePanle.SetActive(true);
        timeSpent = 10 * (m_agentData.Awarness);
        inGame = true;
        m_player.inControl = true;
    }

    public void CloseMiniGame()
    {
        m_miniGamePanle.SetActive(false);
        inGame = false;
        m_player.inControl = false;

    }
}
