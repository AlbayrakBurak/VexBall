using GoogleMobileAds.Api;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class OdulluGecisReklam : MonoBehaviour
{
    // Reklam birim kimliklerini burada tanımlayın
#if UNITY_EDITOR
    string adUnitID = "ca-app-pub-3940256099942544/5354046379";
#elif UNITY_IPHONE
    string adUnitID = "ca-app-pub-3940256099942544/6978759866";
#else
    string adUnitID = "ca-app-pub-7605629714512840/2903734358";
#endif

    RewardedInterstitialAd odulluGecisReklam;
    public Button rewardContinueButton;
    public GameObject failPanel;
    public GameObject levelPanel;
    public GameObject playerBase;
    public TextMeshProUGUI countText;
    public GameObject countPanel;
    public GameObject top;
    private float count = 4f;
    private bool countDownActive = false;
    public TextMeshProUGUI internetConnectionPopupText ;


    void Start()
    {
        MobileAds.Initialize((InitializationStatus initStatus) =>
        {

        });
        OdulluGecisReklamOlustur();
        rewardContinueButton.onClick.AddListener(OdulluGecisReklamGoster);
    }

    void OdulluGecisReklamOlustur()
    {
        if (odulluGecisReklam != null)
        {
            odulluGecisReklam.Destroy();
            odulluGecisReklam = null;
        }

        var adRequest = new AdRequest.Builder().Build();

        RewardedInterstitialAd.Load(adUnitID, adRequest, (RewardedInterstitialAd ad, LoadAdError error) =>
        {
            if (error != null || ad == null)
            {
                Debug.LogError("Ödüllü Geçiş reklamı yüklenirken hata oluştu. HATA: " + error);
                return;
            }

            odulluGecisReklam = ad;
        });

        ReklamOlaylariniDinle(odulluGecisReklam);
    }

    void ReklamOlaylariniDinle(RewardedInterstitialAd ad)
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
    private bool isCountdownActive=false;
    public void OdulluGecisReklamGoster()
    {
        const string odulMesaji = "Ödüllü Geçiş Kazanıldı. Ürün: {0}, Değer: {1}";
        if (Application.internetReachability != NetworkReachability.NotReachable)
        {

        if  (!isCountdownActive && odulluGecisReklam != null && odulluGecisReklam.CanShowAd())
        {
            odulluGecisReklam.Show((Reward reward) =>
            {
                Debug.Log(string.Format(odulMesaji, reward.Type, reward.Amount));
                rewardContinueButton.interactable = false;
                countDownActive = true;
                top.GetComponent<Rigidbody>().isKinematic = true;
            });
        }
        else
        {
            Debug.Log("Ödüllü reklam henüz hazır değil.");
        }
    }
    else
        {

        
            ShowInternetConnectionPopup();
        }
    }

    void Update()
    {
        
        if (countDownActive)
        {
            failPanel.SetActive(false);
            levelPanel.SetActive(true);
            playerBase.SetActive(true);
            top.SetActive(true);
            Time.timeScale = 1f;
            count -= Time.deltaTime;

            int countInt = (int)count;
            countPanel.SetActive(true);
            countText.text = countInt.ToString("D1");
            Debug.Log("Tüm işlemler bitti: " + count);
        }

        if (count < 1)
        {
            countDownActive = false;
            Debug.Log("If bloğuna girdi.");
            ContinueGame();
        }
    }

    public void ContinueGame()
    {
        countPanel.SetActive(false);
        count = 4f;
        Debug.Log("4f bloğuna girdi.");
        top.GetComponent<Rigidbody>().isKinematic = false;
    }
    
     void ShowInternetConnectionPopup()
    {
        // İnternet bağlantısı uyarı popup'ını göster
          string message = "Check your internet connection!";
        // Burada istediğiniz şekilde bir popup gösterimi sağlayabilirsiniz.

         internetConnectionPopupText.text = message;
    }

}
