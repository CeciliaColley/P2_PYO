using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsButton : MonoBehaviour
{
    // These variables get set from the intializer, depending on whether the user is on a phone, or computer.
    // The variables set by the initializer are static, so that all of the game objects using this script have the correct reference depending on the device being used.
    private static GameObject _creditsCanvas;
    private static GameObject _endScreen;

    // This static method is used to initialize _creditsCanvas from the initializer, while maintaining encapsulation.
    public static GameObject CreditsCanvas
    {
        set => _creditsCanvas = value;
    }

    // This static method is used to initialize _endScreen from the initializer, while maintaining encapsulation.
    public static GameObject EndScreen
    {
        set => _endScreen = value;
    }

    // Subscribe the Close credits function to the TimesUpIsTrue event.
    private void Start()
    {
        GameManager.Instance.TimesUpIsTrue += CloseCredits;
    }

    // Unsubscribing events to avoid memory leaks.
    private void OnDestroy()
    {
        GameManager.Instance.TimesUpIsTrue += CloseCredits;
    }

    // This function is called from the inspector as an on click event of the credits button.
    // It check if the endScreen is false as condition for activating the credits canvas.
    // In other words, you cannot view the credits screen from the endscreen.
    public void OnClickCreditsButton()
    {
        if (_endScreen.activeSelf == false)
        {
            _creditsCanvas.SetActive(true);
        }
    }

    // This function closes the credits screen, and is subscribed to the TimesUpIsTrue event.
    // In other words, every time time is up, the credits canvas will close.
    private void CloseCredits()
    {
        _creditsCanvas.SetActive(false);
    }
}
