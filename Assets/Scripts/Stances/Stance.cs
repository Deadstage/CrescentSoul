using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stance : MonoBehaviour
{
    private CollisionSenses CollisionSenses { get => collisionSenses ?? core.GetCoreComponent(ref collisionSenses); }

    private CollisionSenses collisionSenses;

    [SerializeField] protected SO_WeaponData weaponData;

    protected Animator baseAnimator;
    protected Animator stanceAnimator;

    protected int attackCounter;
    private float resetTime;
    private float attackTimer = 1.0f;

    protected PlayerAttackState attackState;
    protected PlayerSecondaryAttackState secondaryAttackState;
    protected Core core;
    protected PlayerCrouchAttackState crouchState;

    protected virtual void Awake()
    {
        baseAnimator = transform.Find("Base").GetComponent<Animator>();
        stanceAnimator = transform.Find("Weapon").GetComponent<Animator>();
        resetTime = Time.time;
        gameObject.SetActive(false);
    }

    public virtual void EnterCrouchStance()
    {
        gameObject.SetActive(true);
        //Debug.Log("CroucStance Entered");

        if (attackCounter >= 3 || Time.time >= resetTime + attackTimer)
        {
            attackCounter = 4;
            //Debug.Log("attackCounter " + attackCounter);
        }

        if (collisionSenses.Ground)
        {
            //baseAnimator.SetBool("attack", true);
            //stanceAnimator.SetBool("attack", true);

            baseAnimator.SetBool("crouchAttack", true);
            stanceAnimator.SetBool("crouchAttack", true);

            attackCounter = 4;

            baseAnimator.SetBool("attack", true);
            stanceAnimator.SetBool("attack", true);

            attackCounter = 4;

            baseAnimator.SetInteger("attackCounter", 4);
            stanceAnimator.SetInteger("attackCounter", 4);

            attackCounter = 4;

            //Debug.Log("CrouchAttack");
            //Debug.Log("attackCounter =" + attackCounter);

            attackCounter = 4;
        }

        resetTime = Time.time;

    }

    public virtual void EnterStance()
    {
        //Debug.Log("isCrouching =" + crouchState.isCrouching);

        gameObject.SetActive(true);

        if (attackCounter >= 3 || Time.time >= resetTime + attackTimer)
        {
            attackCounter = 0;
        }

        if (CollisionSenses.Ground)
        {
            baseAnimator.SetBool("attack", true);
            stanceAnimator.SetBool("attack", true);

            baseAnimator.SetInteger("attackCounter", attackCounter);
            stanceAnimator.SetInteger("attackCounter", attackCounter);

            //Debug.Log("grounded");
        }

        else if (!CollisionSenses.Ground)
        {
            baseAnimator.SetBool("attack", true);
            stanceAnimator.SetBool("attack", true);

            baseAnimator.SetBool("airAttack", true);
            stanceAnimator.SetBool("airAttack", true);

            baseAnimator.SetInteger("attackCounter", -1);
            stanceAnimator.SetInteger("attackCounter", -1);

            attackCounter = 3;

            //Debug.Log("not grounded");
        }

        resetTime = Time.time;

    }

    public virtual void EnterSecondaryStance()
    {
        //Debug.Log("EnterSecondaryStance called");
        gameObject.SetActive(true);

        // if (attackCounter >= 3 || Time.time >= resetTime + attackTimer)
        // {
        //     attackCounter = 5;
        // }

        if (CollisionSenses.Ground)
        {
            //Debug.Log("Entering grounded secondary attack stance");
            baseAnimator.SetBool("secondaryAttack", true);
            stanceAnimator.SetBool("secondaryAttack", true);
            //Debug.Log("Bools set to true");

            attackCounter = 5;
            
            baseAnimator.SetInteger("attackCounter", 5);
            stanceAnimator.SetInteger("attackCounter", 5);
            //Debug.Log("Attack Counter set");


            //Debug.Log("grounded");
        }

        else if (!CollisionSenses.Ground)
        {
            baseAnimator.SetBool("secondaryAttack", true);
            stanceAnimator.SetBool("secondaryAttack", true);

            baseAnimator.SetBool("airAttack", true);
            stanceAnimator.SetBool("airAttack", true);

            baseAnimator.SetInteger("attackCounter", -2);
            stanceAnimator.SetInteger("attackCounter", -2);

            attackCounter = 3;

            //Debug.Log("not grounded");
        }

        resetTime = Time.time;
    }

    public virtual void ExitCrouchStance()
    {
        //Debug.Log("CrouchStance Exiting");

        if (collisionSenses.Ground)
        {
            //baseAnimator.SetBool("attack", false);
            //stanceAnimator.SetBool("attack", false);
            baseAnimator.SetBool("crouchAttack", false);
            stanceAnimator.SetBool("crouchAttack", false);

            baseAnimator.SetBool("attack", false);
            stanceAnimator.SetBool("attack", false);

            //Debug.Log("CrouchAttack false");

            attackCounter = 0;

            //Debug.Log("attackCounter =" + attackCounter);
        }

        gameObject.SetActive(false);
    }

    public virtual void ExitStance()
    {
        if (CollisionSenses.Ground)
        {
            baseAnimator.SetBool("attack", false);
            stanceAnimator.SetBool("attack", false);

            attackCounter++;
        }

        else if (!CollisionSenses.Ground)
        {
            baseAnimator.SetBool("attack", false);
            stanceAnimator.SetBool("attack", false);
            baseAnimator.SetBool("airAttack", false);
            stanceAnimator.SetBool("airAttack", false);

            attackCounter = 0;
        }

        gameObject.SetActive(false);
    }

    public virtual void ExitSecondaryStance()
    {
        //Debug.Log("Exiting Secondary Attack");

        if (CollisionSenses.Ground)
        {
            baseAnimator.SetBool("secondaryAttack", false);
            stanceAnimator.SetBool("secondaryAttack", false);
            //Debug.Log("Bools set to false");

            attackCounter = 0;
            //Debug.Log("attackCounter set to 0");
        }

        else if (!CollisionSenses.Ground)
        {
            baseAnimator.SetBool("secondaryAttack", false);
            stanceAnimator.SetBool("secondaryAttack", false);
            baseAnimator.SetBool("airAttack", false);
            stanceAnimator.SetBool("airAttack", false);

            attackCounter = 0;
        }

        //Debug.Log("Deactivating gameObject");
        gameObject.SetActive(false);
    }

    #region Animation Triggers

    public virtual void AnimationFinishTrigger()
    {
        attackState.AnimationFinishTrigger();
        secondaryAttackState.AnimationFinishTrigger();
        crouchState.AnimationFinishTrigger();
    }

    public virtual void AnimationStartMovementTrigger()
    {
        //Debug.Log("AnimationStartMovementTrigger called");
        //Debug.Log("Weapon movement speed: " + weaponData.movementSpeed[attackCounter]);
        attackState.SetPlayerVelocity(weaponData.movementSpeed[attackCounter]);
        secondaryAttackState.SetPlayerVelocity(weaponData.movementSpeed[attackCounter]);
        crouchState.SetPlayerVelocity(weaponData.movementSpeed[attackCounter]);
    }

    public virtual void AnimationStopMovementTrigger()
    {
        //Debug.Log("AnimationStopMovementTrigger called");
        attackState.SetPlayerVelocity(0f);
        secondaryAttackState.SetPlayerVelocity(0f);
        crouchState.SetPlayerVelocity(0f);
    }

    public virtual void AnimationStartMovementTriggerReverse()
    {
        attackState.SetPlayerReverseVelocity(weaponData.movementSpeed[attackCounter]);
        secondaryAttackState.SetPlayerReverseVelocity(weaponData.movementSpeed[attackCounter]);
        crouchState.SetPlayerReverseVelocity(weaponData.movementSpeed[attackCounter]);
    }

    public virtual void AnimationTurnOffFlipTrigger()
    {
        attackState.SetFlipCheck(false);
        secondaryAttackState.SetFlipCheck(false);
        crouchState.SetFlipCheck(false);
    }

    public virtual void AnimationTurnOnFlipTrigger()
    {
        attackState.SetFlipCheck(true);
        secondaryAttackState.SetFlipCheck(true);
        crouchState.SetFlipCheck(true);

    }

    public virtual void AnimationActionTrigger()
    {

    }

    public virtual void AnimationMoveDownwardsTrigger()
    {
        float speed = weaponData.movementSpeed[attackCounter]; // Get the speed from the weapon data
        float downwardSpeed = -Mathf.Abs(speed); // Ensure the speed is negative to move downwards

        attackState.SetPlayerVerticalVelocity(downwardSpeed);
        secondaryAttackState.SetPlayerVerticalVelocity(downwardSpeed);
        crouchState.SetPlayerVerticalVelocity(downwardSpeed);
    }

    public virtual void AnimationStartUpwardMovementTrigger()
    {
        attackState.SetPlayerUpwardVelocity(weaponData.movementSpeed[attackCounter]);
        secondaryAttackState.SetPlayerUpwardVelocity(weaponData.movementSpeed[attackCounter]);
        crouchState.SetPlayerUpwardVelocity(weaponData.movementSpeed[attackCounter]);
    }

    #endregion

    public void InitalizeStance(PlayerAttackState state, Core core)
    {
        this.attackState = state;
        this.core = core;
    }

    public void InitalizeSecondaryStance(PlayerSecondaryAttackState state, Core core)
    {
        //Debug.Log("Initializing Secondary Stance");
        this.secondaryAttackState = state;
        this.core = core;
        //Debug.Log("Secondary Stance Initialized");
    }

    public void InitalizeCrouchStance(PlayerCrouchAttackState state, Core core)
    {
        this.crouchState = state;
        this.core = core;
    }
}
