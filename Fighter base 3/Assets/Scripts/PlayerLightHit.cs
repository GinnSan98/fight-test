using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLightHit : MonoBehaviour
{

    public static Vector3 ContactPoint;
    public Collider lightHitCol;
    private bool returnIfPlayerPunch;

    void Start()
    {
        ContactPoint = Vector3.zero;
        lightHitCol = GetComponent<Collider>();
        lightHitCol.enabled = false;
    }


    void Update()
    {
        returnIfPlayerPunch = OpponentAI.IsLightAttacking;

        if (returnIfPlayerPunch == true)
        {
            lightHitCol.enabled = true;
        }
    }


    void LightHit()
    {

        
    }
}
