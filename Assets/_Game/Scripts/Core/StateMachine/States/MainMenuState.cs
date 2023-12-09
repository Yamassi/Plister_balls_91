using System;
using UnityEngine.UI;

public class MainMenuState : State
{
    private readonly MainMenu _mainMenu;
    private readonly TopB _topB;
    public MainMenuState(IStateSwitcher stateSwitcher,
    TopA topA, TopB topB, MainMenu mainMenu) : base(stateSwitcher, topA)
    {
        _topB = topB;
        _mainMenu = mainMenu;
    }
    public override void Enter()
    {
        _topA.Coins.gameObject.SetActive(true);
        _topB.Header.gameObject.SetActive(true);
        _mainMenu.gameObject.SetActive(true);

        _topB.Header.HeaderText.text = "DAILY MINI GAME";

        _mainMenu.MySets.onClick.AddListener(SwitchToMySets);
        _mainMenu.Shop.onClick.AddListener(SwitchToShop);
        _mainMenu.Play.onClick.AddListener(SwitchToSelectSet);
    }
    public override void Exit()
    {
        _topA.Coins.gameObject.SetActive(false);
        _topB.Header.gameObject.SetActive(false);
        _mainMenu.gameObject.SetActive(false);

        _mainMenu.MySets.onClick.RemoveListener(SwitchToMySets);
        _mainMenu.Shop.onClick.RemoveListener(SwitchToShop);
        _mainMenu.Play.onClick.RemoveListener(SwitchToSelectSet);
    }
    private void SwitchToSelectSet()
    {
        _stateSwitcher.SwitchState<SelectSetState>();
    }

    private void SwitchToShop()
    {
        _stateSwitcher.SwitchState<ShopState>();
    }


    private void SwitchToMySets()
    {
        _stateSwitcher.SwitchState<MySetsState>();
    }


}