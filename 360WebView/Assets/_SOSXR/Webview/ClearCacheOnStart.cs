using UnityEngine;
using Vuplex.WebView;


public class ClearCacheOnStart : MonoBehaviour
{
    [SerializeField] private bool m_clearCacheOnStart = true;


    private void Awake()
    {
        if (!m_clearCacheOnStart)
        {
            return;
        }

        Web.ClearAllData();
    }
}