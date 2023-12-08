using UnityEngine.UI;

public class ShopState : State
{
    private readonly ShopButtons _shopButtons;
    private readonly Shop _shop;
    public ShopState(IStateSwitcher stateSwitcher, TopA topA,
    ShopButtons shopButtons, Shop shop) : base(stateSwitcher, topA)
    {
        _shopButtons = shopButtons;
        _shop = shop;
    }
    public override void Enter()
    {

    }
    public override void Exit()
    {

    }
}
