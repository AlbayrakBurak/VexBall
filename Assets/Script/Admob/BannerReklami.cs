using GoogleMobileAds.Api;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BannerReklami : MonoBehaviour
{

#if UNITY_EDITOR
    string _adUnitID = "ca-app-pub-3940256099942544/6300978111";
#elif UNITY_IPHONE
        string _adUnitID = "ca-app-pub-3940256099942544/2934735716";
#else
        //string _adUnitID = "ca-app-pub-7605629714512840/5621333399";
#endif

    BannerView _Banner;
    void Start()
    {

        MobileAds.Initialize((InitializationStatus initStatus) =>
        {

        });
        BannerYukle();
    }

    void BannerOlustur()
    {
        if (_Banner != null)
        {
            _Banner.Destroy();
            _Banner = null;
        }

        //_Banner = new BannerView(_adUnitID, AdSize.Banner,0,50); // y�ntem 1
        // _Banner = new BannerView(_adUnitID, AdSize.Banner,AdPosition.Bottom); // y�ntem 2

        AdSize Adsize = new(468,60);
        _Banner = new BannerView(_adUnitID, Adsize, AdPosition.Bottom); // y�ntem 3
    }

    void BannerYukle()
    {
        if (_Banner == null)
        {
            BannerOlustur();

            var _AdRequest = new AdRequest.Builder().Build();

            _Banner.LoadAd(_AdRequest);
            ReklamOlaylariniDinle();
        }

    }

    void ReklamOlaylariniDinle()
    {
        _Banner.OnBannerAdLoaded += () =>
        {
            Debug.Log("banner Y�klendi");

        };
        _Banner.OnBannerAdLoadFailed += (LoadAdError error) =>
        {
            Debug.Log("Banner Y�klenemedi. HATA : " + error);
           // BannerYukle();
        };
    }

}
