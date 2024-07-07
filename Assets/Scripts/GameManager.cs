using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public Wolf_City city;
    public Language game_language;
    public Texture2D screenshot;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {

    }

    public void ScreenShot()
    {
        StartCoroutine("TakeScreenshot");
    }

    private IEnumerator TakeScreenshot()
    {
        yield return new WaitForEndOfFrame(); // Wait for the end of the frame to capture the screenshot

        // Capture the screenshot
        screenshot = ScreenCapture.CaptureScreenshotAsTexture();

        
    }
}