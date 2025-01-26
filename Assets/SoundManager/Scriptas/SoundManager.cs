using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : SingletonMonoBehaviour<SoundManager>
{
    public const string BGM_RESOURCES_PATH = "Sounds/BGM/";
    public const string SE_RESOURCES_PATH = "Sounds/SE/";

    [Header("Settings")]
    [SerializeField] private int _bgmChannel = 1;
    [SerializeField] private int _seChannel = 5;

    private AudioSource[] _bgmSources;
    private AudioSource[] _seSources;

    private Dictionary<string, AudioClip> _bgmClipPool;
    private Dictionary<string, AudioClip> _seClipPool;

    public void PlayBGM(string poolKey, float vol = 1, float pitch = 1)
    {
        try
        {
            AudioSource source = GetBGMSource();
            source.clip = _bgmClipPool[poolKey];
            source.loop = true;
            source.volume = vol;
            source.pitch = pitch;

            source.Play();
        }
        catch(System.Exception e)
        { 
            Debug.LogError($"BGM���Đ��ł��܂���B>>> \n{ e }");
        }
    }

    public void PlaySE(string poolKey, float vol = 1, float pitch = 1)
    {
        try
        {
            AudioSource source = GetSESource();
            source.clip = _seClipPool[poolKey];
            source.loop = false;
            source.volume = vol;
            source.pitch = pitch;

            source.PlayOneShot(source.clip);
        }
        catch(System.Exception e)
        { 
            //Debug.LogError($"SE���Đ��ł��܂���B>>> \n{ e }");
        }
    }


    private AudioSource GetBGMSource()
    { 
        AudioSource source = null;

        for(int i = 0; i < _bgmChannel; i++)
        {
            AudioSource check = _bgmSources[i];
            if(!check.isPlaying)
            {
                source = check;
                break;
            }
        }

        return source;
    }

    private AudioSource GetSESource()
    { 
        AudioSource source = null;

        for(int i = 0; i < _seChannel; i++)
        {
            AudioSource check = _seSources[i];
            if(!check.isPlaying)
            {
                source = check;
                break;
            }
        }

        return source;
    }

    private void LoadBGM()
    {
        AudioClip[] clips = Resources.LoadAll<AudioClip>(BGM_RESOURCES_PATH);
        for(int i = 0; i < clips.Length; i++)
        {
            _bgmClipPool.Add(clips[i].name, clips[i]);
            
            // Debug.Log($"BGM Pool: added >>{clips[i].name }");
        }
    }

    private void LoadSE()
    { 
        AudioClip[] clips = Resources.LoadAll<AudioClip>(SE_RESOURCES_PATH);
        for(int i = 0; i < clips.Length; i++)
        {
            _seClipPool.Add(clips[i].name, clips[i]);
            
            // Debug.Log($"BGM Pool: added >>{clips[i].name }");
        }
    }

    private void CreateBGMSource()
    {
        GameObject bgmBox = new GameObject("BGM");
        bgmBox.transform.SetParent(transform);
        for(int i = 0; i < _bgmChannel; i++)
        {
            AudioSource source = bgmBox.AddComponent<AudioSource>();
            _bgmSources[i] = source;
        }
    }


    private void CreateSESource()
    {
        GameObject seBox = new GameObject("SE");
        seBox.transform.SetParent(transform);
        for(int i = 0; i < _seChannel; i++)
        {
            AudioSource source = seBox.AddComponent<AudioSource>();
            _seSources[i] = source;
        }
    }

    void Awake()
    {
        base.Awake();

        _bgmSources = new AudioSource[_bgmChannel];
        _seSources = new AudioSource[_seChannel];

        _bgmClipPool = new Dictionary<string, AudioClip>();
        _seClipPool = new Dictionary<string, AudioClip>();

        CreateBGMSource();
        LoadBGM();

        CreateSESource();
        LoadSE();
    }

    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }
}

