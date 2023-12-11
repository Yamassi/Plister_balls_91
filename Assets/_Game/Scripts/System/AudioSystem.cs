using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Audio;

public class AudioSystem : MonoBehaviour
{
    [SerializeField] private AudioSource _musicPlayer, _soundPlayer;
    [SerializeField] protected AudioMixer _audioMixer;
    [SerializeField] private AudioClip[] _allMusic;
    [SerializeField]
    private AudioClip _click, _ballLaunch, _ballFallToObstacle,
    _ballFallToSlot, _winSound, _loseSound;

    public static AudioSystem Instance { get; private set; } = null;
    private void Awake()
    {
        Initialize();
    }
    private async void Initialize()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance == this)
            Destroy(gameObject);

        DontDestroyOnLoad(this.gameObject);

        await UniTask.Delay(100);
        PlayRandomMusic();
    }
    public async void LoadSettingsValues()
    {
        await UniTask.Delay(100);
        float musicVolume = PlayerPrefs.GetFloat("MusicVolume");
        float soundVolume = PlayerPrefs.GetFloat("SoundVolume");
        Debug.Log($"Load Settings musis is {musicVolume}, sound is {soundVolume}");
        _audioMixer.SetFloat("Sound", Mathf.Log10(Mathf.Clamp(soundVolume, 0.0001f, 1f)) * 20);
        _audioMixer.SetFloat("Music", Mathf.Log10(Mathf.Clamp(musicVolume, 0.0001f, 1f)) * 20);
    }
    public void ChangeSoundVolume(float volume)
    {
        _audioMixer.SetFloat("Sound", Mathf.Log10(Mathf.Clamp(volume, 0.0001f, 1f)) * 20);
        PlayerPrefs.SetFloat("SoundVolume", volume);
    }
    public void ChangeMusicVolume(float volume)
    {
        _audioMixer.SetFloat("Music", Mathf.Log10(Mathf.Clamp(volume, 0.0001f, 1f)) * 20);
        PlayerPrefs.SetFloat("MusicVolume", volume);
    }
    public void PlayClick() => PlaySound(_click);

    public void BallLaunch() => PlaySound(_ballLaunch);

    public void BallFallToObstacle() => PlaySound(_ballFallToObstacle);

    public void BallFallToSlot() => PlaySound(_ballFallToSlot);

    public void WinSound() => PlaySound(_winSound);

    public void LoseSound() => PlaySound(_loseSound);

    private void PlaySound(AudioClip audio)
    {
        if (audio != null)
            _soundPlayer.PlayOneShot(audio);
    }
    public void PlayMusicByIndex(int index)
    {
        PlayMusic(_allMusic[index]);
    }
    private void PlayMusic(AudioClip audio)
    {
        if (audio != null)
        {
            _musicPlayer.Stop();
            _musicPlayer.clip = audio;
            _musicPlayer.Play();
        }
    }
    private void PlayRandomMusic()
    {
        int randomMusic = Random.Range(0, _allMusic.Length);
        PlayMusic(_allMusic[randomMusic]);
    }
    private void Update()
    {
        if (!_musicPlayer.isPlaying)
        {
            PlayRandomMusic();
        }
    }
}
