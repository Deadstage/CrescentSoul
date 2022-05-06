using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCrouchIdleState : PlayerGroundedState
{
    public bool isCrouching;

    public PlayerCrouchIdleState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        Movement?.SetVelocityZero();
        player.SetColliderHeight(playerData.crouchColliderHeight);
        isCrouching = true;
    }

    public override void Exit()
    {
        base.Exit();
        player.SetColliderHeight(playerData.standColliderHeight);
        isCrouching = false;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (!isExitingState)
        {
            if(xInput != 0)
            {
                stateMachine.ChangeState(player.CrouchMoveState);
            }
            else if(yInput != -1 && !isTouchingCeiling)
            {
                stateMachine.ChangeState(player.IdleState);
            }
        }
    }
}
