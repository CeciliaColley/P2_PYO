using UnityEngine;
using TMPro;
using System.Collections;

public class Initializer : MonoBehaviour
{

    [Header("REFERENCES FOR WEB")]
    [Header("References To canvases")]
    [SerializeField] private GameObject webUI;
    [SerializeField] private GameObject gameCanvasWeb;
    [SerializeField] private GameObject creditsCanvasWeb;
    [SerializeField] private GameObject pauseCanvasWeb;
    [SerializeField] private GameObject endScreenPopupWeb;
    [Header("References to text displayers")]
    [SerializeField] private TextMeshProUGUI clicksNumberGameScreenWeb;
    [SerializeField] private TextMeshProUGUI clicksNumberEndScreenWeb;
    [SerializeField] private TextMeshProUGUI highscoreNumberWeb;
    [SerializeField] private TextMeshProUGUI timeTextWeb;
    [Header("References to Clicker")]
    [SerializeField] private GameObject clickerWeb;
    [SerializeField] private ClickerBehaviour clickerBehaviourWeb;
    [Header("References to Timer")]
    [SerializeField] private Timer timerWeb;
    [Space(10)]
    [Header("REFERENCES FOR ANDROID")]
    [Header("References to canvases")]
    [SerializeField] private GameObject androidUI;
    [SerializeField] private GameObject gameCanvasAndroid;
    [SerializeField] private GameObject creditsCanvasAndroid;
    [SerializeField] private GameObject pauseCanvasAndroid;
    [SerializeField] private GameObject endScreenPopupAndroid;
    [Header("References to text displayers")]
    [SerializeField] private TextMeshProUGUI clicksNumberGameScreenAndroid;
    [SerializeField] private TextMeshProUGUI clicksNumberEndScreenAndroid;
    [SerializeField] private TextMeshProUGUI timeTextAndroid;
    [SerializeField] private TextMeshProUGUI highscoreNumberAndroid;
    [Header("References to Clicker")]
    [SerializeField] private GameObject clickerAndroid;
    [SerializeField] private ClickerBehaviour clickerBehaviourAndroid;
    [Header("References to Timer")]
    [SerializeField] private Timer timerAndroid;

    private void Awake()
    {
        StartCoroutine(FindButtonWorldPosition());
        
#if UNITY_ANDROID
        BackButton.GameCanvas = gameCanvasAndroid;
        BackButton.CreditsCanvas = creditsCanvasAndroid;
        BackButton.PauseCanvas = pauseCanvasAndroid;
        BackButton.EndScreenPopup = endScreenPopupAndroid;

        ClickerBehaviour.ClicksNumberEndScreen = clicksNumberEndScreenAndroid;
        ClickerBehaviour.ClicksNumberGameScreen = clicksNumberGameScreenAndroid;

        HighscoreBehaviour.ClickerBehaviour = clickerBehaviourAndroid;
        HighscoreBehaviour.HighscoreNumber = highscoreNumberAndroid;

        Timer.TimeText = timeTextAndroid;

        GameManager.Instance.pauseCanvas = pauseCanvasAndroid;
        GameManager.Instance.endScreenPopup = endScreenPopupAndroid;
        GameManager.Instance.gameCanvas = gameCanvasAndroid;
        GameManager.Instance.creditsCanvas = creditsCanvasAndroid;
        GameManager.Instance.buttonBehaviour = clickerBehaviourAndroid;
        GameManager.Instance.timer = timerAndroid;

        CreditsButton.CreditsCanvas = creditsCanvasAndroid;
        CreditsButton.EndScreen = endScreenPopupAndroid;

#elif UNITY_WEBGL
        BackButton.GameCanvas = gameCanvasWeb;
        BackButton.CreditsCanvas = creditsCanvasWeb;
        BackButton.PauseCanvas = pauseCanvasWeb;
        BackButton.EndScreenPopup = endScreenPopupWeb;

        ClickerBehaviour.ClicksNumberEndScreen = clicksNumberEndScreenWeb;
        ClickerBehaviour.ClicksNumberGameScreen = clicksNumberGameScreenWeb;

        HighscoreBehaviour.ClickerBehaviour = clickerBehaviourWeb;
        HighscoreBehaviour.HighscoreNumber = highscoreNumberWeb;

        Timer.TimeText = timeTextWeb;

        GameManager.Instance.pauseCanvas = pauseCanvasWeb;
        GameManager.Instance.endScreenPopup = endScreenPopupWeb;
        GameManager.Instance.gameCanvas = gameCanvasWeb;
        GameManager.Instance.creditsCanvas = creditsCanvasWeb;
        GameManager.Instance.buttonBehaviour = clickerBehaviourWeb;
        GameManager.Instance.timer = timerWeb;

        CreditsButton.CreditsCanvas = creditsCanvasWeb;
        CreditsButton.EndScreen = endScreenPopupWeb;
#endif
    }

    private void Start()
    {
#if UNITY_WEBGL
        webUI.SetActive(true);
#elif UNITY_ANDROID || UNITY_IOS
        androidUI.SetActive(true);
#endif
    }

    private IEnumerator FindButtonWorldPosition()
    {
        GameObject clicker = null;

#if UNITY_ANDROID
        if (clickerAndroid != null)
        {
            clicker = clickerAndroid;
        }
#endif
#if UNITY_WEBGL
if (clickerWeb != null)
        {
            clicker = clickerWeb;
        }
#endif
        yield return new WaitUntil(() => clicker.activeSelf == true);

        Vector3 buttonPosition = new Vector3();
        if (clicker != null)
        {
            buttonPosition = Camera.main.ScreenToWorldPoint(clicker.transform.position);
            buttonPosition.z = 0;
            Debug.Log("Button position : " + buttonPosition);
        }
        SprinkleBehaviour.startingPosition = buttonPosition;
    }
}