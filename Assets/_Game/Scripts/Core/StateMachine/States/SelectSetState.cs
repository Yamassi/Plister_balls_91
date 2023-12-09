public class SelectSetState : State
{
    private TopB _topB;
    private SelectSet _selectSet;
    public SelectSetState(IStateSwitcher stateSwitcher, IDataService dataService, TopA topA,
   TopB topB, SelectSet selectSet) : base(stateSwitcher, dataService, topA)
    {
        _topB = topB;
        _selectSet = selectSet;
    }
    public override void Enter()
    {
        _topA.Coins.gameObject.SetActive(true);
        _topA.BackButton.gameObject.SetActive(true);
        _topA.SettingsButton.gameObject.SetActive(true);
        _topB.Header.gameObject.SetActive(true);
        _selectSet.gameObject.SetActive(true);

        _topB.Header.HeaderText.text = "CHOOSE A SET";

        _topA.BackButton.onClick.AddListener(BackToMainMenu);
    }
    public override void Exit()
    {
        _topA.Coins.gameObject.SetActive(false);
        _topA.BackButton.gameObject.SetActive(false);
        _topA.SettingsButton.gameObject.SetActive(false);
        _topB.Header.gameObject.SetActive(false);
        _selectSet.gameObject.SetActive(false);

        _topA.BackButton.onClick.RemoveListener(BackToMainMenu);
    }

    private void BackToMainMenu()
    {
        _stateSwitcher.SwitchState<MainMenuState>();
    }
}