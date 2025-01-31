using SOSXR.EnhancedLogger;
using UnityEngine;


public class VideoClipHelper : MonoBehaviour
{
    [SerializeField] private WebViewConfigData m_configData;


    private void Awake()
    {
        if (m_configData == null)
        {
            this.Error("No WebViewConfigData found!");

            return;
        }

        if (m_configData.VideoLocation == WebViewConfigData.VideoLocationType.Arbor)
        {
            m_configData.ClipDirectory = FileHelpers.GetArborXRPath();
            this.Info("Video location set to: " + m_configData.ClipDirectory);
        }
        else if (m_configData.VideoLocation == WebViewConfigData.VideoLocationType.Local)
        {
            m_configData.ClipDirectory = FileHelpers.GetMoviesPath();
            this.Info("Video location set to: " + m_configData.ClipDirectory);
        }
        else
        {
            this.Info("No video location selected, please set one manually in the config file.");
        }
    }
}