using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(AgentData))]
public class DemoInspector : Editor
{
    public override void OnInspectorGUI()
    {
        AgentData agent = (AgentData)target;

        agent.Value = EditorGUILayout.IntField("Value", agent.Value);
        agent.Awarness = EditorGUILayout.Slider("Awarness", agent.Awarness, 0.0f, 1.0f);
        agent.Attributes = (AgentData.eAgentAttributes) EditorGUILayout.EnumFlagsField("Attributes", agent.Attributes);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("Items"), true);
    }
}