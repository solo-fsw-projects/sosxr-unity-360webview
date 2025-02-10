using System;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;


namespace SOSXR.ConfigData.Samples
{
    [CustomEditor(typeof(BuildQueryURL), true)]
    public class BuildQueryURLEditor : Editor
    {
        private readonly string[] _excludedNames = {"name", "hideFlags", "BaseURL", "QueryStringURL", "PPNString", "Extensions", "UpdateJsonOnValueChange", "ClipDirectory", "DebugUpdateInterval"};

        private string[] _validValueNames;
        private SerializedProperty _queryURLVariables;
        private SerializedProperty _configDataProp;
        private bool[] _selectedValues;

        private string _storedQueryStringURL;


        private void OnEnable()
        {
            _queryURLVariables = serializedObject.FindProperty("m_queryStringVariables");
            _configDataProp = serializedObject.FindProperty("m_configData");

            UpdateFieldsAndPropertiesList();
        }


        private void UpdateFieldsAndPropertiesList()
        {
            if (_configDataProp.objectReferenceValue == null)
            {
                _validValueNames = Array.Empty<string>();

                return;
            }

            var configType = _configDataProp.objectReferenceValue.GetType();

            var properties = configType
                             .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                             .Where(p => p.CanRead && !_excludedNames.Contains(p.Name))
                             .Select(p => p.Name);

            var fields = configType
                         .GetFields(BindingFlags.Public | BindingFlags.Instance)
                         .Where(f => !_excludedNames.Contains(f.Name))
                         .Select(f => f.Name);

            _validValueNames = properties.Concat(fields).ToArray();
        }


        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            DrawDefaultInspector();

            EditorGUILayout.Space();

            if (_validValueNames.Length > 0)
            {
                EditorGUILayout.LabelField("These variables will be included in the QueryURL", EditorStyles.boldLabel);

                DrawCheckBoxes();
            }
            else
            {
                EditorGUILayout.HelpBox("No valid fields or properties found.", MessageType.Warning);
            }

            GUILayout.Space(15);

            var webViewConfigData = _configDataProp.objectReferenceValue as WebViewConfigData;

            if (webViewConfigData != null)
            {
                EditorGUILayout.LabelField(webViewConfigData.QueryStringURL, EditorStyles.boldLabel);
            }

            serializedObject.ApplyModifiedProperties();
        }


        private void DrawCheckBoxes()
        {
            for (var i = 0; i < _validValueNames.Length; i++)
            {
                var isSelected = _queryURLVariables.arraySize > 0 &&
                                 Enumerable.Range(0, _queryURLVariables.arraySize)
                                           .Select(index => _queryURLVariables.GetArrayElementAtIndex(index).stringValue)
                                           .Contains(_validValueNames[i]);

                var newSelected = EditorGUILayout.ToggleLeft(_validValueNames[i], isSelected);

                if (newSelected == isSelected)
                {
                    continue;
                }

                if (newSelected)
                {
                    AddToQueryURLVariablesList(_validValueNames[i]);
                }
                else
                {
                    RemoveFromQueryURLVariablesList(_validValueNames[i]);
                }
            }
        }


        private void AddToQueryURLVariablesList(string value)
        {
            _queryURLVariables.arraySize++;
            _queryURLVariables.GetArrayElementAtIndex(_queryURLVariables.arraySize - 1).stringValue = value;
        }


        private void RemoveFromQueryURLVariablesList(string value)
        {
            for (var i = 0; i < _queryURLVariables.arraySize; i++)
            {
                if (_queryURLVariables.GetArrayElementAtIndex(i).stringValue == value)
                {
                    _queryURLVariables.DeleteArrayElementAtIndex(i);

                    break;
                }
            }
        }
    }
}