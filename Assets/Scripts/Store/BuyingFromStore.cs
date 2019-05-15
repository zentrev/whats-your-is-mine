using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BuyingFromStore : MonoBehaviour
{
    [SerializeField] PlayerData m_player = null;

    public void Bought(string costString, string upgradeString)
    {
        float cost = float.Parse(costString);
        float upgradeAmount = float.Parse(upgradeString);
        if(m_player.money > cost)
        {
            m_player.time = m_player.time + upgradeAmount;
        }
    }

    public void OnButtonClick(TextMeshProUGUI cost, TextMeshProUGUI thing)
    {

    }
}
