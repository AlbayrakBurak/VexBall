using UnityEngine;
using GoogleMobileAds.Api;
using System;
using TMPro;

public class AdManager : MonoBehaviour
{
    
#if UNITY_EDITOR
    string adUnitID = "ca-app-pub-3940256099942544/5224354917";
#elif UNITY_IPHONE
    string adUnitID = "ca-app-pub-3940256099942544/6978759866";
#else
    string adUnitID = "ca-app-pub-7605629714512840/2946236383";
#endif

    RewardedAd ballSkinReward;
    public TextMeshProUGUI internetConnectionPopupText ;
    private Action<bool> adCompletionCallback;
    


     void Start()
    {
        MobileAds.Initialize((InitializationStatus initStatus) =>
        {

        });
        OdulluGecisReklamOlustur();
        
    }

    public void OdulluGecisReklamOlustur()
    {
        if (ballSkinReward != null)
        {
            ballSkinReward.Destroy();
            ballSkinReward = null;
        }

        var adRequest = new AdRequest.Builder().Build();

        RewardedAd.Load(adUnitID, adRequest, (RewardedAd ad, LoadAdError error) =>
        {
            if (error != null || ad == null)
            {
                Debug.LogError("Ödüllü Geçiş reklamı yüklenirken hata oluştu. HATA: " + error);
                return;
            }

            ballSkinReward = ad;
        });

        ReklamOlaylariniDinle(ballSkinReward);
    }

    void ReklamOlaylariniDinle(RewardedAd ad)
    {
        ad.OnAdPaid += (AdValue adValue) =>
        {
            Debug.Log(string.Format("Ücretli geçiş reklamı {0} {1} ile ödendi.", adValue.Value, adValue.CurrencyCode));
        };

        ad.OnAdImpressionRecorded += () =>
        {
            Debug.Log("Ödüllü Geçiş reklamı bir görüntüleme kaydetti.");
        };

        ad.OnAdClicked += () =>
        {
            Debug.Log("Ödüllü Geçiş reklamı tıklandı.");
        };

        ad.OnAdFullScreenContentOpened += () =>
        {
            Debug.Log("Ödüllü Geçiş reklamı açıldı.");
        };

        ad.OnAdFullScreenContentClosed += () =>
        {
            Debug.Log("Ödüllü Geçiş reklamı kapatıldı.");
            OdulluGecisReklamOlustur();
        };

        ad.OnAdFullScreenContentFailed += (AdError error) =>
        {
            Debug.Log("Geçiş reklamı tam ekran açılamadı. HATA: " + error);
            OdulluGecisReklamOlustur();
        };
    }

    public void OdulluGecisReklamGoster(Action<bool> callback)
    {
        const string odulMesaji = "Ödüllü Geçiş Kazanıldı. Ürün: {0}, Değer: {1}";
        if (Application.internetReachability != NetworkReachability.NotReachable)
        {
            if (ballSkinReward != null && ballSkinReward.CanShowAd())
            {
                // Reklam gösterildiğinde bu kod çalışacak
                adCompletionCallback = callback;
                ballSkinReward.Show((Reward reward) =>
                {
                    Debug.Log(string.Format(odulMesaji, reward.Type, reward.Amount));
                    adCompletionCallback?.Invoke(true); // Reklam izlendi, callback'i başarıyla çağırın
                });
            }
            else
            {
                Debug.Log("Ödüllü reklam henüz hazır değil.");
                callback?.Invoke(false); // Reklam henüz hazır değil, callback'i başarısız çağırın
            }
            Debug.Log("Gösterildi");
        }
    
    else
        {

        
            ShowInternetConnectionPopup();
        }
    }
     void ShowInternetConnectionPopup()
    {
        // İnternet bağlantısı uyarı popup'ını göster
          string message = "Check your internet connection!";
        // Burada istediğiniz şekilde bir popup gösterimi sağlayabilirsiniz.

         internetConnectionPopupText.text = message;
    }

}
