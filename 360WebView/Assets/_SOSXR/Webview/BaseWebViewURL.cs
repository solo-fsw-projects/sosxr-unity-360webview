using mrstruijk.Events;
using SOSXR.EditorTools;
using SOSXR.EnhancedLogger;
using UnityEngine;
using Vuplex.WebView;


public abstract class BaseWebViewURL : MonoBehaviour
{
    [SerializeField] protected CanvasWebViewPrefab m_webViewPrefab;
    [SerializeField] private bool m_startAutomatically = true;

    [DisableEditing] public string BaseURL;


    private void Awake()
    {
        if (m_webViewPrefab == null)
        {
            m_webViewPrefab = FindFirstObjectByType<CanvasWebViewPrefab>();
        }
    }


    protected virtual void Start()
    {
        if (m_startAutomatically)
        {
            Invoke(nameof(LoadURL), 1);
        }
    }


    private void OnEnable()
    {
        EventsSystem.ConfigInformationChanged += LoadURL;
    }


    [ContextMenu(nameof(LoadURL))]
    public void LoadURL()
    {
        if (string.IsNullOrEmpty(BaseURL))
        {
            this.Error("Trying to load an empty URL!");

            return;
        }

        m_webViewPrefab.WebView.LoadUrl(BaseURL);
    }


    private void OnDisable()
    {
        EventsSystem.ConfigInformationChanged -= LoadURL;
    }
}