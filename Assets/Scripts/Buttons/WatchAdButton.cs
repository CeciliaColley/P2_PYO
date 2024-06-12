using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WatchAdButton : MonoBehaviour
{
    //These variables must be set from the inspector.
    public int rewardedSeconds = 0;

    // These variables get set from the intializer, depending on whether the user is on a phone, or computer.
    // The variables set by the initializer are static, so that all of the game objects using this script have the correct reference depending on the device being used.
    private static GameObject _endScreen;
    public static GameObject EndScreen
    {
        get => _endScreen;
        set => _endScreen = value;
    }

    private void OnEnable()
    {
        if (GameManager.Instance.highscoreSurepassed)
        {
            gameObject.SetActive(false);
        }
    }

    public void RewardedAdButton()
    {
        _endScreen.SetActive(false);
        // Play Ad
        // When add is done do this: 

        GameManager.Instance.maxTime += rewardedSeconds;
        GameManager.Instance.RestartGame();
    }

    private void PlayAd()
    {

    }
}
