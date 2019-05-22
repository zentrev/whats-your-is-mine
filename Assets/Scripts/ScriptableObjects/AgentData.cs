using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(fileName = "Data", menuName = "AgentData")]
public class AgentData : ScriptableObject
{
    [System.Flags]
    public enum eAgentAttributes
    {
        NONE        = (1 << 0),
        KEYHOLDER   = (1 << 1),
        GUARD       = (1 << 2),
        DECOY       = (1 << 3),
        CAPTIN      = (1 << 4),
    }

    public int Value = 10;
    public float Awarness = .5f;
    public eAgentAttributes Attributes;
    public List<GameObject> Items;
}
