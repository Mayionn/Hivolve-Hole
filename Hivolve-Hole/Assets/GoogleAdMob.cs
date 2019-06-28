using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class GoogleAdMob : MonoBehaviour
{
    // Start is called before the first frame update
    public void Start()
    {
#if UNITY_ANDROID
        string appId = "ca-app-pub-1249591444731632~3514994673";
#else
        string appId = "unexpected_platform";
#endif

        string adUnitId = "ca-app-pub-3940256099942544/6300978111";

        // Initialize the Google Mobile Ads SDK.
        /*MobileAds.Initialize(appId);

        // Create a 320x50 banner at the top of the screen.
        BannerView bannerView = new BannerView(adUnitId, AdSize.SmartBanner, AdPosition.Bottom);

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();

        // Load the banner with the request.
        bannerView.LoadAd(request); */
    }


}
