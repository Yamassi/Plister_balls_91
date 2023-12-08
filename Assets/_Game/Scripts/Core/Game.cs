using System.Collections.Generic;
using System.Linq;

public class Game : IStateSwitcher
{
    private UIHolder _uIHolder;
    private StateMachine _stateMachine;
    private List<State> _allStates;
    private State _currentState;

    public Game(UIHolder uIHolder)
    {
        _uIHolder = uIHolder;
    }
    public void Init()
    {
        _stateMachine = new StateMachine();

        _allStates = new List<State>(){
            new LoadingState(_uIHolder.Loading),
            new MainMenuState(_uIHolder.TopA.Coins,_uIHolder.TopB.Header,_uIHolder.BottomB.MainMenu),
            new ShopState(),
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
