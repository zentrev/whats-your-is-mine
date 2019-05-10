using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "AgentData")]
public class PlayerData : ScriptableObject
{
    List<float> itemsHeld = new List<float>();
    public float money = 0.0f;
    public float time = 0.0f;
}
