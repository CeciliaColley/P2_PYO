using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class RewardedAdManager : MonoBehaviour
#if UNITY_ANDROID || UNITY_IOS
    , IUnityAdsLoadListener, IUnityAdsShowListener
#endif
{
#if UNITY_ANDROID || UNITY_IOS
    //These variables must be set from the inspector.
    [SerializeField] string androidAdUnitID = "Rewarded_Android";
    [SerializeField] string iOSAdUnitID = "Rewarded_iOS";
    public int rewardedSeconds = 0;

    string adUnitID = null;
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

    public void ShowRewardedAd()
    {
        Advertisement.Show(adUnitID, this);
    }

    public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
    {
        Debug.Log($"Rewarded Ad: Error loading Ad Unit: {adUnitID} - {error.ToString()} - {message}");
    }

    public void OnUnityAdsShowStart(string _adUnitId)
    {
        Advertisement.Banner.Hide();
        Debug.Log("Showing rewarded ad.");
    }

    public void OnUnityAdsShowClick(string _adUnitId)
    {
        Debug.Log("An rewarded ad was clicked on.");
    }

    public void OnUnityAdsShowComplete(string _adUnitId, UnityAdsShowCompletionState showCompletionState)
    {
        if (_adUnitId.Equals(_adUnitId) && showCompletionState.Equals(UnityAdsShowCompletionState.COMPLETED))
        {
            Debug.Log("Reward is being delivered now.");
            GameManager.Instance.maxTime += rewardedSeconds;
            GameManager.Instance.RestartGame();
        }
        Debug.Log("A rewarded ad is closing.");
        AdsManager.Instance.banner.Show();
    }

    public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
    {
        Debug.Log($"Error showing Ad Unit {adUnitID}: {error.ToString()} - {message}");
    }
    public void OnUnityAdsAdLoaded(string placementId)
    {
        adLoaded = true;
    }

#endif
}