using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class FitToScreen : MonoBehaviour
{
    [SerializeField] private bool fitScreenWidth;
    [SerializeField] private bool fitScreenHeight;
    [SerializeField] private ScreenBorder borderPosition;
    private enum ScreenBorder { Bottom, Top, Left, Right }
    private float screenHeight;
    private float screenWidth;

    void Start()
    {
        GetScreenInfo();
        ScaleToScreen();
        PositionAtScreenLimit();
    }

    private void GetScreenInfo()
    {
        Camera mainCamera = Camera.main;
        if (mainCamera == null) { return; }

        screenHeight = 2f * mainCamera.orthographicSize;
        screenWidth = screenHeight * mainCamera.aspect;
    }

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

    private void PositionAtScreenLimit()
    {
        switch (borderPosition)
        {
            case ScreenBorder.Bottom:
                transform.position = new Vector3 (0, -screenHeight / 2, 0);
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