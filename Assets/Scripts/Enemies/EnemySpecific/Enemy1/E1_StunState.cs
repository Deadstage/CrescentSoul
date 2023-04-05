using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E1_StunState : StunState
{

    private Enemy1 enemy;

    //Player State Detection
    public PlayerEnemyCache playerEnemyCache;

    public E1_StunState(Entity etity, FiniteStateMachine stateMachine, string animBoolName, D_StunState stateData, Enemy1 enemy) : base(etity, stateMachine, animBoolName, stateData)
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

        if (isStunTimeOver)
        {
            playerEnemyCache?.CheckPlayerState();

            if (performCloseRangeAction && playerEnemyCache.playerIsStunned == true)
            {
                stateMachine.ChangeState(enemy.chargeState);
            }
            else if (performCloseRangeAction && playerEnemyCache.playerIsStunned == false)
            {
                stateMachine.ChangeState(enemy.meleeAttackState);
            }
            else if (isPlayerInMinAgroRange)
            {
                stateMachine.ChangeState(enemy.chargeState);
            }
            else
            {
                enemy.lookForPlayerState.SetTurnImmediately(true);
                stateMachine.ChangeState(enemy.lookForPlayerState);
            }
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
