using SOSXR.EditorTools;
using SOSXR.EnhancedLogger;
using UnityEngine;


public class LoadConfigDataWebViewURL : BaseWebViewURL
{
    [SerializeField] private ConfigData m_configData;
    [SerializeField] private bool m_addVariableString = true;
    [SerializeField] [DisableEditing] private string m_variablesString;


    protected override void Start()
    {
        if (m_addVariableString)
        {
            m_variablesString = $"?{nameof(m_configData.TaskName)}={m_configData.TaskName}&{nameof(m_configData.VideoName)}={m_configData.VideoName}&{nameof(m_configData.PPN)}={m_configData.PPN}";
        }

        BaseURL = m_configData.WebsiteUrl + m_variablesString;

        m_configData.WebsiteUrl = BaseURL;

        this.Debug(BaseURL);

        base.Start();
    }
}