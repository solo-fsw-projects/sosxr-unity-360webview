using System;
using System.Collections;
using TMPro;
using UnityEngine;
using Vuplex.WebView;


public class UpdateDebugTextField : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI m_infoText;
    [SerializeField] private WebViewConfigData m_configData;
    [SerializeField] private CanvasWebViewPrefab m_webViewPrefab;
    [SerializeField] private VideoPlayerManager m_videoPlayerManager;


    private void OnValidate()
    {
        if (m_infoText == null)
        {
            m_infoText = GetComponentInChildren<TextMeshProUGUI>();
        }

        if (m_webViewPrefab == null)
        {
            m_webViewPrefab = FindFirstObjectByType<CanvasWebViewPrefab>();
        }

        if (m_videoPlayerManager == null)
        {
            m_videoPlayerManager = FindFirstObjectByType<VideoPlayerManager>();
        }
    }


    private void OnEnable()
    {
        if (m_infoText == null)
        {
            Debug.LogError("No TextMeshProUGUI found");

            return;
        }

        StartCoroutine(UpdateTextCR());
    }


    private IEnumerator UpdateTextCR()
    {
        for (;;)
        {
            yield return new WaitForSeconds(m_configData.DebugUpdateInterval);

            if (m_videoPlayerManager == null)
            {
                m_infoText.text = "No VideoPlayerManager found";

                continue;
            }

            if (m_videoPlayerManager.Clips.Count == 0)
            {
                m_infoText.text = "No clips found" + "\n" + "Please add clips to:" + "\n" + m_videoPlayerManager.m_configData.VideoDirectory;

                continue;
            }

            if (m_videoPlayerManager.Trials == null)
            {
                m_infoText.text = "No Trials found";

                continue;
            }

            var modulus = -999;

            if (m_videoPlayerManager.Trials != null && m_configData.Order == WebViewConfigData.Ordering.Counterbalanced)
            {
                modulus = m_videoPlayerManager.Trials.Modulus;
            }

            try
            {
                m_infoText.text =
                    "Base URL: " + m_configData.BaseURL + "\n" +
                    "Query String URL: " + m_configData.QueryStringURL + "\n" +
                    "URL in WebView: " + m_webViewPrefab.WebView.Url + "\n" +
                    "Task Name: " + m_configData.TaskName + "\n" +
                    "Video Name: " + m_configData.VideoName + "\n" +
                    "PPN: " + m_configData.PPN + "\n" +
                    "Video Path: " + m_videoPlayerManager.VideoPlayer.url + "\n" +
                    "Playing: " + m_videoPlayerManager.CurrentClipTime + " sec" + "\n" +
                    "Clip Length: " + m_videoPlayerManager.CurrentClipDuration + " sec" + "\n" +
                    "Dimensions: " + m_videoPlayerManager.Dimensions + "\n" +
                    "Ordering: " + m_configData.Order + "\n" +
                    "PlayWay: " + m_configData.PlayWayEnum + "\n" +
                    "Conditions: " + m_videoPlayerManager.Trials.Conditions.Count + "\n" +
                    "Modulus: " + modulus;
            }
            catch (Exception e)
            {
                Debug.LogWarning("Error updating debug text: " + e.Message);
            }
        }
    }


    private void OnDisable()
    {
        StopAllCoroutines();
    }
}