using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UpdatePlayerGUIItems : MonoBehaviour
{
    [SerializeField] PlayerData m_playerData = null;
    [SerializeField] TextMeshProUGUI m_moneyText = null;

    private void Update()
    {
        m_moneyText.text = m_playerData.money.ToString("D4");
    }
}
