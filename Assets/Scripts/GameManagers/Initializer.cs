using UnityEngine;
using TMPro;

public class Initializer : MonoBehaviour
{

    [Header("REFERENCES FOR WEB")]
    [Header("References To canvases")]
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
        SprinkleBehaviour.startingPosition = FindButtonWorldPosition();
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

        WatchAdButton.EndScreen = endScreenPopupAndroid;

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

        WatchAdButton.EndScreen = endScreenPopupWeb;
#endif
    }

    private Vector3 FindButtonWorldPosition()
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

        Vector3 buttonPosition = new Vector3();
        if (clicker != null)
        {
            buttonPosition = Camera.main.ScreenToWorldPoint(clicker.transform.position);
            buttonPosition.z = 0;
        }
        return buttonPosition;
    }
}