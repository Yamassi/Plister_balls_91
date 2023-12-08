public class LoadingState : State
{
    private Loading _loadingPage;
    public LoadingState(Loading loadingPage)
    {
        _loadingPage = loadingPage;
    }
    public override void Enter()
    {
        _loadingPage.gameObject.SetActive(true);
    }
    public override void Exit()
    {
        _loadingPage.gameObject.SetActive(false);
    }
}
