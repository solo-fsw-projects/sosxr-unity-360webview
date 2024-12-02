using SOSXR.EditorTools;
using UnityEngine;


[CreateAssetMenu(fileName = "ConfigData - DefaultValues", menuName = "SOSXR/ConfigData - DefaultValues")]
public class DefaultConfigData : ScriptableObject
{
    [Header("Website")]
    public string WebsiteUrl = "https://researchwiki.solo.universiteitleiden.nl/xwiki/wiki/researchwiki.solo.universiteitleiden.nl/view/VR%20in%20Unity3D/Webview%20Tasks/#HTasks";

    [Header("Movies")]
    public string ClipDirectory;
    public string[] Extensions = {".mp4"};

    public PlayWay PlayWay = PlayWay.One;
    [DisableEditing] public string PlayWayString = "One";
    public Order Order = Order.Counterbalanced;
    [DisableEditing] public string OrderString = "Counterbalanced";

    [Header("Web Metadata")]
    public string TaskName = "TaskToDo";
    public string VideoName = "VideoName";
    public int PPN = -1;

    [Header("Debug")]
    public bool ShowDebug = false;


    private void OnEnable()
    {
        ClipDirectory = FileHelpers.GetArborXRPath();
        PlayWayString = PlayWay.ToString();
        OrderString = Order.ToString();
    }
}


public enum PlayWay
{
    One,
    All,
    Repeat
}


public enum Order
{
    InOrder,
    Random,
    Permutation,
    Counterbalanced
}