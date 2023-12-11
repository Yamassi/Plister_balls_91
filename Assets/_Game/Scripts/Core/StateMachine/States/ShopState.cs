using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tretimi;
using UnityEngine;
using UnityEngine.UI;

public class ShopState : State
{
    private readonly ShopButtons _shopButtons;
    private readonly Shop _shop;
    private ItemType _currentItemsType;
    public ShopState(IStateSwitcher stateSwitcher, IDataService dataService,
    TopA topA, ShopButtons shopButtons, Shop shop) : base(stateSwitcher, dataService, topA)
    {
        _shopButtons = shopButtons;
        _shop = shop;
    }
    public override void Enter()
    {
        _topA.Coins.gameObject.SetActive(true);
        _topA.BackButton.gameObject.SetActive(true);
        _topA.SettingsButton.gameObject.SetActive(true);
        _shopButtons.gameObject.SetActive(true);
        _shop.gameObject.SetActive(true);

        _topA.BackButton.onClick.AddListener(BackToMainMenu);

        _shopButtons.BallButton.Button.onClick.AddListener(OpenBallShop);
        _shopButtons.BackgroundButton.Button.onClick.AddListener(OpenBackgroundShop);
        _shopButtons.MapButton.Button.onClick.AddListener(OpenMapShop);
        _shopButtons.CoinsButton.Button.onClick.AddListener(OpenCoinsShop);

        OpenBallShop();
        SubcribeToButtons();
    }

    public override void Exit()
    {
        _topA.Coins.gameObject.SetActive(false);
        _topA.BackButton.gameObject.SetActive(false);
        _topA.SettingsButton.gameObject.SetActive(false);
        _shopButtons.gameObject.SetActive(false);
        _shop.gameObject.SetActive(false);

        _topA.BackButton.onClick.RemoveListener(BackToMainMenu);

        _shopButtons.BallButton.Button.onClick.RemoveListener(OpenBallShop);
        _shopButtons.BackgroundButton.Button.onClick.RemoveListener(OpenBackgroundShop);
        _shopButtons.MapButton.Button.onClick.RemoveListener(OpenMapShop);
        _shopButtons.CoinsButton.Button.onClick.RemoveListener(OpenCoinsShop);
    }


    private void SubcribeToButtons()
    {
        for (int i = 0; i < _shop.ShopItems.Count; i++)
        {
            _shop.ShopItems[i].OnItemTryToBuy += TryToBuy;
        }
    }

    private void TryToBuy(int id)
    {
        int coins = _dataService.GetData().Coins;
        List<int> availableItemsData;
        int price;

        switch (_currentItemsType)
        {
            case ItemType.Color:
                price = _dataService.GetItemsData().Backgrounds[id].Price;
                availableItemsData = _dataService.GetData().AvailableBackgrounds;
                break;
            case ItemType.Map:
                price = _dataService.GetItemsData().Maps[id].Price;
                availableItemsData = _dataService.GetData().AvailableMaps;
                break;
            default:
                price = _dataService.GetItemsData().Balls[id].Price;
                availableItemsData = _dataService.GetData().AvailableBalls;
                break;
        }

        if (coins >= price)
        {
            Debug.Log($"Buying Item {id} in {availableItemsData}");
            _dataService.GetData().Coins -= price;
            availableItemsData.Add(id);

            _dataService.UpdateUI();

            _shop.ShopItems[id].Received();
        }
        else
        {
            _shop.ShopItems[id].NotEnoughMoney();
        }

    }

    private void OpenCoinsShop()
    {
        _shop.ShopCoinsList.gameObject.SetActive(true);
        _shop.ShopItemsPoint.gameObject.SetActive(false);

        _shopButtons.BallButton.Deactivate();
        _shopButtons.BackgroundButton.Deactivate();
        _shopButtons.MapButton.Deactivate();
        _shopButtons.CoinsButton.Activate();
    }

    private void OpenMapShop()
    {
        _currentItemsType = ItemType.Map;

        _shop.ShopCoinsList.gameObject.SetActive(false);
        _shop.ShopItemsPoint.gameObject.SetActive(true);

        _shopButtons.BallButton.Deactivate();
        _shopButtons.BackgroundButton.Deactivate();
        _shopButtons.MapButton.Activate();
        _shopButtons.CoinsButton.Deactivate();


        List<ShopItemSO> itemsSO = _dataService.GetItemsData().Maps;
        List<int> availableItems = _dataService.GetData().AvailableMaps;
        UpdateList(itemsSO, availableItems, ItemType.Map);
    }

    private void OpenBackgroundShop()
    {
        _currentItemsType = ItemType.Color;

        _shop.ShopCoinsList.gameObject.SetActive(false);
        _shop.ShopItemsPoint.gameObject.SetActive(true);

        _shopButtons.BallButton.Deactivate();
        _shopButtons.BackgroundButton.Activate();
        _shopButtons.MapButton.Deactivate();
        _shopButtons.CoinsButton.Deactivate();

        List<ShopItemSO> itemsSO = _dataService.GetItemsData().Backgrounds;
        List<int> availableItems = _dataService.GetData().AvailableBackgrounds;
        UpdateList(itemsSO, availableItems, ItemType.Color);
    }

    private void OpenBallShop()
    {
        _currentItemsType = ItemType.Ball;

        _shop.ShopCoinsList.gameObject.SetActive(false);
        _shop.ShopItemsPoint.gameObject.SetActive(true);

        _shopButtons.BallButton.Activate();
        _shopButtons.BackgroundButton.Deactivate();
        _shopButtons.MapButton.Deactivate();
        _shopButtons.CoinsButton.Deactivate();

        List<ShopItemSO> itemsSO = _dataService.GetItemsData().Balls;
        List<int> availableItems = _dataService.GetData().AvailableBalls;
        UpdateList(itemsSO, availableItems, ItemType.Ball);
    }

    private async void UpdateList(List<ShopItemSO> itemsSO, List<int> availableItems, ItemType itemType)
    {
        string spriteName = itemType switch
        {
            ItemType.Color => "Background",
            ItemType.Map => "Map",
            _ => "ShopBall",
        };

        for (int i = 0; i < _shop.ShopItems.Count; i++)
        {
            Sprite sprite = await Tretimi.Assets.GetAsset<Sprite>($"{spriteName}{itemsSO[i].ID}");

            switch (itemType)
            {
                case ItemType.Ball:
                    _shop.ShopItems[i].SetImage(sprite);
                    break;
                case ItemType.Color:
                    _shop.ShopItems[i].SetFullImage(sprite);
                    break;
                case ItemType.Map:
                    _shop.ShopItems[i].SetMapImage(sprite);
                    break;
            }

            _shop.ShopItems[i].SetName(itemsSO[i].Name);

            if (availableItems.Contains(i))
                _shop.ShopItems[i].Received();
            else
                _shop.ShopItems[i].SetPrice(itemsSO[i].Price.ToString());
        }
    }

    private void BackToMainMenu()
    {
        _stateSwitcher.SwitchState<MainMenuState>();
    }
}

public enum ItemType
{
    Ball,
    Color,
    Map,
}
