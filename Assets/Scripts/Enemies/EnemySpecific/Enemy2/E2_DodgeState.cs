using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E2_DodgeState : DodgeState
{
    private Enemy2 enemy;

    public PlayerEnemyCache playerEnemyCache;
    public E2_DodgeState(Entity etity, FiniteStateMachine stateMachine, string animBoolName, D_DodgeState stateData, Enemy2 enemy) : base(etity, stateMachine, animBoolName, stateData)
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

        if (isDodgeOver)
        {
            playerEnemyCache?.CheckPlayerState();

            if (isPlayerInMaxAgroRange && performCloseRangeAction && playerEnemyCache.playerIsStunned == true)
            {
                stateMachine.ChangeState(enemy.chargeState);
            }
            else if (isPlayerInMaxAgroRange && performCloseRangeAction && playerEnemyCache.playerIsStunned == false)
            {
                stateMachine.ChangeState(enemy.meleeAttackState);
            }

            else if (isPlayerInMaxAgroRange && !performCloseRangeAction && playerEnemyCache.playerIsStunned == true)
            {
                stateMachine.ChangeState(enemy.chargeState);
            }
            else if (isPlayerInMaxAgroRange && !performCloseRangeAction && playerEnemyCache.playerIsStunned == false)
            {
                stateMachine.ChangeState(enemy.rangedAttackState);
            }

            else if (!isPlayerInMaxAgroRange)
            {
                stateMachine.ChangeState(enemy.lookForPlayerState);
            }

            //TODO: ranged attack state
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
