using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class ConfigureSetState : State
{
    private ConfigureSet _configureSet;
    private int _currentConfigureSet;
    private int _currentMap, _currentBall, _currentColor;
    private ItemType _currentItemType;
    public ConfigureSetState(IStateSwitcher stateSwitcher, IDataService dataService,
    TopA topA, ConfigureSet configureSet) : base(stateSwitcher, dataService, topA)
    {
        _configureSet = configureSet;
    }
    public override void Enter()
    {
        _currentConfigureSet = PlayerPrefs.GetInt("CurrentConfigureSet");

        _topA.Header.HeaderText.text = $"SET {_currentConfigureSet + 1}";
        _topA.Header.gameObject.SetActive(true);
        _configureSet.gameObject.SetActive(true);

        _configureSet.Init();
        ColorsSelect();

        if (_currentConfigureSet <= _dataService.GetData().MySets.Count - 1)
            LoadConfigure();
        else
            ConfigureNew();

        SubscribeToButtons();
    }

    public override void Exit()
    {
        _topA.Header.gameObject.SetActive(false);
        _configureSet.gameObject.SetActive(false);

        UnsubscribeToButtons();
    }

    private void LoadConfigure()
    {
        SetPreview(_currentConfigureSet);
    }

    private void ConfigureNew()
    {
        _dataService.GetData().MySets.Add((0, 0, 0));
        SetPreview(_dataService.GetData().MySets.Count - 1);
    }

    private void SubscribeToButtons()
    {
        _configureSet.OnColorsSelect += ColorsSelect;
        _configureSet.OnBallsSelect += BallsSelect;
        _configureSet.OnMapsSelect += MapsSelect;

        _configureSet.OnNextSelect += NextItem;
        _configureSet.OnPrevSelect += PrevItem;

        _configureSet.SaveButton.onClick.AddListener(SaveSet);
    }

    private void UnsubscribeToButtons()
    {
        _configureSet.OnColorsSelect -= ColorsSelect;
        _configureSet.OnBallsSelect -= BallsSelect;
        _configureSet.OnMapsSelect -= MapsSelect;

        _configureSet.OnNextSelect -= NextItem;
        _configureSet.OnPrevSelect -= PrevItem;

        _configureSet.SaveButton.onClick.RemoveListener(SaveSet);
    }
    private void SaveSet()
    {
        Debug.Log($"Current Configure Set {_currentConfigureSet}");
        Debug.Log($"Save Set Ball {GetAvailableBallID(_currentBall)}, Background {GetAvailableColorID(_currentColor)}, Map {GetAvailableMapID(_currentMap)}");
        _dataService.GetData().MySets[_currentConfigureSet] = (
            _currentBall,
           _currentColor,
          _currentMap);
        _stateSwitcher.SwitchState<MySetsState>();
    }
    private async void ColorsSelect()
    {
        _currentItemType = ItemType.Color;

        _configureSet.ClearConfigureSetItems();

        var availableItems = _dataService.GetData().AvailableBackgrounds;

        ConfigureSetItem firstEmptySetItem = _configureSet.CreateConfigureSetItem();
        firstEmptySetItem.SetEmpty();
        _configureSet.ConfigureSetItems.Add(firstEmptySetItem);

        for (int i = 0; i < availableItems.Count; i++)
        {
            ConfigureSetItem configureSetItem = _configureSet.CreateConfigureSetItem();
            Sprite sprite = await Tretimi.Assets.GetAsset<Sprite>($"Background{availableItems[i]}");
            configureSetItem.SetColor(sprite);
            configureSetItem.ID = availableItems[i];
            _configureSet.ConfigureSetItems.Add(configureSetItem);
            _configureSet.AvailableSetItems.Add(configureSetItem);
        }

        ConfigureSetItem lastEmptySetItem = _configureSet.CreateConfigureSetItem();
        lastEmptySetItem.SetEmpty();
        _configureSet.ConfigureSetItems.Add(lastEmptySetItem);

        _configureSet.SetCurrentScrollItem(_currentColor);
        int id = GetAvailableColorID(_currentColor);
        _configureSet.CurrentSetName.text = _dataService.GetItemsData().Backgrounds[id].Name;
    }
    private async void BallsSelect()
    {
        _currentItemType = ItemType.Ball;

        _configureSet.ClearConfigureSetItems();

        var availableItems = _dataService.GetData().AvailableBalls;

        ConfigureSetItem firstEmptySetItem = _configureSet.CreateConfigureSetItem();
        firstEmptySetItem.SetEmpty();
        _configureSet.ConfigureSetItems.Add(firstEmptySetItem);

        for (int i = 0; i < availableItems.Count; i++)
        {
            ConfigureSetItem configureSetItem = _configureSet.CreateConfigureSetItem();
            Sprite sprite = await Tretimi.Assets.GetAsset<Sprite>($"ShopBall{availableItems[i]}");
            configureSetItem.SetBall(sprite);
            configureSetItem.ID = availableItems[i];
            _configureSet.ConfigureSetItems.Add(configureSetItem);
            _configureSet.AvailableSetItems.Add(configureSetItem);
        }

        ConfigureSetItem lastEmptySetItem = _configureSet.CreateConfigureSetItem();
        lastEmptySetItem.SetEmpty();
        _configureSet.ConfigureSetItems.Add(lastEmptySetItem);

        _configureSet.SetCurrentScrollItem(_currentBall);
        int id = GetAvailableBallID(_currentBall);
        _configureSet.CurrentSetName.text = _dataService.GetItemsData().Balls[id].Name;
    }

    private async void MapsSelect()
    {
        _currentItemType = ItemType.Map;

        _configureSet.ClearConfigureSetItems();

        var availableItems = _dataService.GetData().AvailableMaps;

        ConfigureSetItem firstEmptySetItem = _configureSet.CreateConfigureSetItem();
        firstEmptySetItem.SetEmpty();
        _configureSet.ConfigureSetItems.Add(firstEmptySetItem);

        for (int i = 0; i < availableItems.Count; i++)
        {
            ConfigureSetItem configureSetItem = _configureSet.CreateConfigureSetItem();
            Sprite sprite = await Tretimi.Assets.GetAsset<Sprite>($"Map{availableItems[i]}");
            configureSetItem.SetMap(sprite);
            configureSetItem.ID = availableItems[i];
            _configureSet.ConfigureSetItems.Add(configureSetItem);
            _configureSet.AvailableSetItems.Add(configureSetItem);
        }

        ConfigureSetItem lastEmptySetItem = _configureSet.CreateConfigureSetItem();
        lastEmptySetItem.SetEmpty();
        _configureSet.ConfigureSetItems.Add(lastEmptySetItem);

        _configureSet.SetCurrentScrollItem(_currentMap);
        int id = GetAvailableMapID(_currentMap);
        _configureSet.CurrentSetName.text = _dataService.GetItemsData().Maps[id].Name;
    }

    private void PrevItem()
    {
        int id;
        switch (_currentItemType)
        {
            case ItemType.Color:
                _currentColor--;
                id = GetAvailableColorID(_currentColor);
                _configureSet.CurrentSetName.text = _dataService.GetItemsData().Backgrounds[id].Name;
                break;
            case ItemType.Ball:
                _currentBall--;
                id = GetAvailableBallID(_currentBall);
                _configureSet.CurrentSetName.text = _dataService.GetItemsData().Balls[id].Name;
                break;
            case ItemType.Map:
                _currentMap--;
                id = GetAvailableMapID(_currentMap);
                _configureSet.CurrentSetName.text = _dataService.GetItemsData().Maps[id].Name;
                break;
        }
        UpdatePreview();
    }

    private void NextItem()
    {
        int id;
        switch (_currentItemType)
        {
            case ItemType.Color:
                _currentColor++;
                id = GetAvailableColorID(_currentColor);
                _configureSet.CurrentSetName.text = _dataService.GetItemsData().Backgrounds[id].Name;
                break;
            case ItemType.Ball:
                _currentBall++;
                id = GetAvailableBallID(_currentBall);
                _configureSet.CurrentSetName.text = _dataService.GetItemsData().Balls[id].Name;
                break;
            case ItemType.Map:
                _currentMap++;
                id = GetAvailableMapID(_currentMap);
                _configureSet.CurrentSetName.text = _dataService.GetItemsData().Maps[id].Name;
                break;
        }
        Debug.Log($"Current Color {_currentColor}");
        UpdatePreview();
    }
    private async void UpdatePreview()
    {
        _configureSet.PreviewBackground.sprite = await Tretimi.Assets.GetAsset<Sprite>(
            $"Background{GetAvailableColorID(_currentColor)}");

        _configureSet.PreviewBall.sprite = await Tretimi.Assets.GetAsset<Sprite>(
            $"Ball{GetAvailableBallID(_currentBall)}");

        _configureSet.PreviewMap.sprite = await Tretimi.Assets.GetAsset<Sprite>(
            $"Map{GetAvailableMapID(_currentMap)}");

        var itemsData = _dataService.GetItemsData();

        _configureSet.Color.Name.text = itemsData.Backgrounds[GetAvailableColorID(_currentColor)].Name;
        _configureSet.Ball.Name.text = itemsData.Balls[GetAvailableBallID(_currentBall)].Name;
        _configureSet.Map.Name.text = itemsData.Maps[GetAvailableMapID(_currentMap)].Name;
    }
    private void SetPreview(int index)
    {
        var mySets = _dataService.GetData().MySets;

        _currentColor = mySets[index].background;
        _currentBall = mySets[index].ball;
        _currentMap = mySets[index].map;

        Debug.Log($"Preview _currentColor {_currentColor}");
        Debug.Log($"Preview _currentBall {_currentBall}");
        Debug.Log($"Preview _currentMap {_currentMap}");

        UpdatePreview();
    }
}