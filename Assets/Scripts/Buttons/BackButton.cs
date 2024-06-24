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

    // This static method is used to initialize _gameCanvas from the initializer, while maintaining encapsulation.
    public static GameObject GameCanvas
    {
        set => _gameCanvas = value;
    }
    
    // This static method is used to initialize _creditsCanvas from the initializer, while maintaining encapsulation.
    public static GameObject CreditsCanvas
    {
        set => _creditsCanvas = value;
    }

    // This static method is used to initialize _pauseCanvas from the initializer, while maintaining encapsulation.
    // Although a pause canvas was not created, the functionality for one is made, so adding one in the future is possible (open for extension.)
    public static GameObject PauseCanvas
    {
        set => _pauseCanvas = value;
    }

    // This static method is used to initialize _endScreenPopup from the initializer, while maintaining encapsulation.
    public static GameObject EndScreenPopup
    {
        set => _endScreenPopup = value;
    }

    // I'm using the old input system and escape because the back key for android is configured as the escape button.
    // If the back button (or escape) is pressed, the GoBack function will be called.
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GoBack();
        }
    }

    // This function closes any UI that's open.
    // If the UI that was open was the endScreen, the game is restarted.
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
