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

    protected PlayerAttackState state;
    protected Core core;
    protected PlayerCrouchIdleState crouch;

    protected virtual void Awake()
    {
        baseAnimator = transform.Find("Base").GetComponent<Animator>();
        stanceAnimator = transform.Find("Weapon").GetComponent<Animator>();
        resetTime = Time.time;
        gameObject.SetActive(false);
    }

    public virtual void EnterStance()
    {
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

            Debug.Log("grounded");
        }

        else if (CollisionSenses.Ground && crouch.isCrouching)
        {
            baseAnimator.SetBool("attack", true);
            stanceAnimator.SetBool("attack", true);
        
            baseAnimator.SetBool("crouchAttack", true);
            stanceAnimator.SetBool("crouchAttack", true);
        
            baseAnimator.SetInteger("attackCounter", -1);
            stanceAnimator.SetInteger("attackCounter", -1);
        
            attackCounter = 3;
        
            Debug.Log("grounded");
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

            Debug.Log("not grounded");
        }

        resetTime = Time.time;

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
        state.AnimationFinishTrigger();
    }

    public virtual void AnimationStartMovementTrigger()
    {
        state.SetPlayerVelocity(weaponData.movementSpeed[attackCounter]);
    }

    public virtual void AnimationStopMovementTrigger()
    {
        state.SetPlayerVelocity(0f);
    }

    public virtual void AnimationStartMovementTriggerReverse()
    {
        state.SetPlayerReverseVelocity(weaponData.movementSpeed[attackCounter]);
    }

    public virtual void AnimationTurnOffFlipTrigger()
    {
        state.SetFlipCheck(false);
    }

    public virtual void AnimationTurnOnFlipTrigger()
    {
        state.SetFlipCheck(true);

    }

    public virtual void AnimationActionTrigger()
    {

    }

    #endregion

    public void InitalizeStance(PlayerAttackState state, Core core)
    {
        this.state = state;
        this.core = core;
    }
}
