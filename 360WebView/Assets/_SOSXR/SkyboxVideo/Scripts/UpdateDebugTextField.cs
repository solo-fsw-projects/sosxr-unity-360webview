using System.Collections;
using TMPro;
using UnityEngine;
using Vuplex.WebView;


public class UpdateDebugTextField : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI m_infoText;
    [SerializeField] private ConfigData m_configData;
    [SerializeField] private CanvasWebViewPrefab m_webViewPrefab;
    [SerializeField] private VideoPlayerManager m_videoPlayerManager;


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
            yield return new WaitForSeconds(1);

            if (m_videoPlayerManager.Clips.Count == 0)
            {
                m_infoText.text = "No clips found" + "\n" + "Please add clips to:" + "\n" + m_videoPlayerManager.m_configData.ClipDirectory;
            }
            else
            {
                var lastDigit = -999;

                if (m_videoPlayerManager.Trials != null)
                {
                    lastDigit = m_videoPlayerManager.Trials.LastDigitModulus;
                }

                m_infoText.text =
                    "URL: " + m_webViewPrefab.WebView.Url + "\n\n" +
                    "Video Path: " + m_videoPlayerManager.VideoPlayer.url + "\n" +
                    "Playing: " + m_videoPlayerManager.CurrentClipTime + " sec" + "\n" +
                    "Clip Length: " + m_videoPlayerManager.CurrentClipDuration + " sec" + "\n" +
                    "Dimensions: " + m_videoPlayerManager.Dimensions + "\n" +
                    "Ordering: " + m_configData.Order + "\n" +
                    "PlayWay: " + m_configData.PlayWay + "\n" +
                    "PPN: " + m_configData.PPN + "\n" +
                    "Last Digit Modulus: " + lastDigit;
            }
        }
    }


    private void OnDisable()
    {
        StopAllCoroutines();
    }
}