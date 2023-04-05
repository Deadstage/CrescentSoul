using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E2_PlayerDetectedState : PlayerDetectedState
{
    private Enemy2 enemy;

    public PlayerEnemyCache playerEnemyCache;

    public E2_PlayerDetectedState(Entity etity, FiniteStateMachine stateMachine, string animBoolName, D_PlayerDetected stateData, Enemy2 enemy) : base(etity, stateMachine, animBoolName, stateData)
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

            if (Time.time >= enemy.dodgeState.startTime + enemy.dodgeStateData.dodgeCooldown && playerEnemyCache.playerIsStunned == true)
            {
                stateMachine.ChangeState(enemy.chargeState);
            }

            else if (Time.time >= enemy.dodgeState.startTime + enemy.dodgeStateData.dodgeCooldown && playerEnemyCache.playerIsStunned == false)
            {
                stateMachine.ChangeState(enemy.dodgeState);
            }
            
            else if (playerEnemyCache.playerIsStunned == true && performCloseRangeAction)
            {
                stateMachine.ChangeState(enemy.chargeState);
            }

            else if (playerEnemyCache.playerIsStunned == false && performCloseRangeAction)
            {
                stateMachine.ChangeState(enemy.meleeAttackState);
            }

        }

        else if (performLongRangeAction)
        {
            playerEnemyCache?.CheckPlayerState();

            if(playerEnemyCache.playerIsStunned == true && performLongRangeAction)
            {
                stateMachine.ChangeState(enemy.chargeState);
            }

            else if (playerEnemyCache.playerIsStunned == false && performLongRangeAction)
            {
                stateMachine.ChangeState(enemy.rangedAttackState);
            }
        }
        
        else if (!isPlayerInMaxAgroRange)
        {
            stateMachine.ChangeState(enemy.lookForPlayerState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
