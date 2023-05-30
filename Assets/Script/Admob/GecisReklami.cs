using GoogleMobileAds.Api;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GecisReklami : MonoBehaviour
{
    

#if UNITY_EDITOR
    string _adUnitID = "ca-app-pub-3940256099942544/1033173712";
#elif UNITY_IPHONE
        string _adUnitID = "ca-app-pub-3940256099942544/4411468910";
#else
        string _adUnitID = "ca-app-pub-7605629714512840/6579191840";
#endif

    InterstitialAd _GecisReklami;

    void Start()
    {
        MobileAds.Initialize((InitializationStatus initStatus) =>
        {
            
        });
        GecisReklamiOlustur();
        int randomX = Random.Range(0, 10);
        Debug.Log(randomX);
        if(randomX>=6){

      
        GecisReklamiGoster();
        }
    }

    void GecisReklamiOlustur()
    {
        if (_GecisReklami !=null)
        {
            _GecisReklami.Destroy();
            _GecisReklami = null;
        }       

        var _AdRequest = new AdRequest.Builder().Build();

        InterstitialAd.Load(_adUnitID, _AdRequest,
            (InterstitialAd Ad, LoadAdError error) =>
            {
                if (error != null || Ad==null) {

                Debug.LogError("Reklam yüklenirken hata oluştu. HATA: " + error);
                    return;
                }

                _GecisReklami = Ad;

            });

        ReklamOlaylariniDinle(_GecisReklami);
    }


    void ReklamOlaylariniDinle(InterstitialAd ad)
    {
        ad.OnAdPaid += (AdValue adValue) =>
        {
          Debug.Log(string.Format("Ödüllü Reklam {0} {1}.", 
          adValue.Value, 
          adValue.CurrencyCode
          ));

        };

        ad.OnAdImpressionRecorded += () =>
        {
            Debug.Log("Geçiş reklamı bir gösterim kaydetti.");
        };

        ad.OnAdClicked += () =>
        {
            Debug.Log("Geçiş reklamı tıklandı.");
        };

        ad.OnAdFullScreenContentOpened += () =>
        {
            Debug.Log("Geçiş reklamı tam ekran açıldı.");
        };

        ad.OnAdFullScreenContentClosed += () =>
        {
            Debug.Log("Geçiş reklamı kapatıldı.");
            GecisReklamiOlustur();
        };

        ad.OnAdFullScreenContentFailed += (AdError error) =>
        {
            Debug.Log("Geçiş reklamı açılamadı. HATA: " + error);
            GecisReklamiOlustur();
        };
    }

    public void GecisReklamiGoster()
    {

        if (_GecisReklami != null && _GecisReklami.CanShowAd())
        {           
            _GecisReklami.Show();
            Debug.Log("Reklam gösterildi");
            Debug.Log(_adUnitID);
        }
        else
        {
            Debug.Log("Geçiş reklamı henüz hazır değil");
        }

    }

    void ReklamiOldur()
    {
        _GecisReklami.Destroy();
    }


    
}
