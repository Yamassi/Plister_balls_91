using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Purchasing;
public class IAP : MonoBehaviour, IStoreListener
{
    [SerializeField] private List<ButtonIAP> _buttonIAPs;
    private IStoreController _storeController;
    private IDataService _dataService;

    public void Init(IDataService dataService)
    {
        _dataService = dataService;

        SetupBuilder();
        for (int i = 0; i < _buttonIAPs.Count; i++)
        {
            _buttonIAPs[i].OnPurchasing += Purchase;
        }
    }

    private void Purchase(string id)
    {
        Debug.Log($"Purchase {id}");
        Product product = _storeController.products.WithID(id);
        _storeController.InitiatePurchase(product);
        Debug.Log($"Purchase {id}");
    }

    private void AddRedBall()
    {
        Debug.Log("On Success Purchase 0");
        _dataService.AddCoins(5000);

    }
    private void AddGreenBall()
    {
        Debug.Log("On Success Purchase 1");
        _dataService.AddCoins(12000);

    }
    private void AddYellowBall()
    {
        Debug.Log("On Success Purchase 2");
        _dataService.AddCoins(50000);

    }
    private void SetupBuilder()
    {
        var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());

        builder.AddProduct(Const.IAP_1, ProductType.Consumable);
        builder.AddProduct(Const.IAP_2, ProductType.Consumable);
        builder.AddProduct(Const.IAP_3, ProductType.Consumable);

        UnityPurchasing.Initialize(this, builder);

    }

    public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
    {
        _storeController = controller;
    }

    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs purchaseEvent)
    {
        var product = purchaseEvent.purchasedProduct;

        switch (product.definition.id)
        {
            case Const.IAP_1:
                AddRedBall();
                break;
            case Const.IAP_2:
                AddGreenBall();
                break;
            case Const.IAP_3:
                AddYellowBall();
                break;
        }

        return PurchaseProcessingResult.Complete;
    }

    public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
    {
        Debug.Log("Purchase Failed");
    }
    public void OnInitializeFailed(InitializationFailureReason error)
    {
        throw new NotImplementedException();
    }

    public void OnInitializeFailed(InitializationFailureReason error, string message)
    {
        throw new NotImplementedException();
    }
}
