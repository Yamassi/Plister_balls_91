using UnityEngine.UI;

public class MainMenuState : State
{
    private Coins _coins;
    private Header _header;
    private MainMenu _mainMenu;
    public MainMenuState(Coins coins, Header header, MainMenu mainMenu)
    {
        _coins = coins;
        _header = header;
        _mainMenu = mainMenu;
    }
    public override void Enter()
    {
        _coins.gameObject.SetActive(true);
        _header.gameObject.SetActive(true);
        _mainMenu.gameObject.SetActive(true);
    }
    public override void Exit()
    {
        _coins.gameObject.SetActive(false);
        _header.gameObject.SetActive(false);
        _mainMenu.gameObject.SetActive(false);
    }
}