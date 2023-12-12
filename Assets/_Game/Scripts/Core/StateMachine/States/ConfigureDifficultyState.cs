using System;
using UnityEngine;

public class ConfigureDifficultyState : State
{
    private ConfigureDifficulty _configureDifficulty;
    private int _currentDifficulty, _currentCost,
    _currentWeight, _currentMap;
    public ConfigureDifficultyState(IStateSwitcher stateSwitcher,
    IDataService dataService, TopA topA,
    ConfigureDifficulty configureDifficulty) : base(stateSwitcher, dataService, topA)
    {
        _configureDifficulty = configureDifficulty;
    }
    public override void Enter()
    {
        Debug.Log("Enter Configure Difficulty State");
        _topA.Coins.gameObject.SetActive(true);
        _topA.BackButton.gameObject.SetActive(true);
        _topA.SettingsButton.gameObject.SetActive(true);
        _configureDifficulty.gameObject.SetActive(true);

        SubscribeToButtons();

        ConfigureCurrentDifficulty();
    }

    public override void Exit()
    {
        Debug.Log("Exit Configure Difficulty State");
        _topA.Coins.gameObject.SetActive(false);
        _topA.BackButton.gameObject.SetActive(false);
        _topA.SettingsButton.gameObject.SetActive(false);
        _configureDifficulty.gameObject.SetActive(false);

        UnsubscribeToButtons();
    }
    private void SubscribeToButtons()
    {
        _topA.BackButton.onClick.AddListener(GoToGamePlay);
        _configureDifficulty.PlayButton.onClick.AddListener(GoToGamePlay);
        _topA.SettingsButton.onClick.AddListener(GoToSettings);

        _configureDifficulty.OnCostSelect += CostChanged;
        _configureDifficulty.OnWeightSelect += WeightChanged;
        _configureDifficulty.OnNextSelect += NextItem;
        _configureDifficulty.OnPrevSelect += PrevItem;
    }
    private void UnsubscribeToButtons()
    {
        _topA.BackButton.onClick.AddListener(GoToGamePlay);
        _configureDifficulty.PlayButton.onClick.RemoveListener(GoToGamePlay);
        _topA.SettingsButton.onClick.RemoveListener(GoToSettings);

        _configureDifficulty.OnCostSelect -= CostChanged;
        _configureDifficulty.OnWeightSelect -= WeightChanged;
        _configureDifficulty.OnNextSelect -= NextItem;
        _configureDifficulty.OnPrevSelect -= PrevItem;
    }
    private void ConfigureCurrentDifficulty()
    {
        _currentDifficulty = PlayerPrefs.GetInt("CurrentDifficulty");
        _currentCost = PlayerPrefs.GetInt("CurrentCost");
        _currentWeight = PlayerPrefs.GetInt("CurrentWeight");
        _currentMap = PlayerPrefs.GetInt("CurrentMap");

        _configureDifficulty.SetCost(_currentCost);
        _configureDifficulty.SetWeight(_currentWeight);
        _configureDifficulty.SetCurrentDifficulty(_currentDifficulty);

        PrepareDifficultyItems();
    }

    private async void PrepareDifficultyItems()
    {

        _configureDifficulty.ClearConfigureSetItems();

        DifficultyItem firstEmptySetItem = _configureDifficulty.CreateSetItem();
        firstEmptySetItem.SetEmpty();
        _configureDifficulty.DifficultyItems.Add(firstEmptySetItem);

        for (int i = 0; i < 5; i++)
        {
            DifficultyItem difficultyItem = _configureDifficulty.CreateSetItem();
            Sprite sprite = await Tretimi.Assets.GetAsset<Sprite>($"Map_{_currentMap}_{i}");
            difficultyItem.MapImage.sprite = sprite;
            _configureDifficulty.DifficultyItems.Add(difficultyItem);
        }

        DifficultyItem lastEmptySetItem = _configureDifficulty.CreateSetItem();
        lastEmptySetItem.SetEmpty();
        _configureDifficulty.DifficultyItems.Add(lastEmptySetItem);

        _configureDifficulty.SetCurrentDifficulty(_currentDifficulty);
        _configureDifficulty.CurrentDifficultyNumber.text = $"{_currentDifficulty + 1}/5";
    }

    private void PrevItem()
    {
        _currentDifficulty--;
        _configureDifficulty.CurrentDifficultyNumber.text = $"{_currentDifficulty + 1}/5";
    }

    private void NextItem()
    {
        _currentDifficulty++;
        _configureDifficulty.CurrentDifficultyNumber.text = $"{_currentDifficulty + 1}/5";
    }



    private void WeightChanged(int weight)
    {
        _currentWeight = weight;
    }

    private void CostChanged(int cost)
    {
        _currentCost = cost;
    }

    private void GoToGamePlay()
    {
        PlayerPrefs.SetInt("CurrentDifficulty", _currentDifficulty);
        PlayerPrefs.SetInt("CurrentCost", _currentCost);
        PlayerPrefs.SetInt("CurrentWeight", _currentWeight);

        _stateSwitcher.SwitchState<GamePlayState>();
    }

    private void GoToSettings()
    {
        PlayerPrefs.SetString("LastPage", "ConfigureDifficultyState");
        _stateSwitcher.SwitchState<SettingsState>();
    }
}