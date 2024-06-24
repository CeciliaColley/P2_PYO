using System;
using System.Diagnostics.CodeAnalysis;
using TMPro;
using UnityEngine;

public class ClickerBehaviour : MonoBehaviour
{
    // These variables get set from the intializer, depending on whether the user is on a phone, or computer.
    // The variables set by the initializer are static, so that all of the game objects using this script have the correct reference depending on the device being used.
    private static TextMeshProUGUI _clicksNumberGameScreen;
    private static TextMeshProUGUI _clicksNumberEndScreen;
    private int clicks;

    // This event watches the clicks variable for any changes.
    public event Action ClickRegistered;

    // This static method is used to initialize _clicksNumberGameScreen from the initializer, while maintaining encapsulation.
    public static TextMeshProUGUI ClicksNumberGameScreen
    {
        set => _clicksNumberGameScreen = value;
    }

    // This static method is used to initialize _clicksNumberEndScreen from the initializer, while maintaining encapsulation.
    public static TextMeshProUGUI ClicksNumberEndScreen
    {
        set => _clicksNumberEndScreen = value;
    }

    // An observer that watches the value of clicks, and can be used to trigger events or call methods every time to clicks value is got or set.
    // When a click is registered, all of the methods subscribed to the ClicksChanged even will fire.
    public int Clicks
    {
        get => clicks;
        set
        {
            if (clicks != value)
            {
                clicks = value;
                OnClicksChanged();
            }
        }
    }

    
    // Made it protected and virtual so that you can add more kinds of clickers in the future, if you want to.
    protected virtual void OnClicksChanged()
    {
        ClickRegistered?.Invoke();
    }

    // When the object this script is attatched to (the clicker) is enabled, DisplayClicks and SpawnSprinkle are subscribed to the ClickRegistered event.
    // The amount of clicks is displayed to the game screen.
    private void OnEnable()
    {
        ClickRegistered += DisplayClicks;
        ClickRegistered += SpawnSprinkle;
        if (_clicksNumberGameScreen != null)
        {
            _clicksNumberGameScreen.text = Clicks.ToString();
        }
    }

    // Unsubscribing events to avoid memory leaks.
    private void OnDisable()
    {
        ClickRegistered -= DisplayClicks;
        ClickRegistered -= SpawnSprinkle;
    }

    // This function is called from the inspector as an on click event of the clicker button.
    // It check if the time is up, in which case it returns and nothing happens.
    // On the other hand, if the time was not up, it increases the clicks value by a unit of 1.
    public void RegisterClick()
    {
        if (GameManager.Instance.TimesUp)
        {
            return;
        }
        Clicks++;
    }

    // This function displays the amount of clicks to the correct TextMeshProUGUI of both the end screen and the game screen.
    private void DisplayClicks()
    {
        if (_clicksNumberGameScreen != null)
        {
            _clicksNumberGameScreen.text = Clicks.ToString();
            _clicksNumberEndScreen.text = Clicks.ToString();
        }
    }

    // Thus function activates a sprinkle from a pool of sprrinkles, and keeps track of how many sprinkles are active.
    private void SpawnSprinkle()
    {
        GameObject sprinkle = GameManager.Instance.sprinkles[GameManager.Instance.activeSprinkles];
        if (sprinkle.activeSelf == true)
        {
            sprinkle.SetActive(false);
        }
        sprinkle.SetActive(true);
        GameManager.Instance.activeSprinkles++;
        if (GameManager.Instance.activeSprinkles >= GameManager.Instance.maxSprinkles)
        {
            GameManager.Instance.activeSprinkles = 0;
        }
    }
}