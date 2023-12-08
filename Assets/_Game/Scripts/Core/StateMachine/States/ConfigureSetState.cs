public class ConfigureSetState : State
{
    private ConfigureSet _configureSet;
    public ConfigureSetState(IStateSwitcher stateSwitcher,
    TopA topA, ConfigureSet configureSet) : base(stateSwitcher, topA)
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