using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State
{
    protected readonly IStateSwitcher _stateSwitcher;
    protected readonly TopA _topA;
    protected State(IStateSwitcher stateSwitcher, TopA topA)
    {
        _stateSwitcher = stateSwitcher;
        _topA = topA;
    }
    public virtual void Enter()
    {

    }
    public virtual void Exit()
    {

    }
}