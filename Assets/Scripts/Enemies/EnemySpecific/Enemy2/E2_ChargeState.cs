using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E2_ChargeState : ChargeState
{
    private Enemy2 enemy;

    //Player State Detection
    public PlayerEnemyCache playerEnemyCache;

    public E2_ChargeState(Entity etity, FiniteStateMachine stateMachine, string animBoolName, D_ChargeState stateData, Enemy2 enemy) : base(etity, stateMachine, animBoolName, stateData)
    {
        this.enemy = enemy;
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
        playerEnemyCache = GameObject.FindObjectOfType<PlayerEnemyCache>();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (performCloseRangeAction)
        {
            playerEnemyCache?.CheckPlayerState();

            if(playerEnemyCache.playerIsStunned == true)
            {
                stateMachine.ChangeState(enemy.chargeState);
            }

            else if(playerEnemyCache.playerIsStunned == false && performCloseRangeAction)
            {
                stateMachine.ChangeState(enemy.meleeAttackState);
            }
        }

        else if (!isDetectingLedge || isDetectingWall)
        {
            stateMachine.ChangeState(enemy.lookForPlayerState);
        }

        else if(isChargeTimeOver)
        {
            if (isPlayerInMinAgroRange)
            {
                stateMachine.ChangeState(enemy.playerDetectedState);
            }
            else
            {
                stateMachine.ChangeState(enemy.lookForPlayerState);
            }
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
