using UnityEngine.UI;

public class MainMenuState : State
{
    private readonly MainMenu _mainMenu;
    public MainMenuState(IStateSwitcher stateSwitcher,
    TopA topA, MainMenu mainMenu) : base(stateSwitcher, topA)
    {
        _mainMenu = mainMenu;
    }
    public override void Enter()
    {
        _topA.Coins.gameObject.SetActive(true);
        _topA.Header.gameObject.SetActive(true);
        _mainMenu.gameObject.SetActive(true);
    }
    public override void Exit()
    {
        _topA.Coins.gameObject.SetActive(false);
        _topA.Header.gameObject.SetActive(false);
        _mainMenu.gameObject.SetActive(false);
    }
}