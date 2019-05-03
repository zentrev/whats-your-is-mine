using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ChangeLevelButton))]
public class ChangeLevelButtonEditor : Editor
{
    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        serializedObject.FindProperty("m_nextLevel").intValue = EditorGUILayout.Popup("Level", serializedObject.FindProperty("m_nextLevel").intValue, ShowScenes(), EditorStyles.popup);
        serializedObject.ApplyModifiedProperties();
    }

    private string[] ShowScenes()
    {
        List<string> temp = new List<string>();
        foreach (UnityEditor.EditorBuildSettingsScene S in UnityEditor.EditorBuildSettings.scenes)
        {
            if (S.enabled)
            {
                string name = S.path.Substring(S.path.LastIndexOf('/') + 1);
                name = name.Substring(0, name.Length - 6);
                temp.Add(name);
            }
        }
        return temp.ToArray();
    }
}
