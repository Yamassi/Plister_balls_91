using System;
using UnityEngine;

public class SelectSetState : State
{
    private IUIService _uIService;
    private TopB _topB;
    private SelectSet _selectSet;
    private int _currentSet;
    public SelectSetState(IStateSwitcher stateSwitcher, IDataService dataService, IUIService uIService, TopA topA,
   TopB topB, SelectSet selectSet) : base(stateSwitcher, dataService, topA)
    {
        _uIService = uIService;
        _topB = topB;
        _selectSet = selectSet;
    }
    public override void Enter()
    {
        Debug.Log("Enter Select State");
        _topA.Coins.gameObject.SetActive(true);
        _topA.BackButton.gameObject.SetActive(true);
        _topA.SettingsButton.gameObject.SetActive(true);
        _topB.Header.gameObject.SetActive(true);
        _selectSet.gameObject.SetActive(true);

        _topB.Header.HeaderText.text = "CHOOSE A SET";
        SubcribeToButtons();

        SetSelect();
    }

    public override void Exit()
    {
        Debug.Log("Exit Select State");
        _topA.Coins.gameObject.SetActive(false);
        _topA.BackButton.gameObject.SetActive(false);
        _topA.SettingsButton.gameObject.SetActive(false);
        _topB.Header.gameObject.SetActive(false);
        _selectSet.gameObject.SetActive(false);

        UnsubcribeToButtons();
    }
    private void SubcribeToButtons()
    {
        _topA.BackButton.onClick.AddListener(GoToMainMenu);
        _topA.SettingsButton.onClick.AddListener(GoToSettings);
        _selectSet.PrevButton.Button.onClick.AddListener(PrevSet);
        _selectSet.NextButton.Button.onClick.AddListener(NextSet);

        _selectSet.PlayButton.onClick.AddListener(Play);
    }

    private void UnsubcribeToButtons()
    {
        _topA.BackButton.onClick.RemoveListener(GoToMainMenu);
        _topA.SettingsButton.onClick.RemoveListener(GoToSettings);
        _selectSet.PrevButton.Button.onClick.RemoveListener(PrevSet);
        _selectSet.NextButton.Button.onClick.RemoveListener(NextSet);

        _selectSet.PlayButton.onClick.RemoveListener(Play);
    }

    private void Play()
    {
        PlayerPrefs.SetInt("CurrentSet", _currentSet);
        _stateSwitcher.SwitchState<GamePlayState>();
    }

    private void NextSet()
    {
        if (_currentSet < _dataService.GetData().MySets.Count)
        {
            _currentSet++;
            UpdateSetPreview(_currentSet);
        }
    }

    private void PrevSet()
    {
        if (_currentSet > 0)
        {
            _currentSet--;
            UpdateSetPreview(_currentSet);
        }
    }

    private void SetSelect()
    {
        _currentSet = PlayerPrefs.GetInt("CurrentSet");
        UpdateSetPreview(_currentSet);
    }

    private async void UpdateSetPreview(int index)
    {
        var itemsData = _dataService.GetItemsData();
        int colorID = GetAvailableColorID(_dataService.GetData().MySets[index].background);
        int ballID = GetAvailableBallID(_dataService.GetData().MySets[index].ball);
        int mapId = GetAvailableMapID(_dataService.GetData().MySets[index].map);

        _selectSet.CurrentBall.text = itemsData
        .Balls[colorID].Name;

        _selectSet.CurrentColor.text = itemsData
        .Backgrounds[ballID].Name;

        _selectSet.CurrentMap.text = itemsData
        .Maps[mapId].Name;

        _selectSet.PreviewBackground.sprite = await Tretimi.Assets.GetAsset<Sprite>(
            $"Background{colorID}");
        _selectSet.PreviewBallImage.sprite = await Tretimi.Assets.GetAsset<Sprite>(
            $"Ball{ballID}");
        _selectSet.PreviewMapImage.sprite = await Tretimi.Assets.GetAsset<Sprite>(
            $"Map{mapId}");

        _selectSet.CurrentSetName.text = $"SET {index + 1}";

        if (_currentSet == _dataService.GetData().MySets.Count - 1)
        {
            _selectSet.NextButton.Deactivate();
            _selectSet.NextButton.Button.interactable = false;
        }
        if (_currentSet > 0)
        {
            _selectSet.PrevButton.Activate();
            _selectSet.PrevButton.Button.interactable = true;
        }

        if (_currentSet == 0)
        {
            _selectSet.PrevButton.Deactivate();
            _selectSet.PrevButton.Button.interactable = false;
        }
        if (_currentSet < _dataService.GetData().MySets.Count - 1)
        {
            _selectSet.NextButton.Activate();
            _selectSet.NextButton.Button.interactable = true;
        }
    }

    private void GoToMainMenu()
    {
        _stateSwitcher.SwitchState<MainMenuState>();
    }
    private void GoToSettings()
    {
        PlayerPrefs.SetString("LastPage", "SelectSetState");
        _stateSwitcher.SwitchState<SettingsState>();
    }
}