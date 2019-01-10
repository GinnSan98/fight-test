using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//Created by Connor-Senpai on 29/09/2018

[RequireComponent(typeof(AudioSource))]

public class MainMenu : MonoBehaviour
{
    public int selectedButton = 0;                                  //refers to button that is selected in the GUI
    public float timeBetweenButtonPress = 0f;
    public float timeDelay;

    public float mainMenuVInputTimer;                              //verticle input timer
    public float mainMenuVInputDelay = 0.001f;

    public Texture2D mainMenuBG;
    public Texture2D mMTitle;

    private AudioSource mainMenuSound;
    public AudioClip mainMenuOST;
    public AudioClip mainMenuStartSFX;
    public AudioClip mainMenuQuitSFX;

    public float mainMenuFadeValue;
    public float mainMenuFadeSpeed = 0.1f;

    public float mainMenuButtonWidth = 200f;
    public float mainMenuButtonHeight = 60f;
    public float mainMenuGUIOffset = 10f;

    private bool startingOnePLayerGame;
    private bool startingTwoPLayerGame;
    private bool startingThreePLayerGame;
    private bool startingFourPLayerGame;
    private bool quittingGame;
    public int mainMenuFailsafe;
    private bool ps4Controller;                                    //self explanatory. Tells the game that the player has a PS4 controller connected
    private bool xBOXController;

    private string[] mainMenuButtons = new string[]
    {
        "onePlayer", "twoPlayer", "threePlayer", "fourPlayer", "quit"
    };

    //private MainMenuController _mainMenuController;
    
    /*private enum MainMenuController
    {
        MainMenuFadeIn = 0,
        MainMenuAtIdle = 1,
        MainMenuFadeOut = 2
         
    }*/

	void Start ()
    {
        startingOnePLayerGame = false;
        //startingTwoPLayerGame = false;
        //startingThreePLayerGame = false;
        //startingFourPLayerGame = false;
        quittingGame = false;

        ps4Controller = false;                                 //ps4 controller is false by default and will be true if one is connected
        xBOXController = false;

        
        mainMenuSound = GetComponent<AudioSource>();

        mainMenuSound.volume = 0;                                               //audio volume at 0 at startup in order to fade it in
        mainMenuSound.clip = mainMenuOST;
        mainMenuSound.loop = true;
        mainMenuSound.Play();

        mainMenuFailsafe = 0;
       
        //_mainMenuController = MainMenu.MainMenuController.MainMenuFadeIn;      


        //StartCoroutine("MainMenuManager");                                      
	}
	

	void Update ()
    {
        if(mainMenuFadeValue < 1)
        {
  
        }

        if (mainMenuFailsafe == 0)
        {
            MainMenuFadeIn();
        }
        if (mainMenuFailsafe == 1)
        {
            MainMenuAtIdle();

        }
        if (mainMenuFailsafe == 2)
        {
            MainMenuFadeOut();
        }
        if(quittingGame == true)
        {
            Application.Quit();
        }
        string[] controllerNames = Input.GetJoystickNames();   //array of controllers. This is built into unity so its an easy thing
        for(int cn = 0; cn < controllerNames.Length; cn++)   //cn equals the length of the controller names array. If it doesn't then the value will rise until it does
        {
            if (controllerNames[cn].Length == 0)
                return;

            if (controllerNames[cn].Length == 19)        //19 may seem random but its the in-built code for recognising a PS4 controller
                ps4Controller = true;
            if (controllerNames[cn].Length == 33)        //33 is the code for xbox, by some black magic miracle
                xBOXController = true;

        }
        if (mainMenuVInputTimer > 0)
            mainMenuVInputTimer -= 1f * Time.deltaTime;

        if (Input.GetAxis("Vertical") > 0f && selectedButton == 0)   //This can be a bit confusing. If input is verticle and positive AND selected button is 0
            return;                                                  //then literally do nothing

        if(Input.GetAxis("Vertical") > 0f && selectedButton == 1)   //This one actually does something
        {                                                     
            if (mainMenuVInputTimer > 0)                            //So this prevents people from navigating menus
                return;                                             //Too quickly, its just polish 

            mainMenuVInputTimer = mainMenuVInputDelay;
            selectedButton = 0;            
        }


        if (Input.GetAxis("Vertical") > 0f && selectedButton == 2)   //This one actually does something
        {
            if (mainMenuVInputTimer > 0)                            //So this prevents people from navigating menus
                return;                                             //Too quickly, its just polish 

            mainMenuVInputTimer = mainMenuVInputDelay;
            selectedButton = 1;
        }

        if (Input.GetAxis("Vertical") < 0f && selectedButton == 2)   //This can be a bit confusing. If input is verticle and negative AND selected button is 0
            return;                                                  //then literally do nothing

        if (Input.GetAxis("Vertical") < 0f && selectedButton == 0)   //This one actually does something
        {
            if (mainMenuVInputTimer > 0)                             //So this prevents people from navigating menus
                return;                                              //Too quickly, its just polish 

            mainMenuVInputTimer = mainMenuVInputDelay;
            selectedButton = 1;
        }

        if (Input.GetAxis("Vertical") < 0f && selectedButton == 1)   //This one actually does something
        {
            if (mainMenuVInputTimer > 0)                             //So this prevents people from navigating menus
                return;                                              //Too quickly, its just polish 

            mainMenuVInputTimer = mainMenuVInputDelay;
            selectedButton = 2;
        }


    }


    /*private IEnumerable MainMenuManager()
    {
        while (true)
        {
            switch (_mainMenuController)
            {
                case MainMenuController.MainMenuFadeIn:
                    MainMenuFadeIn();
                    break;
                case MainMenuController.MainMenuAtIdle:
                    MainMenuAtIdle();
                    break;
                case MainMenuController.MainMenuFadeOut:
                    MainMenuFadeOut();
                    break;
            }

            yield return null;

        }
    }*/
  

    private void MainMenuFadeIn()
    {
        Debug.Log("MainMenuFadeIn");

        mainMenuSound.volume += mainMenuFadeSpeed * Time.deltaTime / 8; //the volume will increase as the menu fades in
        if(mainMenuFadeValue <= 1)     //fade value increases by the fade speed
        {
            mainMenuFadeValue += mainMenuFadeSpeed * Time.deltaTime;
        }
            
        if (mainMenuFadeValue > 1)
            mainMenuFadeValue = 1;   //This is to ensure that the value never exceeds 1, if it does it will be made equal to 1.

        if (mainMenuFadeValue == 1)
        {
            //_mainMenuController = MainMenu.MainMenuController.MainMenuAtIdle;              //the menu is now idle. Until player input that is
            mainMenuFailsafe = 1;
        }
    }
    private void MainMenuAtIdle()
    {
        Debug.Log("MainMenuAtIdle");

        if (startingOnePLayerGame || quittingGame == true) //if one player is playing or is quitting the game
        {
            //_mainMenuController = MainMenu.MainMenuController.MainMenuFadeOut;   //then fade the heck out
            mainMenuFailsafe = 2;
        }

    }

    private void MainMenuFadeOut()
    {
        Debug.Log("MainMenuFadeOut");

        mainMenuSound.volume -= mainMenuFadeSpeed * Time.deltaTime / 4; //the volume will decrease as the menu fades in
        mainMenuFadeValue -= mainMenuFadeSpeed * Time.deltaTime; //fade value decreases by the fade speed
        if (mainMenuFadeValue < 0)
            mainMenuFadeValue = 0;   //This is to ensure that the value never drops below 0, if it does it will be made equal to 0.

        if (mainMenuFadeValue == 0 && startingOnePLayerGame == true)
            SceneManager.LoadScene("ChooseCharacter");
    }

    private void MainMenuButtonPress()
    {
        Debug.Log("MainMenuButtonPress");

        GUI.FocusControl(mainMenuButtons[selectedButton]);

        switch (selectedButton)
        {
            case 0:
                mainMenuSound.PlayOneShot(mainMenuStartSFX);    //plays start button audio
                startingOnePLayerGame = true;
                GameObject.FindGameObjectWithTag("OnePlayerManager").GetComponent<LoadingChar>().enabled = true;
                break;
            case 1:
                mainMenuSound.PlayOneShot(mainMenuStartSFX);    
                startingTwoPLayerGame = true;
                GameObject.FindGameObjectWithTag("TwoPlayerManager").GetComponent<TwoPlayerLoading>().enabled = true;
                break;
            case 2:
                mainMenuSound.PlayOneShot(mainMenuQuitSFX);    //plays start button audio
                quittingGame = true;
                break;
            /*case 3:
                mainMenuSound.PlayOneShot(mainMenuStartSFX);    
                startingThreePLayerGame = true;
                break;
            case 4:
                mainMenuSound.PlayOneShot(mainMenuStartSFX);    
                startingFourPLayerGame = true;
                break;
            case 5:
                mainMenuSound.PlayOneShot(mainMenuQuitSFX);    //plays start button audio
                quittingGame = true;
                break;*/
        }
    }

    private void OnGUI()
    {
        if (Time.deltaTime >= timeDelay && (Input.GetButton("Fire1")))
        {
            Debug.Log("AAAA");
            StartCoroutine("MainMenuButtonPress");
            timeDelay = Time.deltaTime + timeBetweenButtonPress;
        }

        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), mainMenuBG);
        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), mMTitle);
        GUI.color = new Color(1, 1, 1, mainMenuFadeValue);
        GUI.BeginGroup(new Rect(Screen.width / 2 - mainMenuButtonWidth / 2, Screen.height / 1.5f, mainMenuButtonWidth, mainMenuButtonHeight * 3 + mainMenuGUIOffset * 2));

        GUI.SetNextControlName("onePlayer");
        if(GUI.Button(new Rect(0, 0, mainMenuButtonWidth, mainMenuButtonHeight), "VS COMPUTER"))
        {
            selectedButton = 0;
            MainMenuButtonPress();
        }
        GUI.SetNextControlName("twoPlayer");
        if (GUI.Button(new Rect(0, mainMenuButtonHeight + mainMenuGUIOffset , mainMenuButtonWidth, mainMenuButtonHeight), "TWO PLAYER"))
        {
            selectedButton = 1;
            MainMenuButtonPress();
        }
        /*GUI.SetNextControlName("threePlayer");
        if (GUI.Button(new Rect(0, 0, mainMenuButtonWidth, mainMenuButtonHeight), "THREE PLAYER"))
        {
            selectedButton = 0;
            MainMenuButtonPress();
        }
        GUI.SetNextControlName("fourPlayer");
        if (GUI.Button(new Rect(0, 0, mainMenuButtonWidth, mainMenuButtonHeight), "MAYHEM"))
        {
            selectedButton = 0;
            MainMenuButtonPress();
        }*/
        GUI.SetNextControlName("quit");
        if (GUI.Button(new Rect(0, mainMenuButtonHeight * 2 + mainMenuGUIOffset * 2, mainMenuButtonWidth, mainMenuButtonHeight), "END GAME"))
        {
            selectedButton = 2;
            MainMenuButtonPress();
        }

        GUI.EndGroup();

        if (ps4Controller == true || xBOXController == true)            //The || means OR 
            GUI.FocusControl(mainMenuButtons[selectedButton]);

     
    }
}
