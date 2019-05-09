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
        KEYHOLDER   = (1 << 1),
        GUARD       = (1 << 2),
        DECOY       = (1 << 3),
        ROYALTY     = (1 << 4),
    }

    public GameObject AgentObject;    
    public float Value = 10.0f;
    public float Awarness = .5f;
    public eAgentAttributes Attributes;
    public List<GameObject> Items;
}
