using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackState : PlayerAbilityState
{
    private Stance stance;

    private int xInput;

    private float velocityToSet;

    private bool setVelocity;
    private bool shouldCheckFlip;

    private Stats stats;


    public PlayerAttackState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        //Debug.Log("PrimaryAttackState Entered");

        stats = player.stats;
        //Debug.Log("Current Velocity:" + Movement.CurrentVelocity);

        setVelocity = false;

        stance.EnterStance();

        stats.InternalDecreaseStamina(20);
    }

    public override void Exit()
    {
        base.Exit();
        //Debug.Log("PrimaryAttackState Exit");

        stance.ExitStance();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        xInput = player.InputHandler.NormInputX;

        if (shouldCheckFlip)
        {
            Movement?.CheckIfShouldFlip(xInput);
        }

        if (setVelocity)
        {
            Movement?.SetVelocityX(velocityToSet * Movement.FacingDirection);
        }
    }

    public void SetStance(Stance stance)
    {
        this.stance = stance;
        this.stance.InitalizeStance(this, core);
        //Debug.Log("Current Velocity(SetStance):" + Movement.CurrentVelocity);
    }

    public void SetPlayerVelocity(float velocity)
    {
        Movement?.SetVelocityX(velocity * Movement.FacingDirection);
        //Debug.Log("Current Velocity(SetPlayerVelocity):" + Movement.CurrentVelocity);

        velocityToSet = velocity;
        setVelocity = true;
        //Debug.Log("Current Velocity(SetPlayerVelocity):" + Movement.CurrentVelocity);
    }

    public void SetPlayerReverseVelocity(float velocity)
    {
        Movement?.SetVelocityX(velocity * -Movement.FacingDirection);

        velocityToSet = velocity;
        setVelocity = true;
    }

    public void SetPlayerVerticalVelocity(float velocity)
    {
        Movement?.SetVelocityY(velocity);
    }

    public void SetPlayerUpwardVelocity(float velocity)
    {
        Movement?.SetVelocityY(velocity);
    }

    public void SetFlipCheck(bool value)
    {
        shouldCheckFlip = value;
    }


    #region Animation Triggers

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();

        isAbilityDone = true;
    }

    #endregion
}
