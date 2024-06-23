using System;
using System.Collections;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    // These variables get set from the intializer, depending on whether the user is on a phone, or computer.
    // The variables set by the initializer are static, so that all of the game objects using this script have the correct reference depending on the device being used.
    private static TextMeshProUGUI _timeText;
    
    private float _time = 0.0f;

    // This event watches the _time variable and invokes methods if it changes.
    private event Action TimeChanged;

    public static TextMeshProUGUI TimeText
    {
        get => _timeText;
        set { _timeText = value; }
    }

    public float CurrentTime
    {
        get => _time;
        set
        {
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

    protected virtual void OnTimeChanged()
    {
        TimeChanged?.Invoke();
    }

    public IEnumerator CountToMaxTime()
    {
        while (CurrentTime < GameManager.Instance.maxTime)
        {
            yield return null;
            CurrentTime += Time.deltaTime;
        }
        GameManager.Instance.TimesUp = true;
    }

    private void DisplayTime()
    {
        if (_timeText != null)
        {
            _timeText.text = CurrentTime.ToString("F1");
        }
    }
}
