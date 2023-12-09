public class GamePlayState : State
{
    private GamePlayUI _gamePlayUI;
    private GamePlay _gamePlay;
    public GamePlayState(IStateSwitcher stateSwitcher, IDataService dataService,
    TopA topA, GamePlayUI gamePlayUI, GamePlay gamePlay) : base(stateSwitcher, dataService, topA)
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