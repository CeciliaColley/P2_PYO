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
    }

    private void SetHighscore()
    {
        Debug.Log("Setting high score.");
        if (ClickerBehaviour.Clicks > highscore)
        {
            Debug.Log("Clicks: " + ClickerBehaviour.Clicks);
            Debug.Log("Highscore: " + highscore);
            GameManager.Instance.highscoreSurepassed = true;
            Debug.Log("Highscore surpassed: " + GameManager.Instance.highscoreSurepassed);
            highscore = ClickerBehaviour.Clicks;
            Debug.Log("New highscore: " + highscore);
            _highscoreNumber.text = highscore.ToString();
            Debug.Log("To text.");
#if UNITY_ANDROID
            {
                SaveHighscore(highscore);
            }
# endif
        }

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

    private void OnEnable()
    {        
        // Subscribe Setighscore to the TimesUpChanged event, so it will be called every time time is up.
        GameManager.Instance.TimesUpIsTrue += SetHighscore;
    }

    private void OnDisable()
    {
        GameManager.Instance.TimesUpIsTrue -= SetHighscore;
    }
}
