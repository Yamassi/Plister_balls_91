public class MySetsState : State
{
    private MySets _mySets;
    public MySetsState(IStateSwitcher stateSwitcher,
    TopA topA, MySets mySets) : base(stateSwitcher, topA)
    {
        _mySets = mySets;
    }
    public override void Enter()
    {
        _topA.Coins.gameObject.SetActive(true);
        _topA.BackButton.gameObject.SetActive(true);
        _topA.SettingsButton.gameObject.SetActive(true);
        _mySets.gameObject.SetActive(true);

        _topA.BackButton.onClick.AddListener(BackToMainMenu);
    }
    public override void Exit()
    {
        _topA.Coins.gameObject.SetActive(false);
        _topA.BackButton.gameObject.SetActive(false);
        _topA.SettingsButton.gameObject.SetActive(false);
        _mySets.gameObject.SetActive(false);

        _topA.BackButton.onClick.RemoveListener(BackToMainMenu);
    }

    private void BackToMainMenu()
    {
        _stateSwitcher.SwitchState<MainMenuState>();
    }
}