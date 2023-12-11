using System;
using UnityEditor.Build.Player;
using UnityEngine;

public class SettingsState : State
{
    private Settings _settings;
    public SettingsState(IStateSwitcher stateSwitcher, IDataService dataService,
    TopA topA, Settings settings) : base(stateSwitcher, dataService, topA)
    {
        _settings = settings;
    }
    public override void Enter()
    {
        _topA.BackButton.gameObject.SetActive(true);
        _topA.SettingsHeader.gameObject.SetActive(true);
        _settings.gameObject.gameObject.SetActive(true);

        _topA.SettingsHeader.HeaderText.text = "SETTINGS";

        _topA.BackButton.onClick.AddListener(GoToLastPage);

        _settings.MusicMinusButton.Button.onClick.AddListener(DecreaseMusic);
        _settings.MusicPlusButton.Button.onClick.AddListener(IncreaseMusic);

        _settings.SoundMinusButton.Button.onClick.AddListener(DecreaseSound);
        _settings.SoundPlusButton.Button.onClick.AddListener(IncreaseSound);

        UpdateUI();
    }

    public override void Exit()
    {
        _topA.BackButton.gameObject.SetActive(false);
        _topA.SettingsHeader.gameObject.SetActive(false);
        _settings.gameObject.gameObject.SetActive(false);

        _topA.BackButton.onClick.RemoveListener(GoToLastPage);

        _settings.MusicMinusButton.Button.onClick.RemoveListener(DecreaseMusic);
        _settings.MusicPlusButton.Button.onClick.RemoveListener(IncreaseMusic);

        _settings.SoundMinusButton.Button.onClick.RemoveListener(DecreaseSound);
        _settings.SoundPlusButton.Button.onClick.RemoveListener(IncreaseSound);
    }
    private void UpdateUI()
    {
        float musicVolume = PlayerPrefs.GetFloat("MusicVolume") * 10;
        float soundVolume = PlayerPrefs.GetFloat("SoundVolume") * 10;

        _settings.SoundSlider.value = soundVolume;
        _settings.MusicSlider.value = musicVolume;

        _settings.SoundValueText.text = $"{soundVolume}/10";
        _settings.MusicValueText.text = $"{musicVolume}/10";
    }
    private void IncreaseSound()
    {
        _settings.SoundSlider.value++;
        float volume = _settings.SoundSlider.value / 10;
        AudioSystem.Instance.ChangeSoundVolume(volume);
        _settings.SoundValueText.text = $"{_settings.SoundSlider.value}/10";
    }

    private void DecreaseSound()
    {
        _settings.SoundSlider.value--;
        float volume = _settings.SoundSlider.value / 10;
        AudioSystem.Instance.ChangeSoundVolume(volume);
        _settings.SoundValueText.text = $"{_settings.SoundSlider.value}/10";
    }

    private void IncreaseMusic()
    {
        _settings.MusicSlider.value++;
        float volume = _settings.MusicSlider.value / 10;
        AudioSystem.Instance.ChangeMusicVolume(volume);
        _settings.MusicValueText.text = $"{_settings.MusicSlider.value}/10";
    }

    private void DecreaseMusic()
    {
        _settings.MusicSlider.value--;
        float volume = _settings.MusicSlider.value / 10;
        AudioSystem.Instance.ChangeMusicVolume(volume);
        _settings.MusicValueText.text = $"{_settings.MusicSlider.value}/10";
    }
    private void GoToLastPage()
    {
        string lastPage = PlayerPrefs.GetString("LastPage");
        switch (lastPage)
        {
            case "ShopState":
                _stateSwitcher.SwitchState<ShopState>();
                break;
            case "GamePlayState":
                _stateSwitcher.SwitchState<GamePlayState>();
                break;
            case "MySetsState":
                _stateSwitcher.SwitchState<MySetsState>();
                break;
            case "SelectSetState":
                _stateSwitcher.SwitchState<SelectSetState>();
                break;
        }

    }
}