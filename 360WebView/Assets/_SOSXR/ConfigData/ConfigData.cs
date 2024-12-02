using System;
using System.IO;
using SOSXR.EnhancedLogger;
using UnityEngine;


[CreateAssetMenu(fileName = "ConfigData", menuName = "SOSXR/ConfigData")]
public class ConfigData : DefaultConfigData
{
    [Header("Default Values")]
    [SerializeField] private DefaultConfigData m_defaultConfigData;

    private readonly string _configFileName = "config.json";


    /// <summary>
    ///     Gets the path to the config file in the persistent data path.
    /// </summary>
    private string ConfigPath => Path.Combine(Application.persistentDataPath, _configFileName);


    /// <summary>
    ///     Creates a new default config and writes it to the persistent data path.
    /// </summary>
    [ContextMenu(nameof(CreateDefaultConfig))]
    public void CreateDefaultConfig()
    {
        try
        {
            var jsonData = JsonUtility.ToJson(m_defaultConfigData, true);
            File.WriteAllText(ConfigPath, jsonData);

            this.Debug("Created new default config file at: " + ConfigPath);
        }
        catch (Exception e)
        {
            this.Error("Failed to create default config file: " + e.Message);
        }
    }


    /// <summary>
    ///     Loads the JSON data from the config file in the persistent data path.
    /// </summary>
    [ContextMenu(nameof(LoadConfig))]
    public void LoadConfig()
    {
        if (!File.Exists(ConfigPath))
        {
            this.Warning("Config file not found at: " + ConfigPath);
            CreateDefaultConfig();
        }

        try
        {
            var jsonData = File.ReadAllText(ConfigPath);
            JsonUtility.FromJsonOverwrite(jsonData, this);
            this.Debug("Config loaded successfully", jsonData);
        }
        catch (UnauthorizedAccessException e)
        {
            this.Error("UnauthorizedAccessException: " + e.Message);
        }
        catch (Exception e)
        {
            this.Error("Exception: " + e.Message);
        }
    }


    [ContextMenu(nameof(AmendConfigData))]
    public void AmendConfigData()
    {
        try
        {
            var jsonData = JsonUtility.ToJson(this, true);
            File.WriteAllText(ConfigPath, jsonData);

            this.Debug("Amended config file at: " + ConfigPath);

            RemoveDefaultConfigData();
        }
        catch (Exception e)
        {
            this.Error("Failed to amend config file: " + e.Message);
        }
    }


    public void RemoveDefaultConfigData()
    {
        try
        {
            // Load JSON from file
            var jsonData = File.ReadAllText(ConfigPath);

            // Find the m_defaultConfigData field
            var fieldName = "\"m_defaultConfigData\"";
            var startIndex = jsonData.IndexOf(fieldName, StringComparison.Ordinal);

            if (startIndex != -1)
            {
                // Find the end of the field (assumes no nested objects)
                var fieldEndIndex = jsonData.IndexOf('}', startIndex) + 1;

                // Backtrack to find if the previous non-whitespace character is a comma
                var commaIndex = startIndex - 1;

                while (commaIndex >= 0 && char.IsWhiteSpace(jsonData[commaIndex]))
                {
                    commaIndex--;
                }

                // If it's a comma, include it in the removal
                if (commaIndex >= 0 && jsonData[commaIndex] == ',')
                {
                    startIndex = commaIndex;
                }

                // Remove the field from the JSON string
                jsonData = jsonData.Remove(startIndex, fieldEndIndex - startIndex);

                // Write the modified JSON back to the file
                File.WriteAllText(ConfigPath, jsonData);

                Debug.Log("Removed 'm_defaultConfigData' from config file.");
            }
            else
            {
                Debug.LogWarning("'m_defaultConfigData' field not found.");
            }
        }
        catch (IOException e)
        {
            Debug.LogError("IOException: " + e.Message);
        }
        catch (Exception e)
        {
            Debug.LogError("Exception: " + e.Message);
        }
    }
}