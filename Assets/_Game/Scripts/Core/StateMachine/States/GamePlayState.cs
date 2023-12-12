using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using DG.Tweening;
using System.Threading;
public class GamePlayState : State
{
    private IUIService _uIService;
    private GamePlayUI _gamePlayUI;
    private GamePlay _gamePlay;
    private int _currentBallID, _currentColorID, _currentMapID,
    _currentDifficultyID, _currentCost, _currentWeight;
    private Tween _tweenArrow, _tweenText;
    private bool _isFrozen;
    private CancellationTokenSource _cts, _cts2, _cts3;
    public GamePlayState(IStateSwitcher stateSwitcher, IDataService dataService, IUIService uIService,
    TopA topA, GamePlayUI gamePlayUI, GamePlay gamePlay) : base(stateSwitcher, dataService, topA)
    {
        _uIService = uIService;
        _gamePlayUI = gamePlayUI;
        _gamePlay = gamePlay;
    }
    public override void Enter()
    {
        Debug.Log("Enter GamePlay State");
        _topA.Coins.gameObject.SetActive(true);
        _topA.BackButton.gameObject.SetActive(true);
        _topA.SettingsButton.gameObject.SetActive(true);
        _gamePlayUI.gameObject.SetActive(true);
        _gamePlay.gameObject.SetActive(true);

        ResetLocalData();
        PlayGame();
        SubscribeToButtons();
    }

    public override void Exit()
    {
        Debug.Log("Exit GamePlay State");
        _topA.Coins.gameObject.SetActive(false);
        _topA.BackButton.gameObject.SetActive(false);
        _topA.SettingsButton.gameObject.SetActive(false);
        _gamePlayUI.gameObject.SetActive(false);
        _gamePlay.gameObject.SetActive(false);

        _topA.Coins.ArrowDown.gameObject.SetActive(false);
        _topA.Coins.ArrowUp.gameObject.SetActive(false);

        UnsubscribeToButtons();
        _tweenArrow?.Kill();
        _tweenText?.Kill();
        _gamePlay.ClearGamePlay();

        _cts.Cancel();
        _cts2.Cancel();
        _cts3.Cancel();

        _cts.Dispose();
        _cts2.Dispose();
        _cts3.Dispose();
    }

    private void SubscribeToButtons()
    {
        _topA.BackButton.onClick.AddListener(GoToSelectSet);
        _topA.SettingsButton.onClick.AddListener(GoToSettings);
        _gamePlayUI.Parameters.onClick.AddListener(GoToParameters);
        _gamePlayUI.Play.onClick.AddListener(LaunchBall);
        _gamePlay.OnBallFall += BallFallToXSlot;
    }

    private void UnsubscribeToButtons()
    {
        _topA.BackButton.onClick.RemoveListener(GoToSelectSet);
        _topA.SettingsButton.onClick.RemoveListener(GoToSettings);
        _gamePlayUI.Parameters.onClick.RemoveListener(GoToParameters);
        _gamePlayUI.Play.onClick.RemoveListener(LaunchBall);
        _gamePlay.OnBallFall -= BallFallToXSlot;
    }

    private async void BallFallToXSlot(float coefficient)
    {
        if (coefficient < 1)
        {
            _topA.Coins.ArrowDown.gameObject.SetActive(true);
            _tweenArrow = _topA.Coins.ArrowDown.rectTransform.DOShakeScale(0.49f).OnComplete(ResetArrow);
            await UniTask.Delay(500, cancellationToken: _cts.Token);

            _topA.Coins.ArrowDown.gameObject.SetActive(false);
            Debug.Log("Lose Score " + (int)(_currentCost * coefficient));
            AudioSystem.Instance.LoseSound();
        }

        if (coefficient > 1)
        {
            _topA.Coins.ArrowUp.gameObject.SetActive(true);
            _tweenArrow = _topA.Coins.ArrowUp.rectTransform.DOShakeScale(0.49f).OnComplete(ResetArrow);
            await UniTask.Delay(500, cancellationToken: _cts2.Token);
            ResetArrow();

            _topA.Coins.ArrowUp.gameObject.SetActive(false);
            Debug.Log("Win Score " + (int)(_currentCost * coefficient));
            AudioSystem.Instance.WinSound();
        }

        _dataService.AddCoins((int)(_currentCost * coefficient));
        _tweenText = _topA.Coins.CoinsText.rectTransform.DOShakeScale(0.15f).OnComplete(ResetCoins);
        await UniTask.Delay(150);

        _uIService.UpdateUI();
    }

    private void ResetCoins()
    {
        _topA.Coins.CoinsText.rectTransform.localScale = Vector3.one;
    }
    private void ResetArrow()
    {
        _topA.Coins.ArrowUp.rectTransform.localScale = Vector3.one;
        _topA.Coins.ArrowDown.rectTransform.localScale = Vector3.one;
    }
    private async void LaunchBall()
    {
        if (!_isFrozen && _dataService.GetData().Coins >= _currentCost)
        {
            _gamePlay.LaunchBall();
            _dataService.RemoveCoins(_currentCost);
            _uIService.UpdateUI();
            _isFrozen = true;

            await UniTask.Delay(1000, cancellationToken: _cts3.Token);
            _isFrozen = false;
        }

    }
    private async void PlayGame()
    {
        int currentSetID = PlayerPrefs.GetInt("CurrentSet");

        LoadData(currentSetID);

        UpdateUI();

        await _gamePlay.SetMap(_currentMapID, _currentDifficultyID);
        await _gamePlay.SetBall(_currentBallID, _currentWeight);
        _gamePlay.CreateBall();
    }

    private void ResetLocalData()
    {
        _cts = new();
        _cts2 = new();
        _cts3 = new();

        _isFrozen = false;
    }

    private void LoadData(int currentSetID)
    {
        _currentBallID = GetAvailableBallID(_dataService.GetData().MySets[currentSetID].ball);
        _currentMapID = GetAvailableMapID(_dataService.GetData().MySets[currentSetID].map);
        _currentColorID = GetAvailableColorID(_dataService.GetData().MySets[currentSetID].background);

        _currentDifficultyID = PlayerPrefs.GetInt("CurrentDifficulty");
        _currentWeight = PlayerPrefs.GetInt("CurrentWeight");
        _currentCost = PlayerPrefs.GetInt("CurrentCost");

        if (_currentCost == 0)
        {
            _currentCost = 10;
            PlayerPrefs.SetInt("CurrentCost", _currentCost);
        }
        if (_currentWeight == 0)
        {
            _currentWeight = 10;
            PlayerPrefs.SetInt("CurrentWeight", _currentWeight);
        }
    }

    private void UpdateUI()
    {
        _gamePlayUI.CurrentCost.text = _currentCost.ToString();
        _gamePlayUI.CurrentDifficulty.text = (_currentDifficultyID + 1).ToString();
        _gamePlayUI.CurrentWeight.text = _currentWeight.ToString();
        _uIService.ChangeBackground(_currentColorID);
    }
    private void GoToSelectSet()
    {
        _stateSwitcher.SwitchState<SelectSetState>();
    }
    private void GoToParameters()
    {
        PlayerPrefs.SetInt("CurrentMap", _currentMapID);
        _stateSwitcher.SwitchState<ConfigureDifficultyState>();
    }
    private void GoToSettings()
    {
        PlayerPrefs.SetString("LastPage", "GamePlayState");
        _stateSwitcher.SwitchState<SettingsState>();
    }
}