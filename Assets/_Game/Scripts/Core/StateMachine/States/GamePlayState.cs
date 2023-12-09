public class GamePlayState : State
{
    private GamePlayUI _gamePlayUI;
    private GamePlay _gamePlay;
    public GamePlayState(IStateSwitcher stateSwitcher,
    TopA topA, GamePlayUI gamePlayUI, GamePlay gamePlay) : base(stateSwitcher, topA)
    {
        _gamePlayUI = gamePlayUI;
        _gamePlay = gamePlay;
    }
    public override void Enter()
    {

    }
    public override void Exit()
    {

    }
}