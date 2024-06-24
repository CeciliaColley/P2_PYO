using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This class follows the Singleton design pattern.
public class GameManager : MonoBehaviour
{
    // These variables must be set up in the inspector
    public int maxTime = 10;
    public int maxSprinkles = 100;
    public List<GameObject> sprinkles = new List<GameObject>();

    // These variables are initialized by the initializer. They don't have to be static because this class is a singleton.
    [HideInInspector] public int activeSprinkles = 0;
    [HideInInspector]
    public GameObject pauseCanvas;
    [HideInInspector]
    public GameObject endScreenPopup;
    [HideInInspector]
    public GameObject gameCanvas;
    [HideInInspector]
    public GameObject creditsCanvas;
    [HideInInspector]
    public ClickerBehaviour buttonBehaviour;
    [HideInInspector]
    public Timer timer;

    //These variables are used internally, and are set at run time.
    // This boolean check is the timer has counted to the max time or not.
    [HideInInspector]
    public bool timesUp = false;
    // This even is triggered by the OnTimesUpIsTrue method, which is called every time the value of timesUp is set to true.
    // The ReevaluateHighscore method in HighscoreBehaviour is subscribed to this event. In other words, when the time is up, the highscore will be reevaluated. 
    // The EndGame method in GameManager (this script) is also subscribed to this event. In other words, when time is up, the game will end. 
    // The CloseCredits method in Credits button is subscribed to this event. In other words, every time times up is true, the credits will be closed.
    public event Action TimesUpIsTrue;
    private int _maxTime;

    // Singleton pattern
    public static GameManager Instance { get; private set; }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        // Save what the original setting of maxTime was
        _maxTime = maxTime;
        // Subscribe the EndGame method to the TimeUpIsTrue event.
        TimesUpIsTrue += EndGame;
        //Wait for the first click.
        StartCoroutine(WaitForFirstClick());
    }

    // This coroutine waits until the first click is registered, and then starts the timer.
    private IEnumerator WaitForFirstClick()
    {
        yield return new WaitUntil(() => buttonBehaviour.Clicks > 0);
        StartCoroutine(timer.CountToMaxTime());
    }

    // This function ends the game by deactivating the game screen, and activating the end screen. After that it resets max time to it's original value.
    public void EndGame()
    {
        gameCanvas.SetActive(false);
        endScreenPopup.SetActive(true);
        if (maxTime > _maxTime)
        {
            maxTime = _maxTime;
        }
    }

    // This function restarts the game by changing the ammount of clicks back to 0, restarting the UI, turning off all the sprinkles, and waiting for the first click to be registered again.
    public void RestartGame()
    {
        buttonBehaviour.Clicks = 0;
        RestartUI();
        RestartTime();
        RestartSprinkles();
        StartCoroutine(WaitForFirstClick());
    }

    // This function closes all of the UI that isn't supposed to be active while playing the game.
    private void RestartUI()
    {
        if (gameCanvas != null && !gameCanvas.activeSelf)
        {
            creditsCanvas.SetActive(false);
            endScreenPopup.SetActive(false);
            pauseCanvas.SetActive(false);
            gameCanvas.SetActive(true);
        }  
    }

    // This function iterates through the sprinkle pool, deactivating all of the sprinkles and setting active sprinkles back to 0.
    private void RestartSprinkles()
    {
        activeSprinkles = 0;
        foreach (GameObject sprinkle in sprinkles)
        {
            sprinkle.SetActive(false);
        }
    }

    // This function restarts the timer by setting it back to 0 and making sure timesUp is false again.
    private void RestartTime()
    {
        timer.CurrentTime = 0;
        timesUp = false;
    }

    // An observer of timesUp that can be used to trigger events when the value is got or set.
    // In this case, every time timeUp's value is set to true, the OnTimesUpIsTrue method is called.
    public bool TimesUp
    {
        get => timesUp;
        set
        {
            if (timesUp != value)
            {
                timesUp = value;
                if (timesUp == true)
                {
                    OnTimesUpIsTrue();
                }                
            }
        }
    }

    // This function invokes all of the functions that are subscribed to the TimesUpIsTrue event.
    protected virtual void OnTimesUpIsTrue()
    {
        TimesUpIsTrue?.Invoke();
    }
    
}
