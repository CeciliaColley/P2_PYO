using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class InterstitialManager : MonoBehaviour
#if UNITY_ANDROID || UNITY_IOS
, IUnityAdsLoadListener, IUnityAdsShowListener
#endif
{
#if UNITY_ANDROID || UNITY_IOS
    [SerializeField] string androidAdUnitID = "Interstitial_Android";
    [SerializeField] string iOSAdUnitID = "Interstitial_iOS";
    string adUnitID;
    bool adLoaded = false;

    void Start()
    {
#if UNITY_IOS
        adUnitID = iOSAdUnitID;
#elif UNITY_ANDROID
        adUnitID = androidAdUnitID;
#endif
    }

    // Innitialize the Ad.
    internal void Initialize()
    {
        Advertisement.Load(adUnitID, this);
    }

    // Keep track of whether the ad is loaded or not, so that we don't try to show an ad that hasn't been loaded.
    public void OnUnityAdsAdLoaded(string placementId)
    {
        adLoaded = true;
    }


    // Included for debugging purposes.
    public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
    {
        Debug.Log($"Interstitial: Error loading Ad Unit: {adUnitID} - {error.ToString()} - {message}");
    }

    // Play the ad that was loaded.
    public void ShowInterstitial()
    {
        if (adLoaded)
        {
            Advertisement.Show(adUnitID, this);
        }
    }

    // Hide the banner so it doesnt cover the interstitial
    public void OnUnityAdsShowStart(string _adUnitId)
    {
        Advertisement.Banner.Hide();
    }

    // Included for debugging purposes, and to showcase I know that this function exists.
    public void OnUnityAdsShowClick(string _adUnitId)
    {
        Debug.Log("An interstitial ad was clicked on.");
    }

    //  Once the ad is complete, relaod the banner and reload the next interstitial ad.
    public void OnUnityAdsShowComplete(string _adUnitId, UnityAdsShowCompletionState showCompletionState)
    {
        AdsManager.Instance.banner.Show();
        adLoaded = false;
        Advertisement.Load(adUnitID, this);
    }

    // Included for debugging purposes only.
    public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
    {
        Debug.Log($"Error showing Ad Unit {adUnitID}: {error.ToString()} - {message}");
    }
#endif
}