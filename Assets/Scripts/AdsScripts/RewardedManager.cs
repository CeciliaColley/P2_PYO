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

    // Innitialize the Ad.
    internal void Initialize()
    {
        Advertisement.Load(adUnitID, this);
    }

    // Play the ad that was loaded.
    public void ShowRewardedAd()
    {
        if (adLoaded)
        {
            Advertisement.Show(adUnitID, this);
        }        
    }

    // Included for debugging purposes.
    public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
    {
        Debug.Log($"Rewarded Ad: Error loading Ad Unit: {adUnitID} - {error.ToString()} - {message}");
    }

    // Hide the banner so it doesn't cover the rewarded ad.
    public void OnUnityAdsShowStart(string _adUnitId)
    {
        Advertisement.Banner.Hide();
    }

    // Included for debuggin purposes and to showcase that I know thins function exists.
    public void OnUnityAdsShowClick(string _adUnitId)
    {
        Debug.Log("An rewarded ad was clicked on.");
    }

    //  Once the ad is completed, check if is was watched to completion.
    // If it was, add the rewarded seconds to the max time for the next match
    // Restart the game
    // Start showing the banner again and load the next rewarded ad.
    public void OnUnityAdsShowComplete(string _adUnitId, UnityAdsShowCompletionState showCompletionState)
    {
        if (_adUnitId.Equals(_adUnitId) && showCompletionState.Equals(UnityAdsShowCompletionState.COMPLETED))
        {
            GameManager.Instance.maxTime += rewardedSeconds;
            GameManager.Instance.RestartGame();
        }
        AdsManager.Instance.banner.Show();
        adLoaded = false;
        Advertisement.Load(adUnitID, this);
    }

    // Included for debugging purposes.
    public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
    {
        Debug.Log($"Error showing Ad Unit {adUnitID}: {error.ToString()} - {message}");
    }

    // Flag the ad as loaded if it's done succesfully. 
    public void OnUnityAdsAdLoaded(string placementId)
    {
        adLoaded = true;
    }

#endif
}