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

    #region Animation Triggers

    public virtual void AnimationFinishTrigger()
    {
        attackState.AnimationFinishTrigger();
        crouchState.AnimationFinishTrigger();
    }

    public virtual void AnimationStartMovementTrigger()
    {
        attackState.SetPlayerVelocity(weaponData.movementSpeed[attackCounter]);
        crouchState.SetPlayerVelocity(weaponData.movementSpeed[attackCounter]);
    }

    public virtual void AnimationStopMovementTrigger()
    {
        attackState.SetPlayerVelocity(0f);
        crouchState.SetPlayerVelocity(0f);
    }

    public virtual void AnimationStartMovementTriggerReverse()
    {
        attackState.SetPlayerReverseVelocity(weaponData.movementSpeed[attackCounter]);
        crouchState.SetPlayerReverseVelocity(weaponData.movementSpeed[attackCounter]);
    }

    public virtual void AnimationTurnOffFlipTrigger()
    {
        attackState.SetFlipCheck(false);
        crouchState.SetFlipCheck(false);
    }

    public virtual void AnimationTurnOnFlipTrigger()
    {
        attackState.SetFlipCheck(true);
        crouchState.SetFlipCheck(true);

    }

    public virtual void AnimationActionTrigger()
    {

    }

    #endregion

    public void InitalizeStance(PlayerAttackState state, Core core)
    {
        this.attackState = state;
        this.core = core;
    }

    public void InitalizeCrouchStance(PlayerCrouchAttackState state, Core core)
    {
        this.crouchState = state;
        this.core = core;
    }
}
