using System;
using System.Collections.Generic;
using System.Linq;
using Tretimi;
using UnityEngine;
using UniRx;
public class GiftsCore
{
    private readonly Gifts _gifts;
    private List<(GiftType giftType, int id)> _openGifts = new(4);
    private bool _isGiftsAvailable = false;
    private float _itemDropChance = 20;
    private readonly IDataService _dataHolder;
    private CompositeDisposable _disposable = new();
    public GiftsCore(Gifts gifts, IDataService dataHolder)
    {
        _gifts = gifts;
        _dataHolder = dataHolder;
    }
    public void Init()
    {
        DateTime timeToOpenGift = UITools.Timer.ConvertStringToDateTime(_dataHolder.GetData().TimeToOpenGift);
        DateTime currentTime = DateTime.Now;

        if (currentTime > timeToOpenGift)
        {
            Debug.Log($"Time to open gift - {timeToOpenGift}");
            Debug.Log($"Current Time - {currentTime}");
            _isGiftsAvailable = true;
            _openGifts = new(4);

            _gifts.GetButton.gameObject.SetActive(false);
            _gifts.TextBox.Text.text = "0/3";
        }
        else
        {
            Debug.LogError($"Time to open gift NOT COME - {timeToOpenGift}");
            Debug.LogError($"Current Time - {currentTime}");
            _isGiftsAvailable = false;
            StartTimer();
        }

        ResetGifts();

        if (_isGiftsAvailable)
            SubcribeToGitfsClick();
    }

    public void DeInit()
    {
        if (_isGiftsAvailable)
            UnsubcribeToGitfsClicks();

        _disposable.Clear();
    }

    private void SubcribeToGitfsClick()
    {
        for (int i = 0; i < _gifts.GiftItems.Count; i++)
        {
            _gifts.GiftItems[i].OnGiftClick += GiftSelected;
        }
    }

    private void UnsubcribeToGitfsClicks()
    {
        for (int i = 0; i < _gifts.GiftItems.Count; i++)
        {
            _gifts.GiftItems[i].OnGiftClick -= GiftSelected;
        }
    }

    public async void ResetGifts()
    {
        for (int i = 0; i < _gifts.GiftItems.Count; i++)
        {
            _gifts.GiftItems[i].BoxButton.gameObject.SetActive(true);
            _gifts.GiftItems[i].Prize.gameObject.SetActive(false);
            _gifts.GiftItems[i].Prize.PrizeImage.sprite = await Assets.GetAsset<Sprite>("EmptyGift");
        }
    }
    private void GiftSelected(int id)
    {
        GiftItem giftItem = _gifts.GiftItems.FirstOrDefault(s => s.ID == id);
        giftItem.BoxButton.gameObject.SetActive(false);
        Debug.Log($"Gift Selected ID{id} \n giftItem {giftItem}");

        GenerateGift(giftItem.Prize);
        giftItem.Prize.gameObject.SetActive(true);
        _gifts.TextBox.Text.text = $"{_openGifts.Count}/3";
    }

    private async void GenerateGift(Prize prize)
    {
        int dropType = GenerateDropType();
        int randomTypeNum = 3;

        if (dropType == 0)
            randomTypeNum = UnityEngine.Random.Range(0, 3);

        int randomGiftNum = UnityEngine.Random.Range(1, 5);

        switch (randomTypeNum)
        {
            case 0:
                Debug.Log("Win Ball");
                _openGifts.Add((GiftType.Ball, randomGiftNum));
                prize.PrizeImage.sprite = await Assets.GetAsset<Sprite>($"Ball{randomGiftNum}");
                SetBallPrizeText(prize, randomGiftNum);
                break;
            case 1:
                Debug.Log("Win Map");
                _openGifts.Add((GiftType.Map, randomGiftNum));
                prize.PrizeImage.sprite = await Assets.GetAsset<Sprite>($"Map{randomGiftNum}");
                prize.PrizeText.text = $"Map {randomGiftNum + 1}";
                break;
            case 2:
                Debug.Log("Win Background");
                _openGifts.Add((GiftType.Background, randomGiftNum));
                prize.PrizeImage.sprite = await Assets.GetAsset<Sprite>($"Background{randomGiftNum}");
                SetBackgroundPrizeText(prize, randomGiftNum);

                break;
            case 3:
                Debug.Log("Win Coins");
                int coinsAmount = GenerateCoins();
                _openGifts.Add((GiftType.Coins, coinsAmount));
                prize.PrizeImage.sprite = await Assets.GetAsset<Sprite>($"Coins0");
                prize.PrizeText.text = $"x{coinsAmount}";
                break;
        }

        if (_openGifts.Count >= 3)
        {
            UnsubcribeToGitfsClicks();
            _gifts.TextBox.gameObject.SetActive(false);
            _gifts.GetButton.gameObject.SetActive(true);
            _gifts.GetButton.onClick.AddListener(GetGifts);
        }
    }

    private void GetGifts()
    {
        _gifts.GetButton.onClick.RemoveListener(GetGifts);

        DateTime time = DateTime.Now;
        _dataHolder.GetData().TimeToOpenGift = time.AddHours(Const.WaitGiftHours).ToString();

        for (int i = 0; i < _openGifts.Count; i++)
        {
            switch (_openGifts[i].giftType)
            {
                case GiftType.Ball:
                    _dataHolder.GetData().AvailableBalls.Add(_openGifts[i].id);
                    break;
                case GiftType.Map:
                    _dataHolder.GetData().AvailableMaps.Add(_openGifts[i].id);
                    break;
                case GiftType.Background:
                    _dataHolder.GetData().AvailableBackgrounds.Add(_openGifts[i].id);
                    break;
                case GiftType.Coins:
                    _dataHolder.GetData().Coins += _openGifts[i].id;
                    break;
            }
        }

        _dataHolder.UpdateUI();
        ResetGifts();

        StartTimer();
    }

    private void StartTimer()
    {
        _gifts.GetButton.gameObject.SetActive(false);
        _gifts.TextBox.gameObject.SetActive(true);

        DateTime timerToOpen = UITools.Timer.ConvertStringToDateTime(_dataHolder.GetData().TimeToOpenGift);
        DateTime currentTime = DateTime.Now;

        var difference = timerToOpen.Subtract(currentTime);
        float remainingTime = (float)difference.TotalSeconds;

        Observable.EveryUpdate().Subscribe(_ =>
        {
            if (remainingTime != 0)
            {
                remainingTime -= Time.deltaTime;
                TimeSpan time = TimeSpan.FromSeconds(remainingTime);

                _gifts.TextBox.Text.text = $"{time.Hours}:{time.Minutes}:{time.Seconds}";
            }
        }).AddTo(_disposable);


    }

    private void SetBackgroundPrizeText(Prize prize, int randomGiftNum)
    {
        switch (randomGiftNum)
        {
            case 1:
                prize.PrizeText.text = "Purple";
                break;
            case 2:
                prize.PrizeText.text = "Green";
                break;
            case 3:
                prize.PrizeText.text = "Red";
                break;
            case 4:
                prize.PrizeText.text = "Light blue";
                break;
            default:
                break;
        }
    }

    private void SetBallPrizeText(Prize prize, int randomGiftNum)
    {
        switch (randomGiftNum)
        {
            case 1:
                prize.PrizeText.text = "Red";
                break;
            case 2:
                prize.PrizeText.text = "Blue";
                break;
            case 3:
                prize.PrizeText.text = "Green";
                break;
            case 4:
                prize.PrizeText.text = "Purple";
                break;
            default:
                break;
        }
    }

    private int GenerateDropType()
    {
        float range = UnityEngine.Random.Range(0f, 101f);

        if (_itemDropChance > range)
        {
            Debug.Log("Generate Item");
            return 0;
        }
        else
        {
            Debug.Log("Generate Coins");
            return 1;
        }
    }
    private int GenerateCoins()
    {
        int result;
        int coinsAmount = UnityEngine.Random.Range(0, 2);
        if (coinsAmount == 0) result = 500;
        else result = 1000;

        return result;
    }

}
public enum GiftType
{
    Ball,
    Map,
    Background,
    Coins

}
