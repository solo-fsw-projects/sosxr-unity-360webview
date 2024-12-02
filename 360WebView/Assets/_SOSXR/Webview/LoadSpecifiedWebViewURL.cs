using UnityEngine;


public class LoadSpecifiedWebViewURL : BaseWebViewURL
{
    [SerializeField] private string m_URLToLoad;


    protected override void Start()
    {
        BaseURL = m_URLToLoad;
        base.Start();
    }
}