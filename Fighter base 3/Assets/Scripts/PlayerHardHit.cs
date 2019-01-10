using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHardHit : MonoBehaviour
{

    public static Vector3 ContactPoint;
    public Collider hardHitCol;
    private bool returnIfPlayerPunch;

    void Start()
    {
        ContactPoint = Vector3.zero;
        hardHitCol.enabled = false;
    }

    void Update()
    {
        returnIfPlayerPunch = OpponentAI.IsHeavyAttacking;

        if (returnIfPlayerPunch == true)
        {
            hardHitCol.enabled = true;
        }
    }


    void HardHit()
    {
 
    }
}
