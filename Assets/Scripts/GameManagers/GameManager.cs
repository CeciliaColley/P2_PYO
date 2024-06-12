using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public static GameManager Instance { get; private set; }
    [HideInInspector]
    public bool timesUp = false;
    //HighscoreBehaviour subscribes setHighscore to this event, so that when the time is up, the highscore is set.
    public event Action TimesUpIsTrue;

    // This class is a singleton.
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
        TimesUpIsTrue += EndGame;
        StartCoroutine(WaitForFirstClick());
    }

    private IEnumerator WaitForFirstClick()
    {
        yield return new WaitUntil(() => buttonBehaviour.Clicks > 0);
        StartCoroutine(timer.CountToMaxTime());
    }

    public void EndGame()
    {
        gameCanvas.SetActive(false);
        endScreenPopup.SetActive(true);
    }

    public void RestartGame()
    {
        if (gameCanvas != null && !gameCanvas.activeSelf)
        {
            creditsCanvas.SetActive(false);
            endScreenPopup.SetActive(false);
            pauseCanvas.SetActive(false);
            gameCanvas.SetActive(true);

            timer.CurrentTime = 0;
            timesUp = false;
            activeSprinkles = 0;
            buttonBehaviour.Clicks = 0;
            foreach (GameObject sprinkle in sprinkles)
            {
                sprinkle.SetActive(false);
            }
            StartCoroutine(WaitForFirstClick());
        }
    }

    // TimesUp is used by Timer to set timesUp to true.
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
    protected virtual void OnTimesUpIsTrue()
    {
        TimesUpIsTrue?.Invoke();
    }
    
}
