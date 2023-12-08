public class GamePlayState : State
{
    private GamePlayUI _gamePlayUI;
    public GamePlayState(IStateSwitcher stateSwitcher,
    TopA topA, GamePlayUI gamePlayUI) : base(stateSwitcher, topA)
    {
        _gamePlayUI = gamePlayUI;
    }
    public override void Enter()
    {

    }
    public override void Exit()
    {

    }
}