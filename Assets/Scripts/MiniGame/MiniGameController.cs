using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniGameController : Singleton<MiniGameController>
{
    [SerializeField] Player m_player = null;
    [SerializeField] GameObject m_miniGamePanle = null;
    public AgentData m_agentData = null;
    bool m_inGame = false;

    // debug tools
    public Image countdownImage;


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
        m_inGame = true;
        StartCoroutine(Countdown(m_player.PlayerData.time * (1 - m_agentData.Awarness)));
        m_player.inControl = false;
    }

    public void CloseMiniGame()
    {
        m_miniGamePanle.SetActive(false);
        m_inGame = false;
        m_player.inControl = true;

    }

    private IEnumerator Countdown(float time)
    {
        float duration = time;
                             
        float normalizedTime = 0;
        while (normalizedTime <= 1f && m_inGame)
        {
            countdownImage.fillAmount = 1-normalizedTime;
            normalizedTime += Time.deltaTime / duration;
            yield return null;
        }
        CloseMiniGame();
    }
}
