using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E3_StunState : StunState
{

    private Enemy3 enemy;

    //Player State Detection
    public PlayerEnemyCache playerEnemyCache;

    public E3_StunState(Entity etity, FiniteStateMachine stateMachine, string animBoolName, D_StunState stateData, Enemy3 enemy) : base(etity, stateMachine, animBoolName, stateData)
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
        //Debug.Log("Entered Stun State");
    }

    public override void Exit()
    {
        base.Exit();
        //Debug.Log("Exited Stun State");
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
                enemy.lookForPlayerState.SetTurnImmediately(false);
                stateMachine.ChangeState(enemy.lookForPlayerState);
            }
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public void SetStunDuration(float duration)
    {
        this.stateData.stunTime = duration;
        //Debug.Log("Stun duration has been set to: " + duration);
    }

    public void ResetStunTimer()
    {
        isStunTimeOver = false;
        startTime = Time.time;
        //Debug.Log("Stun timer has been reset.");
    }
}
