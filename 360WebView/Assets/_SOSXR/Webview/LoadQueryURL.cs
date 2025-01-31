using SOSXR.ConfigData;
using SOSXR.EnhancedLogger;
using UnityEngine;
using Vuplex.WebView;


public class LoadQueryURL : MonoBehaviour
{
    [SerializeField] protected CanvasWebViewPrefab m_webViewPrefab;
    [SerializeField] private bool m_startAutomatically = false;
    [SerializeField] private WebViewConfigData m_configData;


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


    private void Start()
    {
        if (!m_startAutomatically)
        {
            return;
        }

        Invoke(nameof(LoadURL), 1);
    }


    private void OnEnable()
    {
        BuildQueryURL.OnQueryURLChanged += LoadURL;
    }


    [ContextMenu(nameof(LoadURL))]
    public void LoadURL()
    {
        if (m_webViewPrefab == null)
        {
            this.Error("No WebViewPrefab found!");

            return;
        }

        if (m_webViewPrefab.WebView == null)
        {
            this.Error("No WebView found!");

            return;
        }

        if (m_configData == null)
        {
            this.Error("Cannot continue since we don't have a reference to the relevant ConfigData");

            return;
        }

        if (string.IsNullOrEmpty(m_configData.QueryStringURL))
        {
            this.Error("Trying to load an empty URL!");

            return;
        }

        m_webViewPrefab.WebView.LoadUrl(m_configData.QueryStringURL);

        this.Success("Loaded URL", m_configData.QueryStringURL, "into our webview");
    }


    private void OnDisable()
    {
        BuildQueryURL.OnQueryURLChanged -= LoadURL;
    }
}