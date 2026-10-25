using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [Header("Background Music")]
    public AudioClip bgm;

    [Header("Sound Effects")]
    public AudioClip jump;
    public AudioClip land;
    public AudioClip stomp;
    public AudioClip coin;
    public AudioClip powerup;
    public AudioClip grow;
    public AudioClip death;
    public AudioClip victory;

    [Header("Volumes")]
    public float musicVolume = 0.7f;
    public float sfxVolume = 1f;

    private AudioSource musicSource;

    private void Awake()
    {
        if (Instance != null) { Destroy(gameObject); return; }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        LoadVolumes();

        musicSource = gameObject.AddComponent<AudioSource>();
        musicSource.loop = true;
        musicSource.playOnAwake = false;
        musicSource.volume = musicVolume;
    }

    private void LoadVolumes()
    {
        musicVolume = PlayerPrefs.GetFloat("MusicVolume", 0.7f);
        sfxVolume = PlayerPrefs.GetFloat("SFXVolume", 1f);
    }

    public void SetMusicVolume(float v) { musicVolume = v; if (musicSource) musicSource.volume = v; PlayerPrefs.SetFloat("MusicVolume", v); }
    public void SetSFXVolume(float v) { sfxVolume = v; PlayerPrefs.SetFloat("SFXVolume", v); }

    private void Start() => PlayMusic(bgm);

    public void PlayMusic(AudioClip clip)
    {
        if (clip != null) { musicSource.clip = clip; musicSource.Play(); }
    }

    public void PlaySFX(AudioClip clip, Vector3 position = default)
    {
        if (clip == null) return;
        if (position == default) position = Camera.main.transform.position;
        AudioSource.PlayClipAtPoint(clip, position, 0.85f * sfxVolume);
    }
}