using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//Started by Connor on 26/07/2018

//Tells script to attach audio source
[RequireComponent(typeof(AudioSource))]

public class Splashscreen : MonoBehaviour
{
    public Texture2D splashScreenBackground;
    public Texture2D splashScreenText;

    private AudioSource splashScreenAudio;
    public AudioClip splashScreenMusic;

    private float splashScreenFadeValue;
    private float splashScreenFadeSpeed = 0.20f;

    private SplashScreenController splashScreenController;

    private enum SplashScreenController
    {
        SplashScreenFadeIn = 0,
        SplashScreenFadeOut = 1

    }

    void Awake()
    {
        splashScreenFadeValue = 0;

    }

	void Start ()
    {
        //Change later!!!!
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        splashScreenAudio = GetComponent < AudioSource >();

        splashScreenAudio.volume = 0;
        splashScreenAudio.clip = splashScreenMusic;
        splashScreenAudio.loop = true;
        splashScreenAudio.Play();

        splashScreenController = Splashscreen.SplashScreenController.SplashScreenFadeIn;

        StartCoroutine("SplashScreenManager");  //Starts the SplashScreenManager function
	}
	

	void Update ()
    {
		

	}

    private IEnumerator SplashScreenManager()
    {
        while (true)
        {
            switch (splashScreenController)
            {
                case SplashScreenController.SplashScreenFadeIn:
                    SplashScreenFadeIn();
                    break;
                case SplashScreenController.SplashScreenFadeOut:
                    SplashScreenFadeOut();
                    break;

            }
            yield return null;
        }
    }

    private void SplashScreenFadeIn()
    {
        Debug.Log("SplashScreenFadeIn");

        splashScreenAudio.volume += splashScreenFadeSpeed * Time.deltaTime;  //this changes volume as the text fades
        splashScreenFadeValue += splashScreenFadeSpeed * Time.deltaTime;

        if (splashScreenFadeValue > 1)
            splashScreenFadeValue = 1;

        if (splashScreenFadeValue == 1)
            splashScreenController = Splashscreen.SplashScreenController.SplashScreenFadeOut;
    }
    private void SplashScreenFadeOut()
    {
        Debug.Log("SplashScreenFadeOut");

        splashScreenAudio.volume -= splashScreenFadeSpeed * Time.deltaTime;  
        splashScreenFadeValue -= splashScreenFadeSpeed * Time.deltaTime;


        if (splashScreenFadeValue < 0)
            splashScreenFadeValue = 0;

        if (splashScreenFadeValue == 0)
            SceneManager.LoadScene("ControllerStuff");
    }

    private void OnGUI()
    {
        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), splashScreenBackground);

        GUI.color = new Color(1, 1, 1, splashScreenFadeValue);

        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), splashScreenText);
    }
}
