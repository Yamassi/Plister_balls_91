using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class ShopItem : MonoBehaviour
{
    public int ID;
    public Image Image, ImageFull, ImageMap;
    public TextMeshProUGUI Name;
    public SelectButtonSingleText SelectButtonSingleText;
    public Action<int> OnItemTryToBuy;
    private Tween _tween;
    private bool _isReceived = false;
    private void OnEnable()
    {
        SelectButtonSingleText.Button.onClick.AddListener(ItemSelected);
    }
    private void OnDisable()
    {
        SelectButtonSingleText.Button.onClick.RemoveListener(ItemSelected);
        _tween.Kill();
    }

    private void ItemSelected()
    {
        OnItemTryToBuy?.Invoke(ID);
    }

    public void SetPrice(string price)
    {
        SelectButtonSingleText.ActiveImage.gameObject.SetActive(true);
        SelectButtonSingleText.InactiveImage.gameObject.SetActive(false);

        SelectButtonSingleText.ButtonText.text = price;
    }
    public void Received()
    {
        SelectButtonSingleText.ActiveImage.gameObject.SetActive(false);
        SelectButtonSingleText.InactiveImage.gameObject.SetActive(true);

        SelectButtonSingleText.ButtonText.text = "Received";
        _isReceived = true;
        Debug.Log("Received");
    }
    public void NotEnoughMoney()
    {
        if (!_isReceived)
        {
            _tween = SelectButtonSingleText.GetComponent<RectTransform>().DOShakeScale(0.10f);
            _tween.onComplete += ResetPos;
        }

    }

    private void ResetPos()
    {
        SelectButtonSingleText.GetComponent<RectTransform>().localScale = Vector3.one;
    }

    public void SetName(string name)
    {
        Name.text = name;
    }
    public void SetImage(Sprite sprite)
    {
        Image.gameObject.SetActive(true);
        ImageFull.gameObject.SetActive(false);
        ImageMap.gameObject.SetActive(false);

        Image.sprite = sprite;
    }
    public void SetMapImage(Sprite sprite)
    {
        Image.gameObject.SetActive(false);
        ImageFull.gameObject.SetActive(false);
        ImageMap.gameObject.SetActive(true);

        ImageMap.sprite = sprite;
    }
    public void SetFullImage(Sprite sprite)
    {
        Image.gameObject.SetActive(false);
        ImageFull.gameObject.SetActive(true);
        ImageMap.gameObject.SetActive(false);

        ImageFull.sprite = sprite;
    }
}
