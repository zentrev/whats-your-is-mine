using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BuyingFromStore : MonoBehaviour
{
    [SerializeField] PlayerData m_playerData = null;
    [SerializeField] TextMeshProUGUI costText = null;
    [SerializeField] TextMeshProUGUI upgradeText = null;

    public void Bought(string costString, string upgradeString)
    {
        float cost = float.Parse(costString);
        float upgradeAmount = float.Parse(upgradeString);
        if(m_playerData.money > cost)
        {
            m_playerData.time = m_playerData.time + upgradeAmount;
            m_playerData.money = m_playerData.money - cost;
        }
    }

    public void OnStoreButtonClick()
    {
        Bought(costText.ToString(), upgradeText.ToString());
    }


}
