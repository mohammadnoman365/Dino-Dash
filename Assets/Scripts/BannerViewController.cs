using UnityEngine;
using GoogleMobileAds.Api;

public class BannerViewController : MonoBehaviour
{
    private BannerView bannerView;

    // Test AdMob Banner ID for Android and iOS
#if UNITY_ANDROID
    private string adUnitId = "ca-app-pub-3940256099942544/6300978111";
#elif UNITY_IPHONE
    private string adUnitId = "ca-app-pub-3940256099942544/2934735716";
#else
    private string adUnitId = "unexpected_platform";
#endif

    void Start()
    {
        // Initialize AdMob
        MobileAds.Initialize(initStatus =>
        {
            Debug.Log("AdMob initialized.");
            RequestBannerAd();
        });
    }

    private void RequestBannerAd()
    {
        // If banner already exists, destroy it
        if (bannerView != null)
        {
            bannerView.Destroy();
        }

        // Create banner view at bottom of the screen
        bannerView = new BannerView(adUnitId, AdSize.Leaderboard, AdPosition.Top);

        // Add event listeners
        bannerView.OnBannerAdLoaded += OnBannerAdLoaded;
        bannerView.OnBannerAdLoadFailed += OnBannerAdLoadFailed;
        bannerView.OnAdClicked += OnAdClicked;
        bannerView.OnAdImpressionRecorded += OnAdImpressionRecorded;

        // Create an ad request
        AdRequest request = new AdRequest();
        bannerView.LoadAd(request);
    }

    private void OnBannerAdLoaded()
    {
        Debug.Log("Banner ad loaded successfully.");
    }

    private void OnBannerAdLoadFailed(LoadAdError error)
    {
        Debug.LogError("Banner ad failed to load: " + error.GetMessage());
    }

    private void OnAdClicked()
    {
        Debug.Log("Banner ad clicked.");
    }

    private void OnAdImpressionRecorded()
    {
        Debug.Log("Banner ad impression recorded.");
    }

    void OnDestroy()
    {
        if (bannerView != null)
        {
            bannerView.Destroy();
        }
    }
}
