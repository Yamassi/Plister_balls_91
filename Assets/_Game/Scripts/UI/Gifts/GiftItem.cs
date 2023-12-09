using System;
using UnityEngine;
using UnityEngine.UI;

public class GiftItem : MonoBehaviour
{
    public int ID;
    public Button BoxButton;
    public Prize Prize;
    public Action<int> OnGiftClick;
    private void OnValidate()
    {
        BoxButton = GetComponentInChildren<Button>();
    }
    private void OnEnable()
    {
        BoxButton.onClick.AddListener(GiftSelected);
    }
    private void OnDisable()
    {
        BoxButton.onClick.RemoveListener(GiftSelected);
    }
    private void GiftSelected()
    {
        OnGiftClick?.Invoke(ID);
    }

}
