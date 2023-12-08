public class SettingsState : State
{
    private Settings _settings;
    public SettingsState(IStateSwitcher stateSwitcher,
    TopA topA, Settings settings) : base(stateSwitcher, topA)
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