public class SettingsState : State
{
    private Settings _settings;
    public SettingsState(IStateSwitcher stateSwitcher, IDataService dataService,
    TopA topA, Settings settings) : base(stateSwitcher, dataService, topA)
    {
        _settings = settings;
    }
    public override void Enter()
    {

    }
    public override void Exit()
    {

    }
}