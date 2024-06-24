using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class BannerManager : MonoBehaviour
{
#if UNITY_ANDROID || UNITY_IOS
    [SerializeField] string androidAdUnitID = "Banner_Android";
    [SerializeField] string iOSAdUnitID = "Banner_iOS";
    string adUnitID = null;

    // Start is called before the first frame update
    void Start()
    {
#if UNITY_IOS
        adUnitID = iOSAdUnitID;
#elif UNITY_ANDROID
        adUnitID = androidAdUnitID;
#endif
    }

    public void Show()
    {
        // Callbacks for when the banner is loading
        BannerLoadOptions options = new BannerLoadOptions
        {
            loadCallback = OnBannerLoaded,
            errorCallback = OnBannerError
        };

        // Set the position of the banner and load the ad
        Advertisement.Banner.SetPosition(BannerPosition.TOP_RIGHT);
        Advertisement.Banner.Load(adUnitID, options);
    }

    void OnBannerLoaded()
    {
        Advertisement.Banner.Show(adUnitID);
    }

    // Included for debugging purposes
    void OnBannerError(string message)
    {
        Debug.Log($"Banner Error: {message}");
    }
#endif
}