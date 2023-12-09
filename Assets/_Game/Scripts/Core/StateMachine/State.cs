using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State
{
    protected readonly IStateSwitcher _stateSwitcher;
    protected readonly IDataService _dataService;
    protected readonly TopA _topA;
    protected State(IStateSwitcher stateSwitcher, IDataService dataService, TopA topA)
    {
        _stateSwitcher = stateSwitcher;
        _dataService = dataService;
        _topA = topA;
    }
    public virtual void Enter()
    {

    }
    public virtual void Exit()
    {

    }
}