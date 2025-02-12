using System.Collections;
using SOSXR.EnhancedLogger;
using UnityEngine;
using Vuplex.WebView;


public class LoadQueryURL : MonoBehaviour
{
    [SerializeField] protected CanvasWebViewPrefab m_webViewPrefab;
    [SerializeField] private WebViewConfigData m_configData;
    [SerializeField] private bool m_startAutomatically = false;

    private Coroutine _coroutine;


    private void OnValidate()
    {
        if (m_webViewPrefab == null)
        {
            m_webViewPrefab = FindFirstObjectByType<CanvasWebViewPrefab>();
        }

        if (m_configData == null)
        {
            this.Warning("Missing ConfigData");
        }
    }


    private void OnEnable()
    {
        m_configData.Subscribe(nameof(m_configData.QueryStringURL), _ => LoadURL());

        if (m_startAutomatically)
        {
            LoadURL();
        }
    }


    [ContextMenu(nameof(LoadURL))]
    private void LoadURL()
    {
        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
        }

        _coroutine = StartCoroutine(LoadUrlCR());
    }


    private IEnumerator LoadUrlCR()
    {
        var delay = 0.1f;

        while (m_webViewPrefab.WebView == null)
        {
            this.Warning("WebView is not ready yet. Waiting for", delay, "seconds before checking again");

            yield return new WaitForSeconds(delay);
        }

        if (m_configData == null || string.IsNullOrEmpty(m_configData.QueryStringURL))
        {
            this.Error("Cannot load URL since ConfigData is null or QueryStringURL is empty");

            yield break;
        }

        m_webViewPrefab.WebView.LoadUrl(m_configData.QueryStringURL);

        this.Success("Loaded URL", m_configData.QueryStringURL, "into our lovely WebView");
    }


    private void OnDisable()
    {
        StopAllCoroutines();

        if (m_configData != null)
        {
            m_configData.Unsubscribe(nameof(m_configData.QueryStringURL), _ => LoadURL());
        }
    }
}