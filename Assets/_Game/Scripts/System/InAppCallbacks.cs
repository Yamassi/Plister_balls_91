using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.UI;


public class InAppCallbacks : MonoBehaviour
{
    public GameObject Loader;
    public TextMeshProUGUI MsgText;

    private IDataService _dataService;

    public void Init(IDataService dataService)
    {
        _dataService = dataService;
    }

    public void OpenLoader()
    {
        MsgText.text = "Confirm your Purchase";
        Loader.SetActive(true);
    }

    public void CloseLoader(string message)
    {
        Debug.Log($"Message {message}");
        MsgText.text = message;
        Invoke(nameof(CloseDelay), 2f);
    }

    private void CloseDelay()
    {
        Loader.SetActive(false);
    }

    public void OnPurchaseCompleted(Product product)
    {
        if (product.definition.id == Const.IAP_1)
        {
            _dataService.AddCoins(5000);
        }

        if (product.definition.id == Const.IAP_2)
        {
            _dataService.AddCoins(12000);
        }

        if (product.definition.id == Const.IAP_3)
        {
            _dataService.AddCoins(20000);
        }
    }
}
