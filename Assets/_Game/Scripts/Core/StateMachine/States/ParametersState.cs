public class ConfigureDifficultyState : State
{
    private ConfigureDifficulty _configureDifficulty;
    public ConfigureDifficultyState(IStateSwitcher stateSwitcher,
    TopA topA, ConfigureDifficulty configureDifficulty) : base(stateSwitcher, topA)
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