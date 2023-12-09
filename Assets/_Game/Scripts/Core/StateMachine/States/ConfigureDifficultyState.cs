public class ConfigureDifficultyState : State
{
    private ConfigureDifficulty _configureDifficulty;
    public ConfigureDifficultyState(IStateSwitcher stateSwitcher,
    IDataService dataService, TopA topA,
    ConfigureDifficulty configureDifficulty) : base(stateSwitcher, dataService, topA)
    {
        _configureDifficulty = configureDifficulty;
    }
    public override void Enter()
    {

    }
    public override void Exit()
    {

    }
}