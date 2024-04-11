using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using VS.Base.Manager;

public enum ESoundType
{
    BGM,
    BtnClicked,
    BlockRelease,
    MaxCnt
}

public class SoundManager : Base_Manager
{
    [SerializeField]
    private AudioMixer mixer;

    private AudioMixerGroup[] mixerGroup;

    [Range(-80, 20)]
    public float master = 0;

    [Range(-80, 20)]
    public float bgm = 0;

    [Range(-80, 20)]
    public float sfx = 0;

    /*
    private bool bgmON = true;
    private bool effectON = true;

    private AudioSource[] audioSources = new AudioSource[(int)ESoundType.MaxCnt];
    private Dictionary<string, AudioClip> audioClips = new Dictionary<string, AudioClip>();

    // getter setter
    public bool BGM_ON { get { return bgmON; } set { bgmON = value; } }
    public bool Effect_ON { get { return effectON; } set { effectON = value; } }
    */

    protected override void Logic_Init_Custom()
    {
        //mixerGroup = mixer.FindMatchingGroups("");
    }
    
    public void MixerControl()
    {
        mixer.SetFloat(nameof(master), master);
        mixer.SetFloat(nameof(bgm), bgm);
        mixer.SetFloat(nameof(sfx), sfx);
    }

    public void ToggleAudioVolume()
    {
        AudioListener.volume = (AudioListener.volume == 0) ? 1 : 0;
    }

    void Awake()
    {
        //Init();
    }

    /*
    void Init()
    {
        // create audio source and clip object
        GameObject sound = GameObject.Find("Sound");
        if (sound == null)
        {
            sound = new GameObject { name = "Sound" };
            DontDestroyOnLoad(sound);

            string[] soundNames = System.Enum.GetNames(typeof(ESoundType));

            for (int i = 0; i < soundNames.Length - 1; i++)
            {
                GameObject go = new GameObject { name = soundNames[i] };
                audioSources[i] = go.AddComponent<AudioSource>();
                go.transform.parent = sound.transform;

                string path = "Sound/" + soundNames[i];
                AudioClip audioClip = Resources.Load<AudioClip>(path);

                if (audioClip != null)
                {
                    audioClips.Add(soundNames[i], audioClip);
                }
            }

            audioSources[(int)ESoundType.BGM].loop = true;
        }

    }

    public void Play(ESoundType soundType)
    {
        string[] soundNames = System.Enum.GetNames(typeof(ESoundType));

        switch ((int)soundType)
        {
            case (int)ESoundType.BGM:
                {
                    if (bgmON == false)
                    {
                        break;
                    }

                    AudioSource audio = audioSources[(int)ESoundType.BGM];
                    audio.clip = audioClips[soundNames[(int)ESoundType.BGM]];
                    audio.Play();
                }
                break;
            case (int)ESoundType.BtnClicked:
                {
                    if (effectON == false)
                    {
                        break;
                    }

                    AudioSource audio = audioSources[(int)ESoundType.BtnClicked];
                    AudioClip clip = audioClips[soundNames[(int)ESoundType.BtnClicked]];
                    audio.PlayOneShot(clip);
                }
                break;
            case (int)ESoundType.BlockRelease:
                {
                    if (effectON == false)
                    {
                        break;
                    }

                    AudioSource audio = audioSources[(int)ESoundType.BlockRelease];
                    AudioClip clip = audioClips[soundNames[(int)ESoundType.BlockRelease]];
                    audio.PlayOneShot(clip);
                }
                break;
            default:
                break;
        }
    }

    public bool IsPlayBGM()
    {
        AudioSource audio = audioSources[(int)ESoundType.BGM];
        return audio.isPlaying;
    }

    public void Stop(ESoundType soundType)
    {
        string[] soundNames = System.Enum.GetNames(typeof(ESoundType));

        switch ((int)soundType)
        {
            case (int)ESoundType.BGM:
                {
                    AudioSource audio = audioSources[(int)ESoundType.BGM];
                    audio.clip = audioClips[soundNames[(int)ESoundType.BGM]];
                    audio.Stop();
                }
                break;
            default:
                break;
        }
    }
    */
}
