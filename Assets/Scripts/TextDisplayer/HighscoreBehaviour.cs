using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HighscoreBehaviour : MonoBehaviour
{
    // These variables get set from the intializer, depending on whether the user is on a phone, or computer.
    // The variables set by the initializer are static, so that all of the game objects using this script have the correct reference depending on the device being used.
    private static ClickerBehaviour _clickerBehaviour;
    private static TextMeshProUGUI _highscoreNumber;
    
    private int highscore = 0;

    public static ClickerBehaviour ClickerBehaviour
    {
        get => _clickerBehaviour;
        set => _clickerBehaviour = value;
    }
    public static TextMeshProUGUI HighscoreNumber
    {
        get => _highscoreNumber;
        set => _highscoreNumber = value;
    }

    private void Start()
    {

        // Load the highscore form the save file and print it to the screen.
        highscore = LoadHighscore();
        _highscoreNumber.text = highscore.ToString();        
        GameManager.Instance.TimesUpIsTrue += CheckHighscore;
        Debug.Log("check highscore eubscribed to time is up event.");
    }

    private void OnEnable()
    {
        // Subscribe Setighscore to the TimesUpChanged event, so it will be called every time the time is up.
        if (GameManager.Instance != null)
        {
            GameManager.Instance.TimesUpIsTrue += CheckHighscore;
            Debug.Log("check highscore eubscribed to time is up event.");
        }
    }

    private void OnDisable()
    {
        GameManager.Instance.TimesUpIsTrue -= CheckHighscore;
    }

    private void CheckHighscore()
    {
        if (ClickerBehaviour.Clicks > highscore)
        {
            SetNewHighscore();
        }
        else
        {
            AdsManager.Instance.interstitial.ShowInterstitial();
        }
    }

    private void SetNewHighscore()
    {
        highscore = ClickerBehaviour.Clicks;
        _highscoreNumber.text = highscore.ToString();
        SaveHighscore(highscore);
    }

    private void SaveHighscore(int newHighscore)
    {
#if UNITY_ANDROID
        PlayerPrefs.SetInt("Highscore", newHighscore);
        PlayerPrefs.Save();
#endif
        return;
    }

    private int LoadHighscore()
    {
#if UNITY_ANDROID
        return PlayerPrefs.GetInt("Highscore", 0);
#else
        // Load highscore from other platforms
        return 0;
#endif
    }

}
