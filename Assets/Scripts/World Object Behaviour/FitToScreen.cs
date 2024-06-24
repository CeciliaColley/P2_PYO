using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class FitToScreen : MonoBehaviour
{
    [Tooltip ("Check this box if you want the objects scale in X to stretch to the width of the screen.")]
    [SerializeField] private bool fitScreenWidth;
    [Tooltip("Check this box if you want the objects scale in Y to stretch to the height of the screen.")]
    [SerializeField] private bool fitScreenHeight;
    [Tooltip("Chose the screen border you want this object to be positioned at.")]
    [SerializeField] private ScreenBorder borderPosition;
    private enum ScreenBorder { Bottom, Top, Left, Right }
    private float screenHeight;
    private float screenWidth;

    // When the game begins, if the object is active it will be stretched and positioned according to the input provided in the engine.
    void Start()
    {
        GetScreenInfo();
        ScaleToScreen();
        PositionAtScreenLimit();
    }

    // Uses the main camera to find the screens height and width.
    private void GetScreenInfo()
    {
        Camera mainCamera = Camera.main;
        if (mainCamera == null) { return; }

        screenHeight = 2f * mainCamera.orthographicSize;
        screenWidth = screenHeight * mainCamera.aspect;
    }

    // Equals the objects scale in X and Y to screen height, or width, or both, depending on the input provided in engine.
    void ScaleToScreen()
    {
        Vector3 newScale = transform.localScale;

        if (fitScreenWidth)
        {
            newScale.x = screenWidth;
        }

        if (fitScreenHeight)
        {
            newScale.y = screenHeight;
        }

        transform.localScale = newScale;
    }

    // Positions the object at the screens limit according to the input provided in engine.
    private void PositionAtScreenLimit()
    {
        switch (borderPosition)
        {
            case ScreenBorder.Bottom:
                transform.position = new Vector3(0, -screenHeight / 2, 0);
#if UNITY_WEBGL
                Vector3 scale = transform.localScale;
                scale.y = 1.38f;
                transform.localScale = scale;
#endif
                break;
            case ScreenBorder.Top:
                transform.position = new Vector3(0, screenHeight / 2, 0);
                break;
            case ScreenBorder.Left:
                transform.position = new Vector3(-screenWidth/2, 0, 0);
                break;
            case ScreenBorder.Right:
                transform.position = new Vector3(screenWidth / 2, 0, 0);
                break;
        }
    }
}