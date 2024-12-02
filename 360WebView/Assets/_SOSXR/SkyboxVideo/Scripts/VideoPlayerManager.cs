using System;
using System.Collections;
using System.Collections.Generic;
using SOSXR;
using SOSXR.EditorTools;
using SOSXR.EnhancedLogger;
using SOSXR.Extensions;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Video;


public class VideoPlayerManager : MonoBehaviour
{
    [Header("Required components")]
    public VideoPlayer VideoPlayer;
    [SerializeField] private AudioSource m_audioSource;
    [SerializeField] private Material m_renderMaterial;
    [SerializeField] public ConfigData m_configData;

    [Header("Clip Settings")]
    [SerializeField] private bool m_startAutomatically = true;
    [SerializeField] [Range(0, 60)] private float m_beforeFirstClipPauseDuration = 5f;
    [SerializeField] public List<VideoSettingsCustom> Clips;
    [SerializeField] [DisableEditing] private List<VideoSettingsCustom> m_randomizedClipList;
    [SerializeField] [Range(0, 60)] private float m_betweenEachClipPauseDuration = 2.5f;
    [SerializeField] private UnityEvent<string> VideoClipStarted;

    [Header("Info")]
    [SerializeField] [DisableEditing] public string CurrentClipName;
    [SerializeField] [DisableEditing] public float CurrentClipDuration;
    [SerializeField] [DisableEditing] public float CurrentClipTime;
    [DisableEditing] public Vector2Int Dimensions;


    private RenderTexture _renderTexture;

    private Coroutine _playerCR;


    public Trials<VideoSettingsCustom> Trials;


    private void Awake()
    {
        if (VideoPlayer == null)
        {
            VideoPlayer = GetComponentInChildren<VideoPlayer>();
        }

        VideoPlayer.source = VideoSource.Url;

        if (m_audioSource == null)
        {
            m_audioSource = GetComponentInChildren<AudioSource>();
        }
    }


    private void Start()
    {
        if (VideoPlayer == null || m_audioSource == null)
        {
            this.Error("VideoPlayer or AudioSource not assigned.");

            return;
        }

        var clipNames = FileHelpers.GetFileNamesFromDirectory(m_configData.Extensions, false, true, m_configData.ClipDirectory);

        foreach (var clipName in clipNames)
        {
            Clips.Add(new VideoSettingsCustom
            {
                ClipName = clipName
            });
        }

        if (m_startAutomatically)
        {
            StartPlayer(null);
        }
    }


    private void OnEnable()
    {
        //EventsSystem.KeyCodeEntered += StartPlayer;
        VideoPlayer.errorReceived += ReceivedAnError;
    }


    private void ReceivedAnError(VideoPlayer source, string message)
    {
        this.Error("The VideoPlayer has received an error", source, message);
    }


    public void StartPlayer(string unused)
    {
        if (Clips.Count == 0)
        {
            this.Error("Could not find any available clips in", m_configData.ClipDirectory);

            return;
        }

        _playerCR = StartCoroutine(PlayerCR());
    }


    private IEnumerator PlayerCR()
    {
        yield return new WaitForSeconds(m_beforeFirstClipPauseDuration);

        StartCoroutine(UpdateCurrentClipTimeCR());

        do
        {
            Trials = new Trials<VideoSettingsCustom>(Clips, m_configData);

            m_randomizedClipList = Trials.OrderedConditions;

            foreach (var clip in m_randomizedClipList)
            {
                this.Debug("Playing clip", clip.ClipName, "from", m_randomizedClipList.Count, "clips.");

                VideoClipStarted?.Invoke(clip.ClipName);

                CurrentClipName = clip.ClipName;

                GetURLAndPrepare(clip);

                while (!VideoPlayer.isPrepared)
                {
                    this.Debug("Preparing clip");

                    yield return new WaitForSeconds(0.1f);
                }

                CreateNewRenderTexture();

                SetAudioSourceSettings(clip);

                CurrentClipDuration = (float) VideoPlayer.length.RoundCorrectly(0);

                VideoPlayer.Play();

                m_audioSource.enabled = true;

                this.Debug("Playing clip");

                yield return new WaitForSeconds(CurrentClipDuration);

                StopPlaying();

                yield return new WaitForSeconds(m_betweenEachClipPauseDuration);
            }
        } while (m_configData.PlayWay == PlayWay.Repeat);

        this.Debug("Done playing all clips");
    }


    private IEnumerator UpdateCurrentClipTimeCR()
    {
        for (;;)
        {
            CurrentClipTime = (float) VideoPlayer.clockTime.RoundCorrectly(0);

            yield return new WaitForSeconds(1);
        }
    }


    private void GetURLAndPrepare(VideoSettingsCustom clip)
    {
        VideoPlayer.url = m_configData.ClipDirectory + "/" + clip.ClipName;

        VideoPlayer.Prepare();
    }


    private void CreateNewRenderTexture()
    {
        Dimensions.x = (int) VideoPlayer.width;
        Dimensions.y = (int) VideoPlayer.height;
        _renderTexture = new RenderTexture(Dimensions.x, Dimensions.y, 24, RenderTextureFormat.Default);
        _renderTexture.name = "RenderTexture: " + Dimensions;

        m_renderMaterial.mainTexture = _renderTexture;
        VideoPlayer.targetTexture = _renderTexture;
    }


    private void SetAudioSourceSettings(VideoSettingsCustom clip)
    {
        VideoPlayer.SetTargetAudioSource(0, m_audioSource);

        m_audioSource.spatialBlend = clip.AudioLocation == Vector3.zero ? 0 : 1;

        m_audioSource.transform.position = clip.AudioLocation;
    }


    private void StopPlaying()
    {
        VideoPlayer.Stop();
        VideoPlayer.clip = null;
        m_renderMaterial.mainTexture = new RenderTexture(0, 0, 0);
        m_audioSource.Stop();
        m_audioSource.enabled = false;
        this.Debug("Stopping playing");
    }


    [ContextMenu(nameof(ReshuffleVideos))]
    public void ReshuffleVideos()
    {
        m_configData.Order = Order.Random;

        StartPlaying();
    }


    private void StartPlaying()
    {
        StopPlaying();

        if (_playerCR != null)
        {
            StopCoroutine(_playerCR);

            _playerCR = null;
        }

        _playerCR = StartCoroutine(PlayerCR());
    }


    private void OnDisable()
    {
        //EventsSystem.KeyCodeEntered -= StartPlayer;
        VideoPlayer.errorReceived -= ReceivedAnError;

        StopAllCoroutines();
    }
}


[Serializable]
public class VideoSettingsCustom
{
    [Tooltip("Full name, without path, but with extension. E.g. 'myVideo.mp4'")]
    public string ClipName;
    public Vector3 AudioLocation;
}