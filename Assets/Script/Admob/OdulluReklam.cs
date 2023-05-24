using GoogleMobileAds.Api;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class OdulluReklam : MonoBehaviour
{


#if UNITY_EDITOR
    string _adUnitID = "ca-app-pub-3940256099942544/5224354917";
#elif UNITY_IPHONE
        string _adUnitID = "ca-app-pub-3940256099942544/1712485313";
#else
        string _adUnitID = "ca-app-pub-7605629714512840/3712838439";
#endif


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
    RewardedAd _OdulluReklam;

    public void Start()
    {
        
        MobileAds.Initialize((InitializationStatus initStatus) =>
        {

        });
            
        OdulluReklamOlustur();
      
    }

    void OdulluReklamOlustur()
    {
        if (_OdulluReklam != null)
        {
            _OdulluReklam.Destroy();
            _OdulluReklam = null;
        }

        var _AdRequest = new AdRequest.Builder().Build();

        RewardedAd.Load(_adUnitID, _AdRequest,
            (RewardedAd Ad, LoadAdError error) =>
            {
                if (error != null || Ad == null)
                {

                    Debug.LogError("�d�ll� reklam y�klenirken hata olu�tu HATA : " + error);
                    return;
                }

                _OdulluReklam = Ad;

            });

        ReklamOlaylariniDinle(_OdulluReklam);
    }
    void ReklamOlaylariniDinle(RewardedAd ad)
    {
        ad.OnAdPaid += (AdValue adValue) =>
        {
            Debug.Log(string.Format("�d�ll� reklam� {0} {1}.",
                adValue.Value,
                adValue.CurrencyCode));
        };

        ad.OnAdImpressionRecorded += () =>
        {
            Debug.Log("�d�ll� reklam bir g�sterim kaydetti..");
        };

        ad.OnAdClicked += () =>
        {
            Debug.Log("�d�ll� reklam T�kland�.");
        };

        ad.OnAdFullScreenContentOpened += () =>
        {
            Debug.Log("�d�ll� reklam tam ekran a��ld�");
        };

        ad.OnAdFullScreenContentClosed += () =>
        {
            Debug.Log("�d�ll� reklam kapat�ld�.");
            OdulluReklamOlustur();
        };

        ad.OnAdFullScreenContentFailed += (AdError error) =>
        {
            Debug.Log("�d�ll� reklam a��lamad�. HATA : " + error);
            OdulluReklamOlustur();
        };
    }

    public void OdulluReklamGoster()
    {
        const string OdulMesaji = "�d�l Kazan�ld�. �r�n : {0}, De�er {1}";

        if (_OdulluReklam != null && _OdulluReklam.CanShowAd())
        {
            _OdulluReklam.Show((Reward reward) =>
            {
                 
                Debug.Log(string.Format(OdulMesaji, reward.Type, reward.Amount));
                RewardContinueButton.interactable=false;
               FailPanel.SetActive(false); 
                
                
                LevelPanel.SetActive(true);
                PlayerBase.SetActive(true);
                Top.GetComponent<Rigidbody>().isKinematic=true;
                CountDownActive=true;
                
               
                
            });
        }
        else
        {
            Debug.Log("�d�ll� reklam hen�z haz�r de�il");
        }

    }
    void ReklamiOldur()
    {
        _OdulluReklam.Destroy();
    }


    void Update(){
        if(CountDownActive){
        count-=Time.deltaTime;
        int _count=(int)count;
        CountPanel.SetActive(true);
        Count.text=_count.ToString("D1");
        if(count<1){
            Time.timeScale=1f;    
            CountDownActive=false;
        CountPanel.SetActive(false);
            count=4f;
            Top.GetComponent<Rigidbody>().isKinematic=false;
        }        
    }

    }
    }
