using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Made by Connor-chan

public class ControllerManager : MonoBehaviour
{
    public Texture2D controllerNotDetected;

    public bool pS4Controller;                   //I'm setting up bools for different controllers so we can change some UI elements depending on whats plugged in
    public bool xBOXController;
    public bool controllerDetected;

    public static bool startUpFinished;        //Might be confusing. Its just a bool for when the start up is finished

    void Awake()
    {
        pS4Controller = false;
        xBOXController = false;
        controllerDetected = false;

        startUpFinished = false;
    }

	void Start ()
    {
        DontDestroyOnLoad(this);        //The controller manager game object MUST remain active regardless of what scene is loaded

	}

	void Update ()
    {
	}

    void LateUpdate()
    {
        string[] joyStickNames = Input.GetJoystickNames();

        for (int js = 0; js < joyStickNames.Length; js++)
        {
            if (joyStickNames[js].Length == 19)
            {
                pS4Controller = true;
                controllerDetected = true;

            }
            if (joyStickNames[js].Length == 33)
            {
                xBOXController = true;
                controllerDetected = true;

            }

            if (joyStickNames[js].Length != 0)
                return;

            if (string.IsNullOrEmpty(joyStickNames[js]))
                controllerDetected = false;

        }
    }

    private void OnGUI()
    {
        if (startUpFinished == false)
            return;

        if (controllerDetected == true)
            return;

        if (controllerDetected == false)
            GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), controllerNotDetected);


    }
}

