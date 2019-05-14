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

    Stopwatch stopWatch = new Stopwatch();
    float timeMultiplier = 10.0f;


    void Start()
    {
        CloseMiniGame();
    }

    void Update()
    {
        UnityEngine.Debug.Log(stopWatch.ElapsedMilliseconds + " millseconds");
        //time * (1-agentdata perception
        
        TimeSpan timeSpan = new TimeSpan(0, 0, 0, 0, (int)timeMultiplier);
        if (stopWatch.ElapsedMilliseconds == timeSpan.Milliseconds)
        {
            stopWatch.Stop();
            UnityEngine.Debug.Log("Stopped");
        }
    }

    public void OpenMiniGame()
    {
        m_miniGamePanle.SetActive(true);
        stopWatch.Start();
        timeMultiplier = 1 - m_agentData.Awarness;
        timeMultiplier += 10;
        timeMultiplier *= 1000;
    }

    public void CloseMiniGame()
    {
        m_miniGamePanle.SetActive(false);
    }
}
