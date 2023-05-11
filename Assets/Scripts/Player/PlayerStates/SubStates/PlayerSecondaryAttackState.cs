using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSecondaryAttackState : PlayerAbilityState
{
    private Stance stance;

    private int xInput;

    private float velocityToSet;

    private bool setVelocity;
    private bool shouldCheckFlip;

    private Stats stats;


    public PlayerSecondaryAttackState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        //Debug.Log("SecondaryAttackState Entered");

        stats = player.stats;
        //Debug.Log("Stats defined");
        //Debug.Log("Current Velocity:" + Movement.CurrentVelocity);

        setVelocity = false;
        //Debug.Log("Velocity set");

        stance.EnterSecondaryStance();
        //Debug.Log("stance.EnterSecondaryStance called");

        stats.InternalDecreaseStamina(10);
        //Debug.Log("Stamina spent");
    }

    public override void Exit()
    {
        //Debug.Log("Exiting SecondaryAttackState");
        base.Exit();

        stance.ExitSecondaryStance();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        //Debug.Log("Logic Updating");

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
        //Debug.Log("SetStance called");
        this.stance = stance;
        this.stance.InitalizeSecondaryStance(this, core);
        //Debug.Log("Setstance Initalized");
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