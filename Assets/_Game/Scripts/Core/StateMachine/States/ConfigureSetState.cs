public class ConfigureSetState : State
{
    private ConfigureSet _configureSet;
    public ConfigureSetState(IStateSwitcher stateSwitcher, IDataService dataService,
    TopA topA, ConfigureSet configureSet) : base(stateSwitcher, dataService, topA)
    {
        _configureSet = configureSet;
    }
    public override void Enter()
    {

    }
    public override void Exit()
    {

    }
}