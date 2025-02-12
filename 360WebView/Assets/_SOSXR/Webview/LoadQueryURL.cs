using SOSXR.EnhancedLogger;
using UnityEngine;
using Vuplex.WebView;


public class LoadQueryURL : MonoBehaviour
{
    [SerializeField] protected CanvasWebViewPrefab m_webViewPrefab;
    [SerializeField] private WebViewConfigData m_configData;
    [SerializeField] private bool m_startAutomatically = false;


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
        m_configData.Subscribe(nameof(m_configData.QueryStringURL), o => LoadURL());

        if (m_startAutomatically)
        {
            Invoke(nameof(LoadURL), 1);
        }
    }


    [ContextMenu(nameof(LoadURL))]
    public void LoadURL()
    {
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
        m_configData.Unsubscribe(nameof(m_configData.QueryStringURL), o => LoadURL());
    }
}