using System;
using UnityEngine;

public class ConfigureDifficultyState : State
{
    private ConfigureDifficulty _configureDifficulty;
    private int _currentDifficulty, _currentCost, _currentWeight;
    public ConfigureDifficultyState(IStateSwitcher stateSwitcher,
    IDataService dataService, TopA topA,
    ConfigureDifficulty configureDifficulty) : base(stateSwitcher, dataService, topA)
    {
        _configureDifficulty = configureDifficulty;
    }
    public override void Enter()
    {
        _topA.Coins.gameObject.SetActive(true);
        _topA.BackButton.gameObject.SetActive(true);
        _topA.SettingsButton.gameObject.SetActive(true);
        _configureDifficulty.gameObject.SetActive(true);

        SubscribeToButtons();

        ConfigureCurrentDifficulty();
    }

    public override void Exit()
    {
        _topA.Coins.gameObject.SetActive(false);
        _topA.BackButton.gameObject.SetActive(false);
        _topA.SettingsButton.gameObject.SetActive(false);
        _configureDifficulty.gameObject.SetActive(false);

        UnsubscribeToButtons();
    }

    private void ConfigureCurrentDifficulty()
    {
        _currentDifficulty = PlayerPrefs.GetInt("CurrentDifficulty");
        _currentCost = PlayerPrefs.GetInt("CurrentCost");
        _currentWeight = PlayerPrefs.GetInt("CurrentWeight");

        _configureDifficulty.SetCost(_currentCost);
        _configureDifficulty.SetWeight(_currentWeight);
        _configureDifficulty.SetDifficulty(_currentDifficulty);
    }
    private void SubscribeToButtons()
    {
        _topA.BackButton.onClick.AddListener(GoToGamePlay);
        _configureDifficulty.PlayButton.onClick.AddListener(GoToGamePlay);

        _configureDifficulty.OnCostSelect += CostChanged;
        _configureDifficulty.OnWeightSelect += WeightChanged;
    }

    private void WeightChanged(int weight)
    {
        _currentWeight = weight;
    }

    private void CostChanged(int cost)
    {
        _currentCost = cost;
    }

    private void UnsubscribeToButtons()
    {
        _topA.BackButton.onClick.AddListener(GoToGamePlay);
        _configureDifficulty.PlayButton.onClick.RemoveListener(GoToGamePlay);

        _configureDifficulty.OnCostSelect -= CostChanged;
        _configureDifficulty.OnWeightSelect -= WeightChanged;
    }

    private void GoToGamePlay()
    {
        PlayerPrefs.SetInt("CurrentDifficulty", _currentDifficulty);
        PlayerPrefs.SetInt("CurrentCost", _currentCost);
        PlayerPrefs.SetInt("CurrentWeight", _currentWeight);

        _stateSwitcher.SwitchState<GamePlayState>();
    }


}