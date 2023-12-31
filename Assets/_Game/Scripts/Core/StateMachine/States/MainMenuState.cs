using System;
using UnityEngine.UI;
using UnityEngine;
public class MainMenuState : State
{
    private readonly IUIService _uIService;
    private readonly MainMenu _mainMenu;
    private readonly TopB _topB;
    private GiftsCore _giftsCore;
    public MainMenuState(IStateSwitcher stateSwitcher,
    IDataService dataService, IUIService uIService, TopA topA, TopB topB,
    MainMenu mainMenu) : base(stateSwitcher, dataService, topA)
    {
        _uIService = uIService;
        _topB = topB;
        _mainMenu = mainMenu;
    }
    public override void Enter()
    {
        Debug.Log("Enter Main Menu State");
        _topA.Coins.gameObject.SetActive(true);
        _topB.Header.gameObject.SetActive(true);
        _mainMenu.gameObject.SetActive(true);

        _topB.Header.HeaderText.text = "DAILY MINI GAME";

        _mainMenu.MySets.onClick.AddListener(SwitchToMySets);
        _mainMenu.Shop.onClick.AddListener(SwitchToShop);
        _mainMenu.Play.onClick.AddListener(SwitchToSelectSet);

        if (_giftsCore == null)
            _giftsCore = new(_mainMenu.Gifts, _dataService, _uIService);

        _giftsCore.Init();
    }
    public override void Exit()
    {
        Debug.Log("Exit Main Menu State");
        _topA.Coins.gameObject.SetActive(false);
        _topB.Header.gameObject.SetActive(false);
        _mainMenu.gameObject.SetActive(false);

        _mainMenu.MySets.onClick.RemoveListener(SwitchToMySets);
        _mainMenu.Shop.onClick.RemoveListener(SwitchToShop);
        _mainMenu.Play.onClick.RemoveListener(SwitchToSelectSet);

        _giftsCore.DeInit();
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