public class SelectSetState : State
{

    private SelectSet _selectSet;
    public SelectSetState(IStateSwitcher stateSwitcher, TopA topA,
    SelectSet selectSet) : base(stateSwitcher, topA)
    {
        _selectSet = selectSet;
    }
    public override void Enter()
    {

    }
    public override void Exit()
    {

    }
}