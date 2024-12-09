using UnityEditor;
using UnityEngine;


[CustomEditor(typeof(ConfigData))]
public class ConfigDataEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        var configData = (ConfigData) target;

        GUILayout.Space(10);

        if (GUILayout.Button(nameof(configData.CreateDefaultConfig)))
        {
            configData.CreateDefaultConfig();
        }

        if (GUILayout.Button(nameof(configData.LoadConfig)))
        {
            configData.LoadConfig();
        }

        if (GUILayout.Button(nameof(configData.AmendConfigData)))
        {
            configData.AmendConfigData();
        }
    }
}