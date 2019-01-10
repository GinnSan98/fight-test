using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P1Movement : MonoBehaviour
{
    public Renderer lightuattacku;
    public Collider LightAttack;
    public Collider HeavyAttack;
    private Transform pOneTransform;
    public float pWalkSpeed = 1f;                         //Don't worry i'll make sure each individual player gets different speeds down the line
    public float pBackOffSpeed = 1.5f;
    private Vector3 pOneMoveDir = Vector3.zero;
    public CharacterController pOneController;
    public AnimationClip pOneIdle;                        
    public AnimationClip pOneWalk;
    public AnimationClip pOneDemoAnim;
    public AnimationClip pOneBackOff;
    public AnimationClip pOneJump;
    public AnimationClip pOneDeathAnim;
    private Animation pOneAnim;
    public float pJumpHeight = 1f;
    public float pJumpSpeed = 3f;
    //public bool canDJump;                              //for characters who can Double Jump
    public bool isLightAttacking;
    public static bool isPunchingHard;
    public static bool isPunchingLight;
    public float deadZonePos = 0.01f;
    public float deadZoneNega = -0.01f;
    public float pGravity = 2f;
    public float pSpeedY;
    public float pGravMod = 1.5f;
    public float pJumpHori = 2f;
    public AnimationClip[] pAttacks;
    private CollisionFlags collisionFlags;
    private bool returnDemo;
    private float y;
    private float z;
    private bool returnIntroFinsihed;

    private pOneStates pUnoStates;

    private enum pOneStates
    {
        pOneIdle,
        pOneWalk,                                    
        pOneBackOff,
        pJump,
        fJump,
        bJump,
        Fall,
        fFall,
        bFall,
        LP,
        MP,
        HP,
        LK,
        MK,
        HK,
        WaitForAnimations,
        Demo,
        pOneDead

    }

	void Start ()
    {
        lightuattacku.enabled = false;
        isLightAttacking = false;
        y = transform.position.y;
        z = transform.position.z;
        LightAttack.enabled = false;
        HeavyAttack.enabled = false;
        pOneTransform = transform;
        pOneMoveDir = Vector3.zero;
        pOneController = GetComponent<CharacterController>();
        pSpeedY = 0;
        pOneAnim = GetComponent<Animation>();
        StartCoroutine("PlayerOneStateMachine");
        for (int i = 0; i < pAttacks.Length; i++)
        {
            pOneAnim[pAttacks[i].name].wrapMode = WrapMode.Once;
        }

        returnDemo = false;
        returnDemo = SelectCharacter.demoPlayer;
        isPunchingHard = false;
        isPunchingLight = false;
        if (returnDemo == true)
        {
            pUnoStates = P1Movement.pOneStates.Demo;
        }

    }
	

	void Update ()
    {
        if(y != -2.49f)
        {
            y = -2.49f;
        }
        if(z != -0.62f)
        {
            z = -0.62f;
        }
        Gravity();        
      
        for (int i = 0; i < pAttacks.Length; i ++)
        {
            if (pOneAnim.IsPlaying(pAttacks[i].name))
            {
                return;
            }
        }
        returnIntroFinsihed = FightIntro.introFinished;

        if(returnIntroFinsihed != true)
        {
            return;
        }

        if (Grounded())
        {

            AttackInputManager();
            InputManager();
        }

        if (LightAttack.enabled == true)
        {
            StartCoroutine("LightAttackOff");
        }
        if (HeavyAttack.enabled == true)
        {
            StartCoroutine("HeavyAttackOff");
        }

        if(LightAttack.enabled == true)
        {
            isLightAttacking = true;
        }

    }

    private IEnumerator LightAttackOn()
    {
        yield return new WaitForSeconds(0.3f);
        lightuattacku.enabled = true;
        LightAttack.enabled = true;
    }
    private IEnumerator LightAttackOff()
    {
        yield return new WaitForSeconds(0.6f);
        lightuattacku.enabled = false;
        LightAttack.enabled = false;
    }
    private IEnumerator HeavyAttackOn()
    {
        yield return new WaitForSeconds(0.8f);
        HeavyAttack.enabled = true;
    }
    private IEnumerator HeavyAttackOff()
    {
        yield return new WaitForSeconds(1f);

        HeavyAttack.enabled = false;
    }

    public IEnumerator PlayerOneStateMachine()
    {
      
        while (true)
        {
            switch (pUnoStates)
            {
                case pOneStates.pOneIdle:
                    playerOneIdle();
                    break;
                case pOneStates.pOneWalk:
                    playerOneWalk();
                    break;
                case pOneStates.pOneBackOff:
                    playerOneBackOff();
                    break;
                case pOneStates.pJump:
                    JUMP();
                    break;
                case pOneStates.fJump:
                    fJump();
                    break;
                case pOneStates.bJump:
                    bJump();
                    break;
                case pOneStates.Fall:
                    Fall();
                    break;
                case pOneStates.fFall:
                    Fall();
                    break;
                case pOneStates.bFall:
                    Fall();
                    break;
                case pOneStates.LP:
                    lightPunch();
                    break;
                case pOneStates.MP:
                    mediumPunch();
                    break;
                case pOneStates.HP:
                    hardPunch();
                    break;
                case pOneStates.LK:
                    lightKick();
                    break;
                case pOneStates.MK:
                    mediumKick();
                    break;
                case pOneStates.HK:
                    hardKick();
                    break;
                case pOneStates.WaitForAnimations:
                    WaitForAnimations();
                    break;
                case pOneStates.Demo:
                    Demo();
                    break;
                case pOneStates.pOneDead:
                    pOneDead();
                    break;

            }
            yield return null;
        }
    }

    void playerOneIdle()
    {
        
        pOneIdleAnim();

        if (Grounded())
        {
            return;
        }
        pOneMoveDir = new Vector3(0, pSpeedY, 0);

        pOneMoveDir = pOneTransform.TransformDirection(pOneMoveDir);

        collisionFlags = pOneController.Move(pOneMoveDir * Time.deltaTime);
    }
    private void playerOneWalk()
    {

        pOneWalkAnim();

        pOneMoveDir = new Vector3(+ pWalkSpeed, 0, 0);
        pOneMoveDir = pOneTransform.TransformDirection(pOneMoveDir).normalized;
        pOneMoveDir *= pWalkSpeed;

        collisionFlags = pOneController.Move(pOneMoveDir * Time.deltaTime);

        if (Input.GetAxis("Horizontal") == 0)             
        {
         
            pUnoStates = P1Movement.pOneStates.pOneIdle;
        }
    }
    private void JUMP()
    {

        playerJumpingAnimation();

        pOneMoveDir = new Vector3(0, pJumpSpeed * 5, 0);
        pOneMoveDir = pOneTransform.TransformDirection(pOneMoveDir).normalized;

        pOneMoveDir *= pJumpSpeed;

        collisionFlags = pOneController.Move(pOneMoveDir * Time.deltaTime);

        if (pOneTransform.transform.position.y >= pJumpHeight)
        {
 
            pUnoStates = P1Movement.pOneStates.Fall;
        }
    }
    private void Fall()
    {
        pOneMoveDir = new Vector3(0, pSpeedY, 0);
        pOneMoveDir = pOneTransform.TransformDirection(pOneMoveDir);

        collisionFlags = pOneController.Move(pOneMoveDir * Time.deltaTime);

        if (Grounded())
        {
            pUnoStates = P1Movement.pOneStates.pOneIdle;
        }

    }
    private void fJump()
    {

        playerJumpingAnimation();
        pOneMoveDir = new Vector3(-pJumpHori, pJumpSpeed / 6, 0);
        pOneMoveDir = pOneTransform.TransformDirection(pOneMoveDir);

        pOneMoveDir *= pJumpSpeed;

        collisionFlags = pOneController.Move(pOneMoveDir * Time.deltaTime);

        if (pOneTransform.transform.position.y >= pJumpHeight)
        {
            pUnoStates = P1Movement.pOneStates.fFall;
        }
    }
    private void bJump()
    {
        playerJumpingAnimation();

        pOneMoveDir = new Vector3(+ pJumpHori, pJumpSpeed / 6, 0);
        pOneMoveDir = pOneTransform.TransformDirection(pOneMoveDir);

        pOneMoveDir *= pJumpSpeed;

        collisionFlags = pOneController.Move(pOneMoveDir * Time.deltaTime);

        if (pOneTransform.transform.position.y >= pJumpHeight)
        {
    
            pUnoStates = P1Movement.pOneStates.bFall;
        }
    }
    private void fFall()
    {

        pOneMoveDir = new Vector3(-pJumpHori, pSpeedY, 0);
        pOneMoveDir = pOneTransform.TransformDirection(pOneMoveDir);

        collisionFlags = pOneController.Move(pOneMoveDir * Time.deltaTime);

        if (Grounded())
        {
            pUnoStates = P1Movement.pOneStates.pOneIdle;
        }

    }
    private void bFall()
    {
        pOneMoveDir = new Vector3(+ pJumpHori, pSpeedY, 0);
        pOneMoveDir = pOneTransform.TransformDirection(pOneMoveDir);

        collisionFlags = pOneController.Move(pOneMoveDir * Time.deltaTime);

        if (Grounded())
        {
            pUnoStates = P1Movement.pOneStates.pOneIdle;
        }
    }

    private void WaitForAnimations()
    {
 

        for (int w = 0; w < pAttacks.Length; w++)
        {
            if (pOneAnim.IsPlaying(pAttacks[w].name))
            {
                return;
            }
        }
        isPunchingHard = false;
        isPunchingLight = false;
        pUnoStates = P1Movement.pOneStates.pOneIdle;
    }
    private void playerOneBackOff()
    {
       
        pOneWalkAnim();

        pOneMoveDir = new Vector3(- pBackOffSpeed, 0, 0);
        pOneMoveDir = pOneTransform.TransformDirection(pOneMoveDir).normalized;

        pOneMoveDir *= pBackOffSpeed;

        collisionFlags = pOneController.Move(pOneMoveDir * Time.deltaTime);

        if (Input.GetAxis("Horizontal") == 0)              
        {
            pUnoStates = P1Movement.pOneStates.pOneIdle;
        }
    }
    
    private void Demo()
    {
        demoAnimation();

    }

    private void pOneDead()
    {
     

        pOneMoveDir = new Vector3(0, pSpeedY, 0);

        pOneMoveDir = pOneTransform.TransformDirection(pOneMoveDir);

        collisionFlags = pOneController.Move(pOneMoveDir * Time.deltaTime);

        if (pOneAnim.IsPlaying(pOneDeathAnim.name))
        {
            return;
        }
        StopCoroutine("PlayerOneStateMachine");
    }

    private void SetpOneDead()
    {


        pOneDead();
        pOneDeadAnimation();

        pUnoStates = P1Movement.pOneStates.pOneDead;
    }

    public void AttackInputManager()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            pUnoStates = P1Movement.pOneStates.LK;
        }
        if (Input.GetButtonDown("Fire2"))
        {
            pUnoStates = P1Movement.pOneStates.MK;
        }
        if (Input.GetButtonDown("Fire6"))
        {
            pUnoStates = P1Movement.pOneStates.HK;
        }
        if (Input.GetButtonDown("Fire3"))
        {
            pUnoStates = P1Movement.pOneStates.LP;
            isPunchingLight = true;
        }
        if (Input.GetButtonDown("Fire4"))
        {
            pUnoStates = P1Movement.pOneStates.MP;
        }
        if (Input.GetButtonDown("Fire5"))
        {
            pUnoStates = P1Movement.pOneStates.HP;
            isPunchingHard = true;
        }
    }


    public void InputManager()
    {
       
        if (Input.GetAxis("Horizontal") < deadZoneNega)
        {
            pUnoStates = P1Movement.pOneStates.pOneBackOff;
        }

        if (Input.GetAxis("Horizontal") > deadZonePos)
        {
            pUnoStates = P1Movement.pOneStates.pOneWalk;
        }

        if (Input.GetAxis("Vertical") > deadZonePos)
        {
            pUnoStates = P1Movement.pOneStates.pJump;
        }

        /*if (Input.GetAxis("Vertical") > deadZonePos && Input.GetAxis("Horizontal") > deadZonePos)
        {
            Debug.Log("B Jump");
            pUnoStates = P1Movement.pOneStates.bJump;
        }
        if (Input.GetAxis("Vertical") > deadZonePos && Input.GetAxis("Horizontal") < deadZoneNega)
        {
            Debug.Log("F Jump");
            pUnoStates = P1Movement.pOneStates.fJump;
        }*/

    }
    private void pOneIdleAnim()
    {

        pOneAnim.CrossFade(pOneIdle.name);
    }
    private void playerJumpingAnimation()
    {
        //pOneAnim.CrossFade(pOneJump.name);
    }

    private void LightPunch()
    {
        
        //pOneAnim.CrossFade(pAttacks[0].name);
    }
    private void MediumPunch()
    {
        //pOneAnim.CrossFade(pAttacks[1].name);
    }
    private void HardPunch()
    {
        //pOneAnim.CrossFade(pAttacks[2].name);
    }
    private void LightKick()
    {
        //pOneAnim.CrossFade(pAttacks[3].name);
    }
    private void MediumKick()
    {
        //pOneAnim.CrossFade(pAttacks[4].name);
    }
    private void HardKick()
    {
        //pOneAnim.CrossFade(pAttacks[5].name);
    }

    private void lightPunch()
    {

        LightPunch();
        StartCoroutine("LightAttackOn");
        pUnoStates = P1Movement.pOneStates.WaitForAnimations;
    }
    private void mediumPunch()
    {
        Debug.Log("Medium Punch");

        MediumPunch();

        pUnoStates = P1Movement.pOneStates.WaitForAnimations;
    }
    private void hardPunch()
    {
        HardPunch();
        StartCoroutine("HeavyAttackOn");
        pUnoStates = P1Movement.pOneStates.WaitForAnimations;
    }
    private void lightKick()
    {
        Debug.Log("Light Kick");

        LightKick();

        pUnoStates = P1Movement.pOneStates.WaitForAnimations;
    }
    private void mediumKick()
    {
        Debug.Log("Medium Kick");

        MediumKick();

        pUnoStates = P1Movement.pOneStates.WaitForAnimations;
    }
    private void hardKick()
    {
        Debug.Log("Hard Kick");

        HardKick();

        pUnoStates = P1Movement.pOneStates.WaitForAnimations;
    }

    private void demoAnimation()
    {
        pOneAnim.CrossFade(pOneDemoAnim.name);
    }

    private void pOneWalkAnim()
    {

        pOneAnim.CrossFade(pOneWalk.name);
    }
    private void playerOneBackingOff()
    {

        pOneAnim.CrossFade(pOneBackOff.name);
    }

    private void pOneDeadAnimation()
    {
        pOneAnim.CrossFade(pOneDeathAnim.name);
    }

    private void Gravity()
    {
    
        if (Grounded())
        {
            pSpeedY = 0f;
        }
        else
        {
            //Debug.Log("Gravity");
            pSpeedY -= pGravity * pGravMod * Time.deltaTime;
        }
    }

    public bool Grounded()
    {
       
        //Debug.Log("Grounded");
        return (collisionFlags & CollisionFlags.CollidedBelow) != 0;
       
    }


}
