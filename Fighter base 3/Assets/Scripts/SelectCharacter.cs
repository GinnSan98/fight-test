using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//Made by Connor
[RequireComponent(typeof(AudioSource))]
public class SelectCharacter : CCManager
{
    public Texture2D scText;
    public Texture2D scTextFG;
    public Texture2D scTextBG;
    public Texture2D scLeft;
    public Texture2D scRight;
    public Texture2D xbLeft;
    public Texture2D xbRight;
    public Texture2D ps4Left;
    public Texture2D ps4Right;
    private float fgTextWidth;
    private float fgTextHeight;
    private float arrowSize;
    public float selectCharacterInputTimer;
    public float selectCharacterInputDelay = 1f;
    public static bool demoPlayer;
    public AudioClip characterSelectSwitch;
    public AudioClip characterSelect;

    //private GameObject characterDemoSprite;
    public int selectCharacterState;
    public List<SpriteRenderer> sprites;

    public bool Yeet = true;
    

    private enum selectedCharacterModel                //tells the game who to load
    {
        Bee = 0,
        Bat = 1,
        Badger = 2,
        Dragon = 3,
        unknownOne = 4,
        unknownTwo = 5,
        unknownThree = 6,
        unknownFour = 7,
        unknownFive = 8,
        unknownSix = 9
    }

    void characterHighlight(int heckin)
    {
        for (int i = 0; i < sprites.Count; i++)
        {
            if(i == heckin)
            {
                sprites[i].enabled = true;
            }
            else
            {
                sprites[i].enabled = false;

            }

        }
    }

	void Start ()
    {
        characterHighlight(0);
        ChooseCharacterManager();
        demoPlayer = true;

        fgTextWidth = Screen.width / 1.5f;
        fgTextHeight = Screen.height / 10f;
        arrowSize = Screen.height / 10f;
    }
	

	void Update ()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            demoPlayer = false;
            GameObject.FindGameObjectWithTag("ArtManager").GetComponent<artManager>().SendMessage("BGLoad");
        }

        if (selectCharacterInputTimer >= 0)
        {
            selectCharacterInputTimer -= 1.5f * Time.deltaTime;
            Yeet = false;
        }
        else if(selectCharacterInputTimer <= 0)
        {
            Yeet = true;
        }

            if (Input.GetAxis("Horizontal") < -0.5f && Yeet == true)
            {
                Debug.Log("Detected Input");
                if (selectCharacterState == 0)
                {
                    GetComponent<AudioSource>().PlayOneShot(characterSelectSwitch);
                    selectCharacterState = sprites.Count - 1;
                }
                else
                {
                GetComponent<AudioSource>().PlayOneShot(characterSelectSwitch);
                selectCharacterState--;
                }
                
                ChooseCharacterManager();
                characterHighlight(selectCharacterState);
                selectCharacterInputTimer = selectCharacterInputDelay;
            }

            else if (Input.GetAxis("Horizontal") > 0.5f && Yeet == true)
            {
                Debug.Log("Detected Input");
                if (selectCharacterState == 9)
                {
                GetComponent<AudioSource>().PlayOneShot(characterSelectSwitch);
                selectCharacterState = 0;
                }
                else
                {
                GetComponent<AudioSource>().PlayOneShot(characterSelectSwitch);
                selectCharacterState++;
                }

                ChooseCharacterManager();
                characterHighlight(selectCharacterState);
                selectCharacterInputTimer = selectCharacterInputDelay;
            }
        

    }

    public void ChooseCharacterManager()
    {
        switch (selectCharacterState)
        {
            default:
            case 0:
                Bee();
                break;
            case 1:
                Bat();
                break;
            case 2:
                Badger();
                break;
            case 3:
                Dragon();
                break;
            case 4:
                unknownOne();
                break;
            case 5:
                unknownTwo();
                break;
            case 6:
                unknownThree();
                break;
            case 7:
                unknownFour();
                break;
            case 8:
                unknownFive();
                break;
            case 9:
                unknownSix();
                break;


        }
    }

    private void Bee()
    {
        Debug.Log("Bee Selected");
        bee = true;
        bat = false;
        badger = false;
        dragon = false;
        unknown1 = false;
        unknown2 = false;
        unknown3 = false;
        unknown4 = false;
        unknown5 = false;
        unknown6 = false;
    }
    private void Bat()
    {
        Debug.Log("Bat Selected");
        bee = false;
        bat = true;
        badger = false;
        dragon = false;
        unknown1 = false;
        unknown2 = false;
        unknown3 = false;
        unknown4 = false;
        unknown5 = false;
        unknown6 = false;
    }
    private void Badger()
    {
        Debug.Log("Badger Selected");
        bee = false;
        bat = false;
        badger = true;
        dragon = false;
        unknown1 = false;
        unknown2 = false;
        unknown3 = false;
        unknown4 = false;
        unknown5 = false;
        unknown6 = false;
    }
    private void Dragon()
    {
        Debug.Log("Dragon Selected");
        bee = false;
        bat = false;
        badger = false;
        dragon = true;
        unknown1 = false;
        unknown2 = false;
        unknown3 = false;
        unknown4 = false;
        unknown5 = false;
        unknown6 = false;
    }
    private void unknownOne()
    {
        Debug.Log("Unknown fighter Selected");
        bee = false;
        bat = false;
        badger = false;
        dragon = false;
        unknown1 = true;
        unknown2 = false;
        unknown3 = false;
        unknown4 = false;
        unknown5 = false;
        unknown6 = false;
    }
    private void unknownTwo()
    {
        Debug.Log("Unknown fighter Selected");
        bee = false;
        bat = false;
        badger = false;
        dragon = false;
        unknown1 = false;
        unknown2 = true;
        unknown3 = false;
        unknown4 = false;
        unknown5 = false;
        unknown6 = false;
    }
    private void unknownThree()
    {
        Debug.Log("Unknown fighter Selected");
        bee = false;
        bat = false;
        badger = false;
        dragon = false;
        unknown1 = false;
        unknown2 = false;
        unknown3 = true;
        unknown4 = false;
        unknown5 = false;
        unknown6 = false;
    }
    private void unknownFour()
    {
        Debug.Log("Unknown fighter Selected");
        bee = false;
        bat = false;
        badger = false;
        dragon = false;
        unknown1 = false;
        unknown2 = false;
        unknown3 = false;
        unknown4 = true;
        unknown5 = false;
        unknown6 = false;
    }
    private void unknownFive()
    {
        Debug.Log("Unknown fighter Selected");
        bee = false;
        bat = false;
        badger = false;
        dragon = false;
        unknown1 = false;
        unknown2 = false;
        unknown3 = false;
        unknown4 = false;
        unknown5 = true;
        unknown6 = false;
    }
    private void unknownSix()
    {
        Debug.Log("Unknown fighter Selected");
        bee = false;
        bat = false;
        badger = false;
        dragon = false;
        unknown1 = false;
        unknown2 = false;
        unknown3 = false;
        unknown4 = false;
        unknown5 = false;
        unknown6 = true;
    }

    

    void OnGUI()
    {
        GUI.DrawTexture(new Rect(0,0, Screen.width, Screen.height / 10), scTextBG);
        GUI.DrawTexture(new Rect(Screen.width / 2 - (fgTextWidth / 2), 0, fgTextWidth, fgTextHeight), scTextBG);
        GUI.DrawTexture(new Rect(Screen.width / 2 - (fgTextWidth / 2), 0, fgTextWidth, fgTextHeight), scText);

        if(GameObject.FindGameObjectWithTag("ControllerManager")
            .GetComponent<ControllerManager>()
            .xBOXController == true)
        {
            GUI.DrawTexture(new Rect(Screen.width / 2 - (fgTextWidth / 2) - arrowSize, 0, arrowSize, arrowSize), xbLeft);
            GUI.DrawTexture(new Rect(Screen.width / 2 + (fgTextWidth / 2), 0, arrowSize, arrowSize), xbRight);
        }
        if (GameObject.FindGameObjectWithTag("ControllerManager")
            .GetComponent<ControllerManager>()
            .pS4Controller == true)
        {
            GUI.DrawTexture(new Rect(Screen.width / 2 - (fgTextWidth / 2) - arrowSize, 0, arrowSize, arrowSize), ps4Left);
            GUI.DrawTexture(new Rect(Screen.width / 2 + (fgTextWidth / 2), 0, arrowSize, arrowSize), ps4Right);
        }
        else
        {
            GUI.DrawTexture(new Rect(Screen.width / 2 - (fgTextWidth / 2) - arrowSize, 0, arrowSize, arrowSize), scLeft);
            GUI.DrawTexture(new Rect(Screen.width / 2 + (fgTextWidth / 2), 0, arrowSize, arrowSize), scRight);
        }
    }
}
