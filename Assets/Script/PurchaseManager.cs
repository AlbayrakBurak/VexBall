// using UnityEngine;
// using UnityEngine.Purchasing;

// public class PurchaseManager : MonoBehaviour, IStoreListener
// {
//     private static IStoreController storeController;
//     private static IExtensionProvider storeExtensionProvider;
//     private string productId = "your_product_id"; // Ürün ID'sini buraya girin

//     void Start()
//     {
//         if (storeController == null)
//         {
//             InitializePurchasing();
//         }
//     }

//     private void InitializePurchasing()
//     {
//         var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());

//         builder.AddProduct(productId, ProductType.Consumable); // Ürünü satın alabilir (consumable) olarak tanımlayın

//         UnityPurchasing.Initialize(this, builder);
//     }

//     public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
//     {
//         storeController = controller;
//         storeExtensionProvider = extensions;
//     }

//     public void OnInitializeFailed(InitializationFailureReason error)
//     {
//         // Initialization failed, handle the error if needed
//     }

//     public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
//     {
//         // Purchase failed, handle the failure if needed
//     }

//     public void OnPurchaseComplete(Product product)
//     {
//         // Satın alma başarılı bir şekilde tamamlandı, gerekli işlemleri yapabilirsiniz
//     }

//     public void OnPurchaseButtonClick()
//     {
//         if (storeController != null)
//         {
//             Product product = storeController.products.WithID(productId);
//             if (product != null && product.availableToPurchase)
//             {
//                 storeController.InitiatePurchase(product);
//             }
//         }
//     }
// }
