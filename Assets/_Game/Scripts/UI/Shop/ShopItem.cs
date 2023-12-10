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
        Debug.Log("Received");
    }
    public void NotEnoughMoney()
    {
        _tween = SelectButtonSingleText.GetComponent<RectTransform>().DOShakeAnchorPos(0.3f, 60);
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
