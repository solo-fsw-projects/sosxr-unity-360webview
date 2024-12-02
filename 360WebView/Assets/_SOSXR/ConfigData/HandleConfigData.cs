using mrstruijk.Events;
using SOSXR.EnhancedLogger;
using UnityEngine;
using UnityEngine.Events;


public class HandleConfigData : MonoBehaviour
{
    public ConfigData ConfigData;
    
    [SerializeField] private UnityEvent ConfigInformationChanged;


    private void Awake()
    {
        ConfigData.LoadConfig();
    }
    

    public void ChangeVideoClipName(string videoClipName)
    {
        AmendConfigData(videoClipName: videoClipName);
    }

    
    public void AddParticipantNumber(string participantNumber)
    {
        if (int.TryParse(participantNumber, out var result))
        {
            AmendConfigData(result);
        }
        else
        {
            this.Error("Participant number is not a number");
        }
    }

    public void AddParticipantNumber(int participantNumber)
    {
        AmendConfigData(participantNumber);
    }


    private void AmendConfigData(int participantNumber = -1, string videoClipName = null)
    {
        if (participantNumber != -1)
        {
            ConfigData.PPN = participantNumber;
        }

        if (videoClipName != null)
        {
            ConfigData.VideoName = videoClipName;
        }

      ConfigInformationChanged?.Invoke();
    }


}