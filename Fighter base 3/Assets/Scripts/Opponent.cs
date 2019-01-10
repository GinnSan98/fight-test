using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Opponent : MonoBehaviour
{
   
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
    private int rotateY = -180;
    private bool noSpawn = false;
    public GameObject currentOpponent;            //Stores current opponent but for now will only store data for test dummy;
    public string selectedOpponent = "";               //Will eventually be able to randomise opponent but for the uni project will not
    public int opponentNo;


    public string[] opponentOrder = new string[]
    {
        "Dummy"

    };

	void Start ()
    {
      
        DontDestroyOnLoad(this);
        opponentNo = 0;

        selectedOpponent = opponentOrder[0];                   //This will one day randomise a bunch of fighters, but not yet

        for (int op = 0; op < opponentOrder.Length; op++)
        {
            string tempOp = opponentOrder[op];
            int randomOp = Random.Range(op, opponentOrder.Length);
            opponentOrder[op] = opponentOrder[randomOp];
            opponentOrder[randomOp] = tempOp;
        }

	}
	
	void Update ()
    {
        

    }

    void LoadCurrentOp()
    {
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

        if (selectedOpponent == "BeeOp" && returnBee != true)
        {
            currentOpponent = Instantiate(Resources.Load("Bee")) as GameObject;
         
        }
        else if(selectedOpponent == "BeeOp" && returnBee == true)
        {
            currentOpponent = Instantiate(Resources.Load("BeeAlt")) as GameObject;
            StopSpawningTwoOpponentsGodDammit();
            return;
        }
        if (selectedOpponent == "BatOp" && returnBat != true)
        {
            currentOpponent = Instantiate(Resources.Load("Bat")) as GameObject;
     
        }
        else if (selectedOpponent == "BatOp" && returnBat == true)
        {
            currentOpponent = Instantiate(Resources.Load("BatAlt")) as GameObject;
            StopSpawningTwoOpponentsGodDammit();
            return;
        }

        if (selectedOpponent == "BadgerOp" && returnBat != true)
        {
            currentOpponent = Instantiate(Resources.Load("Badger")) as GameObject;
         
        }
        else if (selectedOpponent == "BadgerOp" && returnBat == true)
        {
            currentOpponent = Instantiate(Resources.Load("BadgerAlt")) as GameObject;
            StopSpawningTwoOpponentsGodDammit();
            return;
        }

        if (selectedOpponent == "DragonOp" && returnBat != true)
        {
            currentOpponent = Instantiate(Resources.Load("Dragon")) as GameObject;
           
        }
        else if (selectedOpponent == "DragonOp" && returnBat == true)
        {
            currentOpponent = Instantiate(Resources.Load("DragonAlt")) as GameObject;
            StopSpawningTwoOpponentsGodDammit();
            return;
        }
        if (selectedOpponent == "Dummy" && noSpawn == false)
        {
            noSpawn = true;
            currentOpponent = Instantiate(Resources.Load("Dummy")) as GameObject;
            StopSpawningTwoOpponentsGodDammit();
            return;
        }
        Debug.Log("Loaded");
        
    }

    private void StopSpawningTwoOpponentsGodDammit()
    {
        currentOpponent.transform.position = new Vector3(-13.53f, -2.49f, -0.6f);
        currentOpponent.transform.eulerAngles = new Vector3(0, rotateY, 0);

        Camera.Opponent = currentOpponent;

        currentOpponent.GetComponent<P1Movement>().enabled = false;
        currentOpponent.GetComponent<P1Health>().enabled = false;

        currentOpponent.GetComponent<OpponentAI>().enabled = true;
        currentOpponent.GetComponent<OppHealth>().enabled = true;
    }
}
