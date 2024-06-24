using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class HighscoreBehaviour : MonoBehaviour
{
    // These variables get set from the intializer, depending on whether the user is on a phone, or computer.
    // The variables set by the initializer are static, so that all of the game objects using this script have the correct reference depending on the device being used.
    private static ClickerBehaviour _clickerBehaviour;
    private static TextMeshProUGUI _highscoreNumber;
    
    // This variable is used internally by the class to have a reference of the highscore in the save file.
    // This varibale is initialized in the Start method of this script.
    private int highscore = 0;

    // This static method is used to initialize _clickerBehaviour from the initializer, while maintaining encapsulation.
    public static ClickerBehaviour ClickerBehaviour
    {
        set => _clickerBehaviour = value;
    }

    // This static method is used to initialize _clickerBehaviour from the initializer, while maintaining encapsulation.
    public static TextMeshProUGUI HighscoreNumber
    {
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
        // Subscribe the ReevaluateHighscore function to the TimesUpChanged event, so every time TimesUpIsTrue, the highscore will be checked.
        if (GameManager.Instance != null)
        {
            GameManager.Instance.TimesUpIsTrue += ReevaluateHighscore;
        }
    }

    // Unsubscribe the even when disabled to avoid memory leaks.
    private void OnDisable()
    {
        GameManager.Instance.TimesUpIsTrue -= ReevaluateHighscore;
    }

    // This function looks at how many clicks were registered, and compares them to the highscore saved in this script.
    // If more clicks were registeres than the highscroe, a new highscore is set. If not, it the user us on an android or iOS device, an interstitial ad will be shown.
    private void ReevaluateHighscore()
    {
        if (_clickerBehaviour.Clicks > highscore)
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

    // This functions saves the new highscore to this script, and displays it to the user using a TextMeshProUGUI component that is intiialized by the initializer.
    // Then, the function calls the SaveHighscore function, passing it this script highscore variable.
    private void SetNewHighscore()
    {
        highscore = _clickerBehaviour.Clicks;
        _highscoreNumber.text = highscore.ToString();
        SaveHighscore(highscore);
    }

    // This function saves the highscore in player prefs under the key "Highscore". 
    // The new value of the highscore will be whatver is passed to this function as a parameter.
    private void SaveHighscore(int newHighscore)
    {
        PlayerPrefs.SetInt("Highscore", newHighscore);
        PlayerPrefs.Save();
    }

    // This function looks for the Highscore key in PlayerPrefs that stores an integer. It returns a default value of 0 if nothing is found.
    private int LoadHighscore()
    {
        return PlayerPrefs.GetInt("Highscore", 0);
    }

}
