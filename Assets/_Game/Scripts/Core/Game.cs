using System.Collections.Generic;
using System.Linq;

public class Game : IStateSwitcher
{
    private UIHolder _uIHolder;
    private GamePlay _gamePlay;
    private StateMachine _stateMachine;
    private List<State> _allStates;
    private State _currentState;

    public Game(UIHolder uIHolder, GamePlay gamePlay)
    {
        _uIHolder = uIHolder;
        _gamePlay = gamePlay;
    }
    public void Init()
    {
        _stateMachine = new StateMachine();

        _allStates = new List<State>(){
            new LoadingState(this,_uIHolder.TopA,
            _uIHolder.Loading),

            new MainMenuState(this,_uIHolder.TopA,
            _uIHolder.TopB,_uIHolder.BottomB.MainMenu),

            new ShopState(this, _uIHolder.TopA,
            _uIHolder.TopB.ShopButtons, _uIHolder.BottomB.Shop),

            new MySetsState(this, _uIHolder.TopA,
            _uIHolder.BottomA.MySets),

            new SelectSetState(this, _uIHolder.TopA,
           _uIHolder.TopB, _uIHolder.BottomB.SelectSet),

            new ConfigureSetState(this, _uIHolder.TopA,
            _uIHolder.BottomA.ConfigureSet),

            new ConfigureDifficultyState(this,_uIHolder.TopA,
            _uIHolder.BottomA.ConfigureDifficulty),

            new GamePlayState(this,_uIHolder.TopA,
            _uIHolder.BottomA.GamePlayUI, _gamePlay),

            new SettingsState(this,_uIHolder.TopA,
            _uIHolder.BottomA.Settings),
        };

        _currentState = _allStates[0];

        _stateMachine.Init(_currentState);
    }

    public void SwitchState<T>() where T : State
    {
        var state = _allStates.FirstOrDefault(s => s is T);
        _stateMachine.ChangeState(state);
    }
}

public interface IStateSwitcher
{
    void SwitchState<T>() where T : State;
}
