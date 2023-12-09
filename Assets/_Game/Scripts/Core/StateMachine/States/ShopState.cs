using System;
using UnityEngine.UI;

public class ShopState : State
{
    private readonly ShopButtons _shopButtons;
    private readonly Shop _shop;
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
    }

    public override void Exit()
    {
        _topA.Coins.gameObject.SetActive(false);
        _topA.BackButton.gameObject.SetActive(false);
        _topA.SettingsButton.gameObject.SetActive(false);
        _shopButtons.gameObject.SetActive(false);
        _shop.gameObject.SetActive(false);
        _topA.BackButton.onClick.RemoveListener(BackToMainMenu);
    }
    private void BackToMainMenu()
    {
        _stateSwitcher.SwitchState<MainMenuState>();
    }
}
