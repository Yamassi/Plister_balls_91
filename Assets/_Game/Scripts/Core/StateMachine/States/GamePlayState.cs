using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using DG.Tweening;
public class GamePlayState : State
{
    private IUIService _uIService;
    private GamePlayUI _gamePlayUI;
    private GamePlay _gamePlay;
    private int _currentBallID, _currentColorID, _currentMapID,
    _currentDifficultyID, _currentCost, _currentWeight;
    private Tween _tween;
    private bool _isFrozen;
    public GamePlayState(IStateSwitcher stateSwitcher, IDataService dataService, IUIService uIService,
    TopA topA, GamePlayUI gamePlayUI, GamePlay gamePlay) : base(stateSwitcher, dataService, topA)
    {
        _uIService = uIService;
        _gamePlayUI = gamePlayUI;
        _gamePlay = gamePlay;
    }
    public override void Enter()
    {
        _topA.Coins.gameObject.SetActive(true);
        _topA.BackButton.gameObject.SetActive(true);
        _topA.SettingsButton.gameObject.SetActive(true);
        _gamePlayUI.gameObject.SetActive(true);
        _gamePlay.gameObject.SetActive(true);

        PlayGame();
        SubscribeToButtons();
    }

    public override void Exit()
    {
        _topA.Coins.gameObject.SetActive(false);
        _topA.BackButton.gameObject.SetActive(false);
        _topA.SettingsButton.gameObject.SetActive(false);
        _gamePlayUI.gameObject.SetActive(false);
        _gamePlay.gameObject.SetActive(false);

        UnsubscribeToButtons();
        _tween?.Kill();
        _gamePlay.ClearGamePlay();
    }

    private void SubscribeToButtons()
    {
        _topA.BackButton.onClick.AddListener(GoToSelectSet);
        _gamePlayUI.Parameters.onClick.AddListener(GoToParameters);
        _gamePlayUI.Play.onClick.AddListener(LaunchBall);
        _gamePlay.OnBallFall += BallFallToXSlot;
    }

    private void UnsubscribeToButtons()
    {
        _topA.BackButton.onClick.RemoveListener(GoToSelectSet);
        _gamePlayUI.Parameters.onClick.RemoveListener(GoToParameters);
        _gamePlayUI.Play.onClick.RemoveListener(LaunchBall);
        _gamePlay.OnBallFall -= BallFallToXSlot;

        if (_tween != null)
            _tween.onComplete -= ResetCoins;
    }

    private void GoToParameters()
    {
        _stateSwitcher.SwitchState<ConfigureDifficultyState>();
    }

    private async void BallFallToXSlot(float coefficient)
    {
        if (coefficient < 1)
        {
            _topA.Coins.ArrowDown.gameObject.SetActive(true);
            _tween = _topA.Coins.ArrowDown.rectTransform.DOShakeScale(0.49f);
            await UniTask.Delay(500);

            _topA.Coins.ArrowDown.gameObject.SetActive(false);
            Debug.Log("Lose Score " + (int)(_currentCost * coefficient));
        }

        if (coefficient > 1)
        {
            _topA.Coins.ArrowUp.gameObject.SetActive(true);
            _tween = _topA.Coins.ArrowUp.rectTransform.DOShakeScale(0.49f);
            await UniTask.Delay(500);
            _topA.Coins.ArrowUp.gameObject.SetActive(false);
            Debug.Log("Win Score " + (int)(_currentCost * coefficient));
        }

        _dataService.AddCoins((int)(_currentCost * coefficient));
        _tween = _topA.Coins.CoinsText.rectTransform.DOShakeScale(0.15f);
        _tween.onComplete += ResetCoins;
        _uIService.UpdateUI();
    }

    private void ResetCoins()
    {
        _topA.Coins.CoinsText.rectTransform.localScale = Vector3.one;
    }

    private async void LaunchBall()
    {
        if (!_isFrozen)
        {
            _gamePlay.LaunchBall();
            _dataService.RemoveCoins(_currentCost);
            _uIService.UpdateUI();
            _isFrozen = true;

            await UniTask.Delay(1000);
            _isFrozen = false;
        }

    }

    private void GoToSelectSet()
    {
        _stateSwitcher.SwitchState<SelectSetState>();
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
}