using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCrouchAttackState : PlayerAbilityState
{
    private Stance stance;

    private int xInput;

    private float velocityToSet;

    private bool setVelocity;
    private bool shouldCheckFlip;

    public bool isCrouching;

    private Stats stats;

    public PlayerCrouchAttackState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {

    }

    public override void Enter()
    {
        //Debug.Log("crouchAttackState Entered");
        base.Enter();

        stats = player.stats;
        //Debug.Log("stats called");

        //setVelocity = false;
        isCrouching = true;
        //Debug.Log("isCrouching =" + isCrouching);

        stance.EnterCrouchStance();
        //Debug.Log("Stance called");

        //stats.InternalDecreaseStamina(20);

        Movement?.SetVelocityZero();
        player.SetColliderHeight(playerData.crouchColliderHeight);
    }

    public override void Exit()
    {
        base.Exit();

        stance.ExitCrouchStance();
        player.SetColliderHeight(playerData.standColliderHeight);
        isCrouching = false;
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
            setVelocity = false;
        }
    }

    public void SetStance(Stance stance)
    {
        this.stance = stance;
        this.stance.InitalizeCrouchStance(this, core);
    }

    public void SetPlayerVelocity(float velocity)
    {
        Movement?.SetVelocityX(velocity * Movement.FacingDirection);

        velocityToSet = velocity;
        setVelocity = true;
    }

    public void SetPlayerReverseVelocity(float velocity)
    {
        Movement?.SetVelocityX(velocity * -Movement.FacingDirection);

        velocityToSet = velocity;
        setVelocity = true;
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
