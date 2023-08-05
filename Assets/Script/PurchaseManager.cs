using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;


using UnityEngine.UI;

public class PurchaseManager : MonoBehaviour, IStoreListener
{
    private static IStoreController s_StoreController;
    private static IExtensionProvider s_ExtensionProvider;

    private static string NoAds="noads";
     private bool isAdsRemoved = false;






    void Start()
    {
        if (s_StoreController == null)
        {
            InitializePurchasing();
        }
    }

    public void InitializePurchasing()
    {
        if (isInitialize())
        {
            return;
        }
        var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());

        builder.AddProduct(NoAds, ProductType.Consumable);

        UnityPurchasing.Initialize(this, builder);
    }

    
    public void ProductBuy_NoAds(){
        BuyProductID(NoAds);

    }


    void BuyProductID(string productId)
    {
        if (isInitialize())
        {
            Product product = s_StoreController.products.WithID(productId);
            if (product != null && product.availableToPurchase)
            {
                s_StoreController.InitiatePurchase(product);
                
            }
            else
            {
                Debug.Log("Error Buy");
            }
        }
        else
        {
            Debug.Log("Not find product");
        }
    }
    public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
    {
        s_StoreController = controller;
        s_ExtensionProvider = extensions;
    }

    public void OnInitializeFailed(InitializationFailureReason error)
    {
        throw new System.NotImplementedException();
    }

    public void OnInitializeFailed(InitializationFailureReason error, string message)
    {
        throw new System.NotImplementedException();
    }

    public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
    {
        throw new System.NotImplementedException();
    }

    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs purchaseEvent)
    {
       if (String.Equals(purchaseEvent.purchasedProduct.definition.id, NoAds, StringComparison.Ordinal))
        {
            // "No Ads" satın alındı, reklamları kaldırın veya etkinleştirilen bir özelliği işaretleyin.
            isAdsRemoved = true;
            PlayerPrefs.SetInt("NoAds",1);
        }
        return PurchaseProcessingResult.Complete;
        
    }

    private bool isInitialize()
    {
        return s_StoreController != null && s_ExtensionProvider != null;
    }
    public bool IsAdsRemoved()
    {
        return isAdsRemoved;
    }

}
