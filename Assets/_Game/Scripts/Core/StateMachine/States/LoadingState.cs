using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class LoadingState : State
{
    private readonly Loading _loadingPage;
    public LoadingState(IStateSwitcher stateSwitcher,
    TopA topA, Loading loadingPage) : base(stateSwitcher, topA)
    {
        _loadingPage = loadingPage;
    }
    public override void Enter()
    {
        Loading();
    }
    public override void Exit()
    {
        _loadingPage.gameObject.SetActive(false);
    }
    private async void Loading()
    {
        _loadingPage.gameObject.SetActive(true);
        Debug.Log("Enter Loading Page ");

        await UniTask.Delay(3000);
        _stateSwitcher.SwitchState<MainMenuState>();
    }


}
