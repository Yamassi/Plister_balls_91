using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using Tretimi;
using UnityEngine;
public class Game : IStateSwitcher, IDataService, IUIService
{
    public SaveData Data;
    private UIHolder _uIHolder;
    private GamePlay _gamePlay;
    private StateMachine _stateMachine;
    private List<State> _allStates;
    private State _currentState;
    private ItemsData _itemsData;
    public Game(UIHolder uIHolder, GamePlay gamePlay, ItemsData itemsData)
    {
        _uIHolder = uIHolder;
        _gamePlay = gamePlay;
        _itemsData = itemsData;
    }

    public async Task InitAsync()
    {
        LoadData();
        UpdateUI();

        _stateMachine = new();

        _allStates = new(){
            new LoadingState(this,this,_uIHolder.TopA,
            _uIHolder.Loading),

            new MainMenuState(this,this,this,_uIHolder.TopA,
            _uIHolder.TopB,_uIHolder.BottomB.MainMenu),

            new ShopState(this,this,this, _uIHolder.TopA,
            _uIHolder.TopB.ShopButtons, _uIHolder.BottomB.Shop),

            new MySetsState(this, this,_uIHolder.TopA,
            _uIHolder.BottomA.MySets),

            new SelectSetState(this,this,this, _uIHolder.TopA,
           _uIHolder.TopB, _uIHolder.BottomB.SelectSet),

            new ConfigureSetState(this,this, _uIHolder.TopA,
            _uIHolder.BottomA.ConfigureSet),

            new ConfigureDifficultyState(this,this,_uIHolder.TopA,
            _uIHolder.BottomA.ConfigureDifficulty),

            new GamePlayState(this,this,this,_uIHolder.TopA,
            _uIHolder.BottomA.GamePlayUI, _gamePlay),

            new SettingsState(this,this,_uIHolder.TopA,
            _uIHolder.BottomA.Settings),
        };

        _currentState = _allStates[0];

        _stateMachine.Init(_currentState);

        await UniTask.Delay(10000);

        RequestToRate();
    }

    private void RequestToRate()
    {
#if UNITY_IOS
Device.RequestStoreReview();
#endif
    }

    public void SwitchState<T>() where T : State
    {
        var state = _allStates.First(s => s is T);
        _stateMachine.ChangeState(state);
    }

    private void LoadData()
    {
        SaveData saveData = DataProvider.LoadDataJSON();

        if (saveData is null)
        {
            List<int> resetedList = new() { 0 };

            DateTime currentTime = DateTime.Now.AddHours(-1);
            Debug.Log("Saved Data is null and reseted");
            Data = new()
            {
                Coins = Const.FirstCoins,

                AvailableBalls = new(resetedList),
                AvailableMaps = new(resetedList),
                AvailableBackgrounds = new(resetedList),
                MySets = new List<(int ball, int background, int map)>() { (0, 0, 0) },
                TimeToOpenGift = currentTime.ToString(CultureInfo.InvariantCulture),
            };

            PlayerPrefs.SetFloat("MusicVolume", 1);
            PlayerPrefs.SetFloat("SoundVolume", 1);
            PlayerPrefs.SetInt("CurrentBackground", 0);
            PlayerPrefs.SetInt("CurrentSet", 0);
            PlayerPrefs.SetInt("CurrentConfigureSet", 0);
            PlayerPrefs.SetInt("CurrentDifficulty", 0);
            PlayerPrefs.SetInt("CurrentWeight", 10);
            PlayerPrefs.SetInt("CurrentCost", 10);
        }
        else
            Data = saveData;

        AudioSystem.Instance.LoadSettingsValues();
    }
    public SaveData GetData()
    {
        return Data;
    }
    public ItemsData GetItemsData()
    {
        return _itemsData;
    }
    public void UpdateUI()
    {
        _uIHolder.TopA.Coins.CoinsText.text = Data.Coins.ToString();
    }

    public void AddCoins(int coins)
    {
        Data.Coins += coins;
    }

    public void RemoveCoins(int coins)
    {
        Data.Coins -= coins;
        if (Data.Coins <= 0)
            Data.Coins = 0;
    }

    public async void ChangeBackground(int index)
    {
        _uIHolder.GameBackground.sprite = await Tretimi.Assets.GetAsset<Sprite>($"Back{index}");
    }
}

public interface IStateSwitcher
{
    void SwitchState<T>() where T : State;
}
public interface IUIService
{
    void UpdateUI();
    void ChangeBackground(int backgroundID);
}
public interface IDataService
{
    SaveData GetData();
    ItemsData GetItemsData();
    void AddCoins(int coins);
    void RemoveCoins(int coins);

}