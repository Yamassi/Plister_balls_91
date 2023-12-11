using System;
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
        Debug.Log($"Save Set Ball {_currentBall}, Background {_currentColor}, Map {_currentMap}");
        _dataService.GetData().MySets[_currentConfigureSet] = (_currentBall, _currentColor, _currentMap);
        _stateSwitcher.SwitchState<MySetsState>();
    }
    private async void ColorsSelect()
    {
        _currentItemType = ItemType.Background;

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
            _configureSet.ConfigureSetItems.Add(configureSetItem);
        }

        ConfigureSetItem lastEmptySetItem = _configureSet.CreateConfigureSetItem();
        lastEmptySetItem.SetEmpty();
        _configureSet.ConfigureSetItems.Add(lastEmptySetItem);

        _configureSet.SetCurrentScrollItem(_currentColor);
        _configureSet.CurrentSetName.text = _dataService.GetItemsData().Backgrounds[_currentColor].Name;
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
            _configureSet.ConfigureSetItems.Add(configureSetItem);
        }

        ConfigureSetItem lastEmptySetItem = _configureSet.CreateConfigureSetItem();
        lastEmptySetItem.SetEmpty();
        _configureSet.ConfigureSetItems.Add(lastEmptySetItem);

        _configureSet.SetCurrentScrollItem(_currentBall);
        _configureSet.CurrentSetName.text = _dataService.GetItemsData().Balls[_currentBall].Name;
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
            _configureSet.ConfigureSetItems.Add(configureSetItem);
        }

        ConfigureSetItem lastEmptySetItem = _configureSet.CreateConfigureSetItem();
        lastEmptySetItem.SetEmpty();
        _configureSet.ConfigureSetItems.Add(lastEmptySetItem);

        _configureSet.SetCurrentScrollItem(_currentMap);
        _configureSet.CurrentSetName.text = _dataService.GetItemsData().Maps[_currentMap].Name;
    }
    private void PrevItem()
    {
        switch (_currentItemType)
        {
            case ItemType.Background:
                _currentColor--;
                _configureSet.CurrentSetName.text = _dataService.GetItemsData().Backgrounds[_currentColor].Name;
                UpdatePreview();
                break;
            case ItemType.Ball:
                _currentBall--;
                _configureSet.CurrentSetName.text = _dataService.GetItemsData().Balls[_currentBall].Name;
                UpdatePreview();
                break;
            case ItemType.Map:
                _currentMap--;
                _configureSet.CurrentSetName.text = _dataService.GetItemsData().Maps[_currentMap].Name;
                UpdatePreview();
                break;
        }
    }

    private void NextItem()
    {
        switch (_currentItemType)
        {
            case ItemType.Background:
                _currentColor++;
                _configureSet.CurrentSetName.text = _dataService.GetItemsData().Backgrounds[_currentColor].Name;
                UpdatePreview();
                break;
            case ItemType.Ball:
                _currentBall++;
                _configureSet.CurrentSetName.text = _dataService.GetItemsData().Balls[_currentBall].Name;
                UpdatePreview();
                break;
            case ItemType.Map:
                _currentMap++;
                _configureSet.CurrentSetName.text = _dataService.GetItemsData().Maps[_currentMap].Name;
                UpdatePreview();
                break;
        }
    }
    private async void UpdatePreview()
    {
        _configureSet.PreviewBackground.sprite = await Tretimi.Assets.GetAsset<Sprite>(
            $"Background{_currentColor}");

        _configureSet.PreviewBall.sprite = await Tretimi.Assets.GetAsset<Sprite>(
            $"Ball{_currentBall}");

        _configureSet.PreviewMap.sprite = await Tretimi.Assets.GetAsset<Sprite>(
            $"Map{_currentMap}");
    }
    private async void SetPreview(int index)
    {
        var mySets = _dataService.GetData().MySets;

        _currentColor = mySets[index].background;
        _currentBall = mySets[index].ball;
        _currentMap = mySets[index].map;

        _configureSet.PreviewBackground.sprite = await Tretimi.Assets.GetAsset<Sprite>(
            $"Background{_currentColor}");

        _configureSet.PreviewBall.sprite = await Tretimi.Assets.GetAsset<Sprite>(
            $"Ball{_currentBall}");

        _configureSet.PreviewMap.sprite = await Tretimi.Assets.GetAsset<Sprite>(
            $"Map{_currentMap}");
    }
}