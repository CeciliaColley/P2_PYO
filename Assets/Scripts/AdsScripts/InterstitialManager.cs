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

    internal void Initialize()
    {
        Advertisement.Load(adUnitID, this);
    }

    public void OnUnityAdsAdLoaded(string placementId)
    {
        adLoaded = true;
    }

    public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
    {
        Debug.Log($"Interstitial: Error loading Ad Unit: {adUnitID} - {error.ToString()} - {message}");
    }

    public void ShowInterstitial()
    {
        if (adLoaded)
        {
            Advertisement.Show(adUnitID, this);
        }
    }

    public void OnUnityAdsShowStart(string _adUnitId)
    {
        Advertisement.Banner.Hide();
        Debug.Log("Showing interstitial now");
    }

    public void OnUnityAdsShowClick(string _adUnitId)
    {
        Debug.Log("An interstitial ad was clicked on.");
    }

    public void OnUnityAdsShowComplete(string _adUnitId, UnityAdsShowCompletionState showCompletionState)
    {
        Debug.Log("An interstitial ad is closing.");
        AdsManager.Instance.banner.Show();
    }

    public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
    {
        Debug.Log($"Error showing Ad Unit {adUnitID}: {error.ToString()} - {message}");
    }
#endif
}