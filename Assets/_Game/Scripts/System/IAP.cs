using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;
public class IAP : MonoBehaviour, IStoreListener
{
    private IStoreController _storeController;
    private const string IAP_RED_BALL = "speed_ball";
    private const string IAP_GREEN_BALL = "saver_ball";

    private void Start()
    {
        SetupBuilder();
        // EventHolder.OnPurchasing += Purchase;
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
        Debug.Log("On Success Purchase 3");
        // EventHolder.OnSuccessPurchasing?.Invoke(3);
    }
    private void AddGreenBall()
    {
        Debug.Log("On Success Purchase 4");
        // EventHolder.OnSuccessPurchasing?.Invoke(4);
    }
    private void SetupBuilder()
    {
        var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());

        builder.AddProduct(IAP_RED_BALL, ProductType.NonConsumable);
        builder.AddProduct(IAP_GREEN_BALL, ProductType.NonConsumable);

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
            case IAP_RED_BALL:
                AddRedBall();
                break;
            case IAP_GREEN_BALL:
                AddGreenBall();
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
