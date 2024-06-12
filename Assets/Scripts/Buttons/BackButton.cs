using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackButton : MonoBehaviour
{
    // These variables get set from the intializer, depending on whether the user is on a phone, or computer.
    // The variables set by the initializer are static, so that all of the game objects using this script have the correct reference depending on the device being used.
    private static GameObject _gameCanvas;
    private static GameObject _creditsCanvas;
    private static GameObject _pauseCanvas;
    private static GameObject _endScreenPopup;

    public static GameObject GameCanvas
    {
        get => _gameCanvas;
        set => _gameCanvas = value;
    }

    public static GameObject CreditsCanvas
    {
        get => _creditsCanvas;
        set => _creditsCanvas = value;
    }

    public static GameObject PauseCanvas
    {
        get => _pauseCanvas;
        set => _pauseCanvas = value;
    }

    public static GameObject EndScreenPopup
    {
        get => _endScreenPopup;
        set => _endScreenPopup = value;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GoBack();
        }
    }

    public void GoBack()
    {
        if (_creditsCanvas != null && _creditsCanvas.activeSelf)
        {
            _creditsCanvas.SetActive(false);
        }

        if (_pauseCanvas != null && _pauseCanvas.activeSelf == true)
        {
            _pauseCanvas.SetActive(false);
        }

        if (_endScreenPopup != null && _endScreenPopup.activeSelf)
        {            
            GameManager.Instance.RestartGame();
        }

        if (_pauseCanvas != null && _pauseCanvas.activeSelf == true && _gameCanvas != null && _gameCanvas.activeSelf)
        {
            _pauseCanvas.SetActive(true);
            _gameCanvas.SetActive(false);
        }
        
    }

    
}
