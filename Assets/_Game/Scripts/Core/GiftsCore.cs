using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Tretimi;
using UnityEngine;

public class GiftsCore
{
    private readonly List<GiftItem> _giftItems;
    private List<(GiftType, int)> _openGifts = new(4);
    private bool _isGiftsAvailable = true;
    private float _itemDropChance = 20;
    public GiftsCore(List<GiftItem> giftItems)
    {
        _giftItems = giftItems;
    }
    public void Init()
    {
        ResetGifts();
        if (_isGiftsAvailable)
            for (int i = 0; i < _giftItems.Count; i++)
            {
                _giftItems[i].OnGiftClick += GiftSelected;
            }
    }
    public void DeInit()
    {
        if (_isGiftsAvailable)
            for (int i = 0; i < _giftItems.Count; i++)
            {
                _giftItems[i].OnGiftClick -= GiftSelected;
            }
    }
    public async void ResetGifts()
    {
        for (int i = 0; i < _giftItems.Count; i++)
        {
            _giftItems[i].BoxButton.gameObject.SetActive(true);
            _giftItems[i].Prize.gameObject.SetActive(false);
            _giftItems[i].Prize.PrizeImage.sprite = await Assets.GetAsset<Sprite>("EmptyGift");
        }
    }
    private void GiftSelected(int id)
    {
        GiftItem giftItem = _giftItems.FirstOrDefault(s => s.ID == id);
        giftItem.BoxButton.gameObject.SetActive(false);
        Debug.Log($"Gift Selected ID{id} \n giftItem {giftItem}");

        GenerateGift(giftItem.Prize);
        giftItem.Prize.gameObject.SetActive(true);
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
                _openGifts.Add((GiftType.Coins, randomGiftNum));
                int coinsAmount = GenerateCoins();
                prize.PrizeImage.sprite = await Assets.GetAsset<Sprite>($"Coins0");
                prize.PrizeText.text = $"x{coinsAmount}";
                break;
        }
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
