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
    private bool subscribed = false;

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
    }

    private void OnEnable()
    {
        // Subscribe Setighscore to the TimesUpChanged event, so it will be called every time the time is up.
        if (GameManager.Instance != null && !subscribed)
        {
            GameManager.Instance.TimesUpIsTrue += CheckHighscore;
            subscribed = true;
            Debug.Log("check highscore subscribed to time is up event.");
        }
    }

    private void OnDisable()
    {
        subscribed = false;
        GameManager.Instance.TimesUpIsTrue -= CheckHighscore;
    }

    private void CheckHighscore()
    {
        if (ClickerBehaviour.Clicks > highscore)
        {
            SetNewHighscore();
        }
#if UNITY_ANDROID || UNITY_IOS
        else
        {
            AdsManager.Instance.interstitial.ShowInterstitial();
        }
#endif
    }

    private void SetNewHighscore()
    {
        highscore = ClickerBehaviour.Clicks;
        _highscoreNumber.text = highscore.ToString();
        SaveHighscore(highscore);
    }

    private void SaveHighscore(int newHighscore)
    {
        PlayerPrefs.SetInt("Highscore", newHighscore);
        PlayerPrefs.Save();
        return;
    }

    private int LoadHighscore()
    {
        return PlayerPrefs.GetInt("Highscore", 0);
    }

}
