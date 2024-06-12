using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsButton : MonoBehaviour
{
    private static GameObject _creditsCanvas;
    private static GameObject _endScreen;

    public static GameObject CreditsCanvas
    {
        get => _creditsCanvas;
        set => _creditsCanvas = value;
    }

    public static GameObject EndScreen
    {
        get => _endScreen;
        set => _endScreen = value;
    }

    private void Start()
    {
        GameManager.Instance.TimesUpIsTrue += CloseCredits;
    }
    public void OnClickCreditsButton()
    {
        if (_endScreen.activeSelf == false)
        {
            _creditsCanvas.SetActive(true);
        }
    }

    private void CloseCredits()
    {
        _creditsCanvas.SetActive(false);
    }
}
