using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;


namespace SOSXR.ConfigData.Samples
{
    /// <summary>
    ///     This demo is maybe a little specific to be in the ConfigData package, but it's a good example of how to use the
    ///     ConfigData in research tasks.
    /// </summary>
    public class BuildQueryURL : MonoBehaviour
    {
        [SerializeField] private WebViewConfigData m_configData;

        [SerializeField] [HideInInspector] private List<string> m_queryStringVariables;


        private void OnValidate()
        {
            if (m_configData == null)
            {
                Debug.LogError("ConfigData not assigned.");

                return;
            }

            BuildQuery();
        }


        private void OnEnable()
        {
            foreach (var query in m_queryStringVariables)
            {
                m_configData.Subscribe(query, _ => BuildQuery());
            }
        }


        [ContextMenu(nameof(BuildQuery))]
        public void BuildQuery()
        {
            m_configData.QueryStringURL = BuildQueryStringURL(m_configData, m_configData.BaseURL, m_queryStringVariables.ToArray());

            Debug.LogFormat(this, "Built query URL");
        }


        /// <summary>
        ///     You can pass any number of values to this method, and it will build a query string URL using the values that match
        ///     those fields
        /// </summary>
        /// <param name="baseURL"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        public static string BuildQueryStringURL(object source, string baseURL, params string[] paramNames)
        {
            if (source == null || string.IsNullOrWhiteSpace(baseURL))
            {
                Debug.LogError("Source object or baseURL are null or empty.");

                return string.Empty;
            }

            if (paramNames.Length == 0)
            {
                Debug.LogWarning("ParamNames are null or empty.");

                return baseURL;
            }

            var queryParams = new List<string>();
            var fields = source.GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public);
            var properties = source.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (var paramName in paramNames)
            {
                var field = fields.FirstOrDefault(f => NormalizeName(f.Name) == paramName);
                var property = properties.FirstOrDefault(p => p.Name == paramName);

                var value = field?.GetValue(source) ?? property?.GetValue(source);

                if (value != null)
                {
                    queryParams.Add($"{paramName}={Uri.EscapeDataString(value.ToString())}");
                }
                else
                {
                    Debug.LogWarning($"Query parameter '{paramName}' not found in fields or properties of {source.GetType().Name}.");
                }
            }

            return queryParams.Count > 0 ? $"{baseURL}?{string.Join("&", queryParams)}" : baseURL;
        }


        private static string NormalizeName(string name)
        {
            return name.TrimStart('m', '_'); // Normalizing Unity-style private field names
        }


        private void OnDisable()
        {
            foreach (var query in m_queryStringVariables)
            {
                m_configData.Unsubscribe(query, _ => BuildQuery());
            }
        }
    }
}