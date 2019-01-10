using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpponentHardHit : MonoBehaviour
{
    public static Vector3 opContactPoint;
    public Collider hardHitCol;
    private bool returnIfPlayerPunch;

	void Start ()
    {
        opContactPoint = Vector3.zero;
        hardHitCol = GetComponent<Collider>();
        hardHitCol.enabled = false;
	}

	void Update ()
    {
        returnIfPlayerPunch = P1Movement.isPunchingHard;

        if(returnIfPlayerPunch == true)
        {
            hardHitCol.enabled = true;
        }
	}

    void OnTriggerEnter(Collider col)
    {
        if(col.tag == ("HardHit"))
        {
            HardHit();
        }

        col.ClosestPointOnBounds(transform.position);

        opContactPoint = col.transform.position;
    }

    void HardHit()
    {
        OpponentAI.opAIState = OpponentAI.OpponentAIState.OpHardHit;
    }
}
