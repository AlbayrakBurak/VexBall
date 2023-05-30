using GoogleMobileAds.Api;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class OdulluGecisReklam : MonoBehaviour
{

#if UNITY_EDITOR
    string _adUnitID = "ca-app-pub-3940256099942544/5354046379";
#elif UNITY_IPHONE
        string _adUnitID = "ca-app-pub-3940256099942544/6978759866";
#else
        string _adUnitID = "ca-app-pub-7605629714512840/2903734358";
#endif

    RewardedInterstitialAd _OdulluGecisReklam;
    public Button RewardContinueButton;
    public GameObject FailPanel;
    public GameObject LevelPanel;
    public GameObject PlayerBase;
    public TextMeshProUGUI Count;
    public GameObject CountPanel;
    public GameObject Top;
    public float count=4f;
    public bool CountDownActive=false;
    GameManager _gameManager=new GameManager();

    void Start()
    {
        MobileAds.Initialize((InitializationStatus initStatus) =>
        {

        });
        OdulluGecisReklamOlustur();
    }

    void OdulluGecisReklamOlustur()
    {
        if (_OdulluGecisReklam != null)
        {
            _OdulluGecisReklam.Destroy();
            _OdulluGecisReklam = null;
        }

        var _AdRequest = new AdRequest.Builder().Build();

        RewardedInterstitialAd.Load(_adUnitID, _AdRequest,
            (RewardedInterstitialAd Ad, LoadAdError error) =>
            {
                if (error != null || Ad == null)
                {

                    Debug.LogError("�d�ll� Geci� reklam y�klenirken hata olu�tu HATA : " + error);
                    return;
                }

                _OdulluGecisReklam = Ad;

            });

        ReklamOlaylariniDinle(_OdulluGecisReklam);
    }
    void ReklamOlaylariniDinle(RewardedInterstitialAd ad)
    {
        ad.OnAdPaid += (AdValue adValue) =>
        {
            Debug.Log(string.Format("�cretli ge�i� reklam� {0} {1}.",
              adValue.Value,
                adValue.CurrencyCode));
        };

        ad.OnAdImpressionRecorded += () =>
        {
            Debug.Log("�d�ll� Ge�i� reklam� bir g�sterim kaydetti.");
        };

        ad.OnAdClicked += () =>
        {
            Debug.Log("�d�ll� Ge�i� reklam� t�kland�.");
        };

        ad.OnAdFullScreenContentOpened += () =>
        {
            Debug.Log("�d�ll� Ge�i� reklam� a��ld�.");
        };

        ad.OnAdFullScreenContentClosed += () =>
        {
            Debug.Log("�d�ll� Ge�i� reklam� kapat�ld�.");
            OdulluGecisReklamOlustur();
        };

        ad.OnAdFullScreenContentFailed += (AdError error) =>
        {
            Debug.Log("Ge�i� reklam� tam ekran a��lamad�. HATA : " + error);
            OdulluGecisReklamOlustur();
        };
    }
    public void OdulluGecisReklamGoster()
    {
        const string OdulMesaji = "�d�ll� Ge�i� Kazan�ld�. �r�n : {0}, De�er {1}";

        if (_OdulluGecisReklam != null && _OdulluGecisReklam.CanShowAd())
        {
            _OdulluGecisReklam.Show((Reward reward) =>
            {
                
                Debug.Log(string.Format(OdulMesaji, reward.Type, reward.Amount));
                 RewardContinueButton.interactable=false;
                
                CountDownActive=true;
                Top.GetComponent<Rigidbody>().isKinematic=true;
            });
        }
        else
        {
            Debug.Log("�d�ll� reklam hen�z haz�r de�il");
        }

    }
    void ReklamiOldur()
    {
        _OdulluGecisReklam.Destroy();
    }
        void Update(){
        if(CountDownActive){
        FailPanel.SetActive(false); 
                LevelPanel.SetActive(true);
                PlayerBase.SetActive(true);
                Top.SetActive(true);
                Time.timeScale=1f;  
                count-=Time.deltaTime;
                
        int _count=(int)count;
        CountPanel.SetActive(true);
        Count.text=_count.ToString("D1");
        Debug.Log("TUM ISLEMLER BİTTİ"+count); 
        } 
        if(count<1){ 
            CountDownActive=false;
            Debug.Log("ife Girdi");
            ContinueGame();
                 
           
              
    }

    }

    public void ContinueGame(){
  

        CountPanel.SetActive(false);
        
            count=4f;
            Debug.Log("4f Girdi");
            Top.GetComponent<Rigidbody>().isKinematic=false;       

    }
}
