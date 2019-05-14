using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "PlayerData")]
public class PlayerData : ScriptableObject
{
    public float money = 0.0f;
    public float time = 0.0f;
    public List<float> itemsHeld = new List<float>();
}
