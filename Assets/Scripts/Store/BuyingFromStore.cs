using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BuyingFromStore : MonoBehaviour
{
    [SerializeField] PlayerData m_playerData = null;

    public void Bought(string costString, string upgradeString)
    {
        float cost = float.Parse(costString);
        float upgradeAmount = float.Parse(upgradeString);
        if(m_playerData.money > cost)
        {
            m_playerData.time = m_playerData.time + upgradeAmount;
        }
    }

    public void OnStoreButtonClick(TextMeshProUGUI cost, TextMeshProUGUI upgradeAmount)
    {
        Bought(cost.ToString(), upgradeAmount.ToString());
    }


}
