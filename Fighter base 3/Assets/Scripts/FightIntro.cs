using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class FightIntro : MonoBehaviour
{
    private int round; 
    public Texture2D rOne;                      //These are for the rounds.
    public Texture2D rTwo;
    public Texture2D rThree;
    public Texture2D fight;
    private AudioSource IntroAudioSource;
    public AudioClip announceROne;
    public AudioClip announceRTwo;
    public AudioClip announceRThree;
    public AudioClip announceFight;
    private float introFadeValue;
    private float introFadeSpeed = 0.25f;
    private bool roundDisplay;
    private bool fightUIDisplay;
    public static bool introFinished;

    private FightIntroductionState FIState;

    private enum FightIntroductionState
    {
        Intro = 0,
        IntroFadeIn = 1,
        IntroFightAnnouncement = 2
    }

    void Start ()
    {
        IntroAudioSource = GetComponent<AudioSource>();
        introFinished = false;
        introFadeValue = 0;
        round = 1;
        roundDisplay = false;
        fightUIDisplay = false;

        StartCoroutine("IntroManager");
	}

	void Update ()
    {
		
	}

    private IEnumerator IntroManager()
    {
        while (true)
        {
            switch (FIState)
            {
                case FightIntroductionState.Intro:
                    Intro();
                    break;
                case FightIntroductionState.IntroFadeIn:
                    IntroFadeIn();
                    break;
                case FightIntroductionState.IntroFightAnnouncement:
                    IntroFightAnnouncement();
                    break;
            }
            yield return null;
        }
    }

    private void Intro()
    {
        roundDisplay = true;

        if (round == 1)
        {
            IntroAudioSource.PlayOneShot(announceROne);
        }
        if (round == 2)
        {
            IntroAudioSource.PlayOneShot(announceRTwo);
        }
        if (round == 3)
        {
            IntroAudioSource.PlayOneShot(announceRThree);
        }
        FIState = FightIntro.FightIntroductionState.IntroFadeIn;

    }
    private void IntroFadeIn()
    {
        introFadeValue += introFadeSpeed * Time.deltaTime;

        if(introFadeValue > 1)
        {
            introFadeValue = 1;
        }
        if (introFadeValue == 1)
        {
            fightUIDisplay = true;
            IntroAudioSource.PlayOneShot(announceFight);
            FIState = FightIntro.FightIntroductionState.IntroFightAnnouncement;
        }
    }
    private void IntroFightAnnouncement()
    {
        introFadeValue -= introFadeSpeed * 2 * Time.deltaTime;

        if(introFadeValue < 0)
        {
            introFadeValue = 0;
        }
        if(introFadeValue == 0)
        {
            roundDisplay = false;
            fightUIDisplay = false;
            introFinished = true;

            StopCoroutine("IntroManager");
        }
    }
    private void RoundIncrease()
    {
        round++;
    }
    private void OnGUI()
    {
        GUI.color = new Color(1, 1, 1, introFadeValue);
        
        if(roundDisplay == true)
        {
            if(round == 1)
            {
                GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), rOne);
            }
            if (round == 2)
            {
                GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), rTwo);
            }
            if (round == 3)
            {
                GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), rThree);
            }
        }
        if (fightUIDisplay == true)
        {
            GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), fight);
        }
    }
}
