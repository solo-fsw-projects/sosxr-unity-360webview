using SOSXR.ConfigData;
using SOSXR.EditorTools;
using UnityEngine;


[CreateAssetMenu(fileName = "WebView Config Data", menuName = "SOSXR/Config Data/WebView Config Data")]
public class WebViewConfigData : BaseConfigData
{
    public enum Location
    {
        ArborXR,
        Movies,
        Documents,
        Other
    }


    public enum Ordering
    {
        InOrder,
        Random,
        Permutation,
        Counterbalanced
    }


    public enum PlayWay
    {
        All,
        One,
        Repeat
    }


    [SerializeField] private string m_baseURL = "https://youtu.be/xvFZjo5PgG0?si=F3cJFXtwofUAeA";
    [SerializeField] [DisableEditing] [TextArea] private string m_queryStringURL = "";
    [SerializeField] private int m_PPN = -1;
    [SerializeField] private string m_taskName = "TaskToDo";
    [SerializeField] private Ordering m_order = Ordering.Counterbalanced;
    [SerializeField] private PlayWay m_playWayEnum = PlayWay.All;
    [SerializeField] private string m_videoName = "VideoName";
    [SerializeField] private Location m_videoLocationType = Location.ArborXR;
    [SerializeField] private string m_videoDirectory = "/Users/Mine/Videos";
    [SerializeField] private string[] m_extensions = {".mp4"};
    [SerializeField] private bool m_showAffordances = false;
    [SerializeField] private bool m_showKeyboard = false;
    [SerializeField] private bool m_showDebug = false;
    [SerializeField] [Range(0, 30)] private int m_debugUpdateInterval = 1;


    public string BaseURL
    {
        get => m_baseURL;
        set => SetValue(ref m_baseURL, value, nameof(BaseURL));
    }

    public string QueryStringURL
    {
        get => m_queryStringURL;
        set => SetValue(ref m_queryStringURL, value, nameof(QueryStringURL));
    }

    public string TaskName
    {
        get => m_taskName;
        set => SetValue(ref m_taskName, value, nameof(TaskName));
    }

    public bool ShowDebug
    {
        get => m_showDebug;
        set => SetValue(ref m_showDebug, value, nameof(ShowDebug));
    }

    public int DebugUpdateInterval
    {
        get => m_debugUpdateInterval;
        set => SetValue(ref m_debugUpdateInterval, value, nameof(DebugUpdateInterval));
    }

    public bool ShowAffordances
    {
        get => m_showAffordances;
        set => SetValue(ref m_showAffordances, value, nameof(ShowAffordances));
    }

    public Location VideoLocationType
    {
        get => m_videoLocationType;
        set => SetValue(ref m_videoLocationType, value, nameof(VideoLocationType));
    }

    public string VideoDirectory
    {
        get => m_videoDirectory;
        set => SetValue(ref m_videoDirectory, value, nameof(VideoDirectory));
    }

    public string[] Extensions
    {
        get => m_extensions;
        set => SetValue(ref m_extensions, value, nameof(Extensions));
    }

    public Ordering Order
    {
        get => m_order;
        set => SetValue(ref m_order, value, nameof(Order));
    }

    public bool ShowKeyboard
    {
        get => m_showKeyboard;
        set => SetValue(ref m_showKeyboard, value, nameof(ShowKeyboard));
    }

    public PlayWay PlayWayEnum
    {
        get => m_playWayEnum;
        set => SetValue(ref m_playWayEnum, value, nameof(PlayWayEnum));
    }

    public int PPN
    {
        get => m_PPN;
        set => SetValue(ref m_PPN, value, nameof(PPN));
    }

    public string PPNString
    {
        get => m_PPN.ToString();
        set => PPN = int.TryParse(value, out var result) ? result : -1;
    }

    public string VideoName
    {
        get => m_videoName;
        set => SetValue(ref m_videoName, value, nameof(VideoName));
    }


    [ContextMenu(nameof(ClearQueryURL))]
    private void ClearQueryURL()
    {
        QueryStringURL = "";
    }
}