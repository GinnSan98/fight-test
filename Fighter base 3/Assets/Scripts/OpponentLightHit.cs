using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpponentLightHit : MonoBehaviour
{
    public static Vector3 opContactPoint;
    public Collider lightHitCol;
    private bool returnIfPlayerPunch;

    void Start()
    {
        opContactPoint = Vector3.zero;
        lightHitCol.enabled = false;
    }


    void Update()
    {
        returnIfPlayerPunch = P1Movement.isPunchingLight;

        if (returnIfPlayerPunch == true)
        {
            lightHitCol.enabled = true;
        }
    }


    void LightHit()
    {
        
        OpponentAI.opAIState = OpponentAI.OpponentAIState.OpHit;
    }
}
