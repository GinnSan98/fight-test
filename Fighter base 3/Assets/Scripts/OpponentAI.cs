using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(AudioSource))]
public class OpponentAI : MonoBehaviour
{
    public bool callonce;
    public Renderer lightottacko;
    public bool ChangeofTactics;
    public bool attackDist;
    public bool retreatDist;
    public bool approachDist;
    private GameObject player;
    private GameObject opponentObject;
    private float dist;
    public float viewDist;
    public Collider LightAttack;
    public Collider HeavyAttack;
    private Transform opTransform;
    private CharacterController opController;
    public float opWalkSpeed = 1f;
    public float opBackOffSpeed = 1.5f;
    private Animation opAnim;
    public AnimationClip opIdleAnim;
    public AnimationClip opWalkAnim;
    public AnimationClip opHitAnim;
    public AnimationClip opHardHitAnim;
    public AnimationClip opDeathAnim;
    private AudioSource opAIAudioSauce;                        //It's half one in the morning let me have my jokes
    public AudioClip opLightHitAudio;
    public AudioClip opHardHitAudio;
    private bool returnIntroFinsihed;
    public GameObject hitEffect;
    private Vector3 opMoveDir = Vector3.zero;
    public static bool IsLightAttacking;
    public static bool IsHeavyAttacking;
    public float y;
    private float z;
    private float opGravity = 3f;
    private float opGravityMod = 5f;
    private float opVerticalSpeed = 0.0f;

    private CollisionFlags collisionFlagsOp;

    public static OpponentAIState opAIState;

    public enum OpponentAIState
    {
        Initialise,
        OpIdle,
        OpMove,
        OpHit,
        OpHardHit,
        OpLightAttack,
        OpHardAttack,
        WaitForHitAnimation,
        OpDeath
    }


	void Start ()
    {
        callonce = false;
        lightottacko.enabled = false;
        y = transform.position.y;
        z = transform.position.z;
        ChangeofTactics = false;
        attackDist = false;
        retreatDist = false;
        approachDist = false;
        IsLightAttacking = false;
        IsHeavyAttacking = false;
        LightAttack.enabled = false;
        HeavyAttack.enabled = false;
        opController = GetComponent<CharacterController>();
        opAnim = GetComponent<Animation>();
        opAIAudioSauce = GetComponent<AudioSource>();
        opTransform = transform;
        opMoveDir = Vector3.zero;

        StartCoroutine("OpponentStateMachine");

	}
	
	void Update ()
    {
      
        player = GameObject.FindGameObjectWithTag("Player");
        opponentObject = GameObject.FindGameObjectWithTag("Enemy");
        float dist = Vector3.Distance(opponentObject.transform.position, player.transform.position);
        viewDist = dist;
        OpponentGravity();

        returnIntroFinsihed = FightIntro.introFinished;

        if (returnIntroFinsihed != true)
        {
            return;
        }
        if (returnIntroFinsihed == true)
        {
            opController.enabled = true;
        }

        if (dist >= 7)
        {
            approachDist = true;
        }
        else if(dist < 4)
        {
            approachDist = false;
        }
        if (dist >= 3.5f && approachDist == false)
        {
            retreatDist = true;
        }
        else if(dist <= 3.5f && approachDist == false)
        {
            retreatDist = false;
        }
        else if (dist >= 4f)
        {
            retreatDist = false;
        }
        if (dist < 3.5f && approachDist == false)
        {
            Debug.Log("Attacking");

            OpLightAttack();
        }
        if (ChangeofTactics == true && dist >= 3.5f && callonce == false)
        {
            approachDist = false;
            callonce = true;
            OpponentBackOff();
        }
        if (ChangeofTactics == true && dist >= 15f)
        {
            approachDist = true;
        }
        if (LightAttack.enabled == true)
        {
            StartCoroutine("LightAttackOff");
        }
        if (HeavyAttack.enabled == true)
        {
            StartCoroutine("HeavyAttackOff");
        }
        if (approachDist == true)
        {
            OpMove();
        }
        /*if(retreatDist == true)
        {
            OpponentBackOff();
        }*/
        if (y != -2.49f)
        {
            y = -2.49f;
        }
        if (z != -0.62f)
        {
            z = -0.62f;
        }

    }
    private IEnumerator LightAttackOn()
    {
        yield return new WaitForSeconds(5f);
        lightottacko.enabled = true;
        LightAttack.enabled = true;
    }
    private IEnumerator LightAttackOff()
    {
        yield return new WaitForSeconds(0.1f);
        callonce = false;
        lightottacko.enabled = false;
        LightAttack.enabled = false;
    }
    private IEnumerator HeavyAttackOn()
    {
        yield return new WaitForSeconds(8f);
        HeavyAttack.enabled = true;
    }
    private IEnumerator HeavyAttackOff()
    {
        yield return new WaitForSeconds(1f);

        HeavyAttack.enabled = false;
    }
    private IEnumerator OpponentStateMachine()
    {
        while (true)
        {
            switch (opAIState)
            {
                case OpponentAI.OpponentAIState.Initialise:
                    Initialise();
                    break;
                case OpponentAI.OpponentAIState.OpIdle:
                    OpIdle();
                    break;
                case OpponentAI.OpponentAIState.OpMove:
                    OpMove();
                    break;
                case OpponentAI.OpponentAIState.OpHit:
                    OpHit();
                    break;
                case OpponentAI.OpponentAIState.OpHardHit:
                    OpHardHit();
                    break;
                case OpponentAI.OpponentAIState.OpLightAttack:
                    OpLightAttack();
                    break;
                case OpponentAI.OpponentAIState.OpHardAttack:
                    OpHardAttack();
                    break;
                case OpponentAI.OpponentAIState.WaitForHitAnimation:
                    WaitForHitAnimation();
                    break;
                case OpponentAI.OpponentAIState.OpDeath:
                    OpDeath();
                    break;


            }

            yield return null;
        }
    }

    private void Initialise()
    {       
        opAIState = OpponentAI.OpponentAIState.OpIdle;
    }

    private void OpIdle()
    {
        Debug.Log("Opponent is Idle");

        OpponentIdleAnimation();

        if (OpponentGrounded())
        {
            return;
        }
        opMoveDir = new Vector3(0, opVerticalSpeed, 0);

        opMoveDir = opTransform.TransformDirection(opMoveDir);

        collisionFlagsOp = opController.Move(opMoveDir * Time.deltaTime);

        returnIntroFinsihed = FightIntro.introFinished;

        if (returnIntroFinsihed != true)
        {
            return;
        }
    }

    private void OpMove()
    {

        opWalkAnimation();
        opMoveDir = new Vector3(+ opWalkSpeed, 0, 0);
        opMoveDir = opTransform.TransformDirection(opMoveDir).normalized;
        opMoveDir *= opWalkSpeed;

        collisionFlagsOp = opController.Move(opMoveDir * Time.deltaTime);
    }

    private void OpponentBackOff()
    {

        opWalkAnimation();

        opMoveDir = new Vector3(-opBackOffSpeed, 0, 0);
        opMoveDir = opTransform.TransformDirection(opMoveDir).normalized;

        opMoveDir *= opBackOffSpeed;

        collisionFlagsOp = opController.Move(opMoveDir * Time.deltaTime);

    }

    private void ChangeTactics()
    {
        ChangeofTactics = true;
    }
    private void ChangeTactics2()
    {
        ChangeofTactics = false;
    }
    public float tempVar2 = 1f;

    private void OpHit()
    {
        OpponentHitAnimation();

        opAIAudioSauce.PlayOneShot(opLightHitAudio);
        Vector3 contactPoint = OpponentLightHit.opContactPoint;
        GameObject hit = Instantiate(hitEffect, new Vector3(contactPoint.x, contactPoint.y + tempVar2, contactPoint.z + -0.3f), Quaternion.identity) as GameObject;

        opAIState = OpponentAI.OpponentAIState.WaitForHitAnimation;
    }

    public float tempVar = 0;

    private void OpHardHit()
    {
        OpponentHardHitAnimation();

        opAIAudioSauce.PlayOneShot(opHardHitAudio);

        Vector3 contactPoint = OpponentHardHit.opContactPoint;

        GameObject hit = Instantiate(hitEffect, new Vector3(contactPoint.x, contactPoint.y + tempVar, contactPoint.z), Quaternion.identity) as GameObject;

        opAIState = OpponentAI.OpponentAIState.WaitForHitAnimation;
    }

    private void OpLightAttack()
    {
        IsLightAttacking = true;
        StartCoroutine("LightAttackOn");
    }

    private void OpHardAttack()
    {
        IsHeavyAttacking = true;
        StartCoroutine("LightAttackOn");
    }

    private void WaitForHitAnimation()
    {
   

        if (opAnim.IsPlaying(opHitAnim.name))
        {
            return;
        }
        if (opAnim.IsPlaying(opHardHitAnim.name))
        {
            return;
        }
        opAIState = OpponentAI.OpponentAIState.OpIdle;
    }

    private void OpDeath()
    {
        
        opMoveDir = new Vector3(0, opVerticalSpeed, 0);

        opMoveDir = opTransform.TransformDirection(opMoveDir);

        collisionFlagsOp = opController.Move(opMoveDir * Time.deltaTime);

        if (opAnim.IsPlaying(opDeathAnim.name))
        {
            return;
        }
        StopCoroutine("OpponentStateMachine");
    }

    private void SetOpDeath()
    {
        OpDeath();
        OpponentDeadAnimation();

        opAIState = OpponentAI.OpponentAIState.OpDeath;
    }

    private void OpponentIdleAnimation()
    {
        opAnim.CrossFade(opIdleAnim.name);
    }
    private void opWalkAnimation()
    {

        //opAnim.CrossFade(opWalkAnim.name);
    }
    private void OpponentHitAnimation()
    {
        opAnim.CrossFade(opHitAnim.name);
    }
    private void OpponentHardHitAnimation()
    {
        opAnim.CrossFade(opHardHitAnim.name);
    }

    private void OpponentDeadAnimation()
    {
        opAnim.CrossFade(opDeathAnim.name);
    }

    void OpponentGravity()
    {
        if (OpponentGrounded())
        {
            opVerticalSpeed = 0.0f;
        }
        else
        {
            opVerticalSpeed -= opGravity * opGravityMod * Time.deltaTime;
        }
    }

    public bool OpponentGrounded()
    {
  
        return (collisionFlagsOp & CollisionFlags.CollidedBelow) != 0;
    }
}
