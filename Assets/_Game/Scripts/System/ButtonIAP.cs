using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class ButtonIAP : MonoBehaviour
{
    [SerializeField] private Button _button;
    public IAPItem iAPItems;
    public Action<string> OnPurchasing;
    private void OnValidate()
    {
        _button = GetComponent<Button>();
    }
    private void OnEnable()
    {
        _button.onClick.AddListener(Action);
    }
    private void OnDisable()
    {
        _button.onClick.RemoveListener(Action);
    }
    private void Action()
    {
        Debug.Log($"Send Purchase {iAPItems}");
        switch (iAPItems)
        {
            case IAPItem.x5000:
                OnPurchasing?.Invoke(Const.IAP_1);
                break;
            case IAPItem.x12000:
                OnPurchasing?.Invoke(Const.IAP_2);
                break;
            case IAPItem.x50000:
                OnPurchasing?.Invoke(Const.IAP_3);
                break;
        }


    }

}
public enum IAPItem
{
    x5000 = 0,
    x12000 = 1,
    x50000 = 2,
}