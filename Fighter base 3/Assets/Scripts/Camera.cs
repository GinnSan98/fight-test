using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    private Vector3 cameraStartPos = new Vector3(0, -3, -7.5f);
    private GameObject fightCamera;
    private float camXAxis;
    private float camZAxis;
    private int camZAxisMod = -8;

    public static GameObject pOne;
    public static GameObject Opponent;

    private Vector3 playerPos;
    private Vector3 oppPos;

    void Start ()
    {
        fightCamera = GameObject.FindGameObjectWithTag("MainCamera");
        fightCamera.transform.position = cameraStartPos;

	}
	

	void Update ()
    {
        UpdatePlayerPos();
        UpdateOppPos();
        UpdateCamPos();
	}

    private void UpdatePlayerPos()
    {


        playerPos = new Vector3(pOne.transform.position.x, pOne.transform.position.y, pOne.transform.position.z);
    }
    private void UpdateOppPos()
    {
       

        oppPos = new Vector3(Opponent.transform.position.x, Opponent.transform.position.y, Opponent.transform.position.z);
    }

    private void UpdateCamPos()
    {

        camXAxis = (pOne.transform.position.x + Opponent.transform.position.x) / 2;

        if(pOne.transform.position.x < Opponent.transform.position.x)
        {
            camZAxis = pOne.transform.position.x - Opponent.transform.position.x;
        }
        if (pOne.transform.position.x > Opponent.transform.position.x)
        {
            camZAxis = Opponent.transform.position.x - pOne.transform.position.x;
        }

        if (camZAxis > -2)
        {
            camZAxis = -2;
        }

        fightCamera.transform.position = new Vector3(camXAxis, 1, camZAxis + camZAxisMod);
    }
}
