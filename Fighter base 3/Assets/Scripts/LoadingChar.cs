using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingChar : MonoBehaviour
{

    private GameObject pOne;
    private bool returnBee;
    private bool returnBadger;
    private bool returnBat;
    private bool returnDragon;
    private bool returnUnknown1;
    private bool returnUnknown2;
    private bool returnUnknown3;
    private bool returnUnknown4;
    private bool returnUnknown5;
    private bool returnUnknown6;

    void Start ()
    {


    }
	

	void Update ()
    {
		
	}

    void LoadPOne()
    {
        Debug.Log("Player 1 loaded");

        if(pOne != null)
        {
            return;
        }

        returnBee = CCManager.bee;
        returnBat = CCManager.bat;
        returnBadger = CCManager.badger;
        returnDragon = CCManager.dragon;
        returnUnknown1 = CCManager.unknown1;
        returnUnknown2 = CCManager.unknown2;
        returnUnknown3 = CCManager.unknown3;
        returnUnknown4 = CCManager.unknown4;
        returnUnknown5 = CCManager.unknown5;
        returnUnknown6 = CCManager.unknown6;

        if (returnBee == true)
        {
            pOne = Instantiate(Resources.Load("Bee")) as GameObject;
        }
        if (returnBat == true)
        {
            pOne = Instantiate(Resources.Load("Bat")) as GameObject;
        }
        if (returnBadger == true)
        {
            pOne = Instantiate(Resources.Load("Badger")) as GameObject;
        }
        if (returnDragon == true)
        {
            pOne = Instantiate(Resources.Load("Dragon")) as GameObject;
        }
        if (returnUnknown1 == true)
        {
           
        }
        if (returnUnknown2 == true)
        {
       
        }
        if (returnUnknown3 == true)
        {
  
        }
        if (returnUnknown4 == true)
        {

        }
        if (returnUnknown5 == true)
        {

        }
        if (returnUnknown6 == true)
        {


        }

        pOne.transform.position = new Vector3(-24.53f, -2.49f, -0.62f);
        Camera.pOne = pOne;

        pOne.GetComponent<P1Movement>().enabled = true;
        pOne.GetComponent<P1Health>().enabled = true;

        pOne.GetComponent<OpponentAI>().enabled = false;
        pOne.GetComponent<OppHealth>().enabled = false;
    }
}
