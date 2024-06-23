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
    public event Action ClicksChanged;

    public static TextMeshProUGUI ClicksNumberGameScreen
    {
        get => _clicksNumberGameScreen;
        set => _clicksNumberGameScreen = value;
    }

    public static TextMeshProUGUI ClicksNumberEndScreen
    {
        get => _clicksNumberEndScreen;
        set => _clicksNumberEndScreen = value;
    }

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
        ClicksChanged?.Invoke();
    }


    private void OnEnable()
    {
        ClicksChanged += DisplayClicks;
        ClicksChanged += SpawnSprinkle;
    }

    private void OnDisable()
    {
        ClicksChanged -= DisplayClicks;
        ClicksChanged -= SpawnSprinkle;
    }

    // This is the method assigned as the buttons event in the inspector.
    public void RegisterClick()
    {
        if (GameManager.Instance.TimesUp)
        {
            return;
        }
        Clicks++;
    }

    private void DisplayClicks()
    {
        if (_clicksNumberGameScreen != null)
        {
            _clicksNumberGameScreen.text = Clicks.ToString();
            _clicksNumberEndScreen.text = Clicks.ToString();
        }
    }

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