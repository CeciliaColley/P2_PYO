using System;
using System.Collections;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    // These variables get set from the intializer, depending on whether the user is on a phone, or computer.
    // The variables set by the initializer are static, so that all of the game objects using this script have the correct reference depending on the device being used.
    private static TextMeshProUGUI _timeText;
    
    // This variable is used internally by the class to keep track of the time.
    private float _time = 0.0f;

    // This event watches the _time variable and invokes methods if it changes.
    private event Action TimeChanged;

    // This static method is used to initialize _timeText from the initializer, while maintaining encapsulation.
    public static TextMeshProUGUI TimeText
    {
        set { _timeText = value; }
    }

    // An observer of _time that can be used to trigger events when the value is got or set.
    public float CurrentTime
    {
        get => _time;
        set
        {
            // If the time changes by a very small amount (epsilon), call the OnTimeChanged method.
            if (Math.Abs(_time - value) > Mathf.Epsilon)
            {
                _time = value;
                OnTimeChanged();
            }
        }
    }

    // Subscribing Display Time to the Time Changed event at the beginning of the program.
    private void Start()
    {
        TimeChanged += DisplayTime;
    }

    //This function invokes all of the events subsribed to the TimeChanged event
    protected virtual void OnTimeChanged()
    {
        TimeChanged?.Invoke();
    }

    // This enumerator counts to the maxTime of GameManager, and marks GameManager's TimesUp value as true.
    public IEnumerator CountToMaxTime()
    {
        while (CurrentTime < GameManager.Instance.maxTime)
        {
            yield return null;
            CurrentTime += Time.deltaTime;
        }
        GameManager.Instance.TimesUp = true;
    }

    // This function uses a TextMeshProUGUI component that the initializer sets up to display the time.
    // This function is subscribed to the TimeChanged event, so every time the time changes, this function will be called.
    private void DisplayTime()
    {
        if (_timeText != null)
        {
            _timeText.text = CurrentTime.ToString("F1");
        }
    }
}
