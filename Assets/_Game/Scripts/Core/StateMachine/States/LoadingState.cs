using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using UnityEngine;
using DG.Tweening;
public class LoadingState : State
{
    private readonly Loading _loadingPage;
    private Sequence _sequence;
    public LoadingState(IStateSwitcher stateSwitcher, IDataService dataService,
    TopA topA, Loading loadingPage) : base(stateSwitcher, dataService, topA)
    {
        _loadingPage = loadingPage;
    }
    public override void Enter()
    {
        _sequence = DOTween.Sequence();
        Loading();
    }
    public override void Exit()
    {
        _loadingPage.gameObject.SetActive(false);
        _sequence.Kill();
    }
    private async void Loading()
    {
        Vector3 punchPos = new Vector3(1.5f, 1.5f, 1.5f);

        _loadingPage.BallA.transform.localScale = Vector3.zero;
        _loadingPage.BallB.transform.localScale = Vector3.zero;
        _loadingPage.Logo.transform.localScale = Vector3.zero;
        _loadingPage.BackSquare.transform.localScale = Vector3.zero;
        _loadingPage.Coins.transform.localScale = Vector3.zero;
        _loadingPage.Gift.transform.localScale = Vector3.zero;

        _loadingPage.gameObject.SetActive(true);
        Debug.Log("Enter Loading Page ");

        _sequence.Append(_loadingPage.Logo.rectTransform.DOScale(Vector3.one, 0.3f));
        _sequence.Append(_loadingPage.BackSquare.rectTransform.DOScale(Vector3.one, 0.3f));
        _sequence.Append(_loadingPage.BallA.rectTransform.DOScale(Vector3.one, 0.5f));
        _sequence.Append(_loadingPage.Coins.rectTransform.DOScale(Vector3.one, 0.5f));
        _sequence.Append(_loadingPage.Gift.rectTransform.DOScale(Vector3.one, 0.5f));
        _sequence.Append(_loadingPage.BallB.rectTransform.DOScale(Vector3.one, 0.5f));



        await UniTask.Delay(3000);
        _stateSwitcher.SwitchState<MainMenuState>();
    }


}
