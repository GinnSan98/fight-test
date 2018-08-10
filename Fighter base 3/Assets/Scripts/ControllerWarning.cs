using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//made by Connor-tan on 10/08/2018
public class ControllerWarning : ControllerManager
{

    public Texture2D controllerWarningBackground;
    public Texture2D controllerWarningText;
    public Texture2D controllerDetectedText;

    public float controllerWarningFadeValue;
    private float controllerWarningFadeSpeed = 0.25f;
    private bool controllerConditionsMet;

	void Start ()
    {
        controllerWarningFadeValue = 1;
        controllerConditionsMet = false;
	}
	
	void Update ()
    {
        if (controllerDetected == true)
            StartCoroutine("WaitToLoadMainMenu");

        if (controllerConditionsMet == false)
            return;

        if(controllerConditionsMet == true)
        {
            controllerWarningFadeValue -= controllerWarningFadeSpeed * Time.deltaTime;
        }

        if (controllerWarningFadeValue < 0)
            controllerWarningFadeValue = 0;

        if (controllerWarningFadeValue == 0)
        {
            startUpFinished = true;
            SceneManager.LoadScene("MainMenu");
        }
    }

    private IEnumerator WaitToLoadMainMenu()
    {
        yield return new WaitForSeconds(2);

        controllerConditionsMet = true;
    }

    private void OnGUI()
    {
        GUI.DrawTexture(new Rect (0, 0, Screen.width, Screen.height), controllerWarningBackground);
    }
}
