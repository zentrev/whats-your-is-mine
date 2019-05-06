using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "AgentData")]
public class AgentData : ScriptableObject
{
    [System.Flags]
    public enum eAgentAttributes
    {
        NONE        = (1 << 0),
        KEYHOLDER   = (1 << 1),
        GUARD       = (1 << 2),
    }

    public float Value = 10.0f;
    [Range(0.0f, 1.0f)]public float Awarness = .5f;
    public eAgentAttributes Attributes = eAgentAttributes.NONE;
}
