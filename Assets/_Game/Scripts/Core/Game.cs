using System;
using System.Collections.Generic;
using System.Linq;
using Tretimi;
using UnityEngine;
public class Game : IStateSwitcher, IDataService
{
    public SaveData Data;
    private UIHolder _uIHolder;
    private GamePlay _gamePlay;
    private StateMachine _stateMachine;
    private List<State> _allStates;
    private State _currentState;

    public Game(UIHolder uIHolder, GamePlay gamePlay)
    {
        _uIHolder = uIHolder;
        _gamePlay = gamePlay;
    }

    public void Init()
    {
        LoadData();
        UpdateUI();

        _stateMachine = new();

        _allStates = new(){
            new LoadingState(this,this,_uIHolder.TopA,
            _uIHolder.Loading),

            new MainMenuState(this,this,_uIHolder.TopA,
            _uIHolder.TopB,_uIHolder.BottomB.MainMenu),

            new ShopState(this,this, _uIHolder.TopA,
            _uIHolder.TopB.ShopButtons, _uIHolder.BottomB.Shop),

            new MySetsState(this, this,_uIHolder.TopA,
            _uIHolder.BottomA.MySets),

            new SelectSetState(this,this, _uIHolder.TopA,
           _uIHolder.TopB, _uIHolder.BottomB.SelectSet),

            new ConfigureSetState(this,this, _uIHolder.TopA,
            _uIHolder.BottomA.ConfigureSet),

            new ConfigureDifficultyState(this,this,_uIHolder.TopA,
            _uIHolder.BottomA.ConfigureDifficulty),

            new GamePlayState(this,this,_uIHolder.TopA,
            _uIHolder.BottomA.GamePlayUI, _gamePlay),

            new SettingsState(this,this,_uIHolder.TopA,
            _uIHolder.BottomA.Settings),
        };

        _currentState = _allStates[0];

        _stateMachine.Init(_currentState);
    }

    public void SwitchState<T>() where T : State
    {
        var state = _allStates.FirstOrDefault(s => s is T);
        _stateMachine.ChangeState(state);
    }

    private void LoadData()
    {
        SaveData saveData = DataProvider.LoadData();

        if (saveData is null)
        {
            List<int> resetedList = new() { 0 };

            DateTime currentTime = DateTime.Now;
            Debug.Log("Saved Data is null and reseted");
            Data = new()
            {
                Coins = Const.FirstCoins,

                AvailableBalls = new(resetedList),
                AvailableMaps = new(resetedList),
                AvailableBackgrounds = new(resetedList),
                MySets = new List<(int, int, int)>() { (0, 0, 0) },

                TimeToOpenGift = currentTime.AddHours(-1).ToString(),
            };

            PlayerPrefs.SetFloat("MusicVolume", 1);
            PlayerPrefs.SetFloat("SoundVolume", 1);
            PlayerPrefs.SetInt("CurrentBackground", 0);
            PlayerPrefs.SetInt("CurrentSet", 0);
        }
        else
            Data = saveData;
    }
    public SaveData GetData()
    {
        return Data;
    }

    public void UpdateUI()
    {
        _uIHolder.TopA.Coins.CoinsText.text = Data.Coins.ToString();
    }
}

public interface IStateSwitcher
{
    void SwitchState<T>() where T : State;
}

public interface IDataService
{
    SaveData GetData();
    void UpdateUI();
}