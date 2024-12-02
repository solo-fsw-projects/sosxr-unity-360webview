using SOSXR.EditorTools;
using UnityEngine;


public class FindControllersUsingTag : MonoBehaviour
{
    [SOSXR.EditorTools.TagSelector] [SerializeField] private string m_tagToSearchFor = "XR_Controller";

    [SerializeField] [DisableEditing] private GameObject[] _foundGameObjects;


    public void ToggleAll(bool enableThis)
    {
        if (_foundGameObjects == null || _foundGameObjects.Length == 0)
        {
            _foundGameObjects = GameObject.FindGameObjectsWithTag(m_tagToSearchFor);
        }

        foreach (var found in _foundGameObjects)
        {
            found.SetActive(enableThis);
        }
    }
}