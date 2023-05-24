using GoogleMobileAds.Api;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GecisReklami : MonoBehaviour
{
    
//ca-app-pub-7605629714512840/6579191840
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
        GecisReklamiGoster();

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

                    Debug.LogError("Reklam y�klenirken hata olu�tu HATA : " + error);
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
            Debug.Log(string.Format("�d�ll� Reklam {0} {1}.",
                adValue.Value,
                adValue.CurrencyCode));
        };

        ad.OnAdImpressionRecorded += () =>
        {
            Debug.Log("Ge�i� reklam� bir g�sterim kaydetti.");
        };

        ad.OnAdClicked += () =>
        {
            Debug.Log("Ge�i� reklam� t�kland�.");
        };

        ad.OnAdFullScreenContentOpened += () =>
        {
            Debug.Log("Ge�i� reklam� tam ekran a��ld�.");
        };

        ad.OnAdFullScreenContentClosed += () =>
        {
            Debug.Log("Ge�i� reklam� kapat�ld�.");
            GecisReklamiOlustur();
        };

        ad.OnAdFullScreenContentFailed += (AdError error) =>
        {
            Debug.Log("Ge�i� reklam� a��lamad�. HATA : " + error);
            GecisReklamiOlustur();
        };
    }

    public void GecisReklamiGoster()
    {

        if (_GecisReklami != null && _GecisReklami.CanShowAd())
        {           
            _GecisReklami.Show();
            Debug.Log("reklam g�sterildi");
        }
        else
        {
            Debug.Log("Ge�i� reklam� hen�z haz�r de�il");
        }

    }

    void ReklamiOldur()
    {
        _GecisReklami.Destroy();
    }
    
}
