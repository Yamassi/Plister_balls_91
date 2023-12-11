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

    protected int GetAvailableColorID(int index)
    {
        return _dataService.GetData().AvailableBackgrounds[index];
    }
    protected int GetAvailableBallID(int index)
    {
        return _dataService.GetData().AvailableBalls[index];
    }
    protected int GetAvailableMapID(int index)
    {
        return _dataService.GetData().AvailableMaps[index];
    }
}