using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game
{
    private UIHolder _uIHolder;
    private StateMachine _stateMachine;
    public Game(UIHolder uIHolder)
    {
        _uIHolder = uIHolder;
    }
    public void Init()
    {
        _stateMachine = new StateMachine();
        _stateMachine.Init(new LoadingState(_uIHolder.Loading));
    }
}
