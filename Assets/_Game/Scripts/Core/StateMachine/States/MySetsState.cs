public class MySetsState : State
{
    private MySets _mySets;
    public MySetsState(IStateSwitcher stateSwitcher,
    TopA topA, MySets mySets) : base(stateSwitcher, topA)
    {
        _mySets = mySets;
    }
    public override void Enter()
    {

    }
    public override void Exit()
    {

    }
}