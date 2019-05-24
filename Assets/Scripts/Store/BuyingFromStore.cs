using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class BuyingFromStore : MonoBehaviour
{
    [SerializeField] PlayerData m_playerData = null;
    [SerializeField] TextMeshProUGUI costText = null;
    [SerializeField] TextMeshProUGUI upgradeText = null;
    [SerializeField] Image m_x = null;

    public void Bought(string costString, string upgradeString)
    {
        float cost = float.Parse(costString);
        float upgradeAmount = float.Parse(upgradeString);
        if (m_playerData.money > cost)
        {
            upgradeAmount = m_playerData.time * (upgradeAmount / 100);
            m_playerData.time = m_playerData.time + upgradeAmount;
            m_playerData.money = m_playerData.money - cost;
        }
    }

    public void OnStoreButtonClick()
    {
        string cost = costText.text;
        string upgrade = upgradeText.text;
        Bought(cost, upgrade);
        m_x.enabled = true;
    }


}
