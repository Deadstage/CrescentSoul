using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E7_PlayerDetectedState : PlayerDetectedState
{
    protected new Movement Movement { get => movement ?? core.GetCoreComponent(ref movement); }

    private Movement movement;

    private Enemy7 enemy;

    //Player State Detection
    public PlayerEnemyCache playerEnemyCache;
    

    public E7_PlayerDetectedState(Entity etity, FiniteStateMachine stateMachine, string animBoolName, D_PlayerDetected stateData, Enemy7 enemy) : base(etity, stateMachine, animBoolName, stateData)
    {
        this.enemy = enemy;
    }

    public override void Enter()
    {
        base.Enter();
        playerEnemyCache = GameObject.FindObjectOfType<PlayerEnemyCache>();
        
        //Player State Detection
        //playerEnemyCache.CheckPlayerState();
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
                //Debug.Log("Charging");
                stateMachine.ChangeState(enemy.chargeState);
            }

            else if(playerEnemyCache.playerIsStunned == false && performCloseRangeAction)
            {
                //Debug.Log("Attacking");
                stateMachine.ChangeState(enemy.meleeAttackState);
            }
        }

        else if (performLongRangeAction)
        {
            playerEnemyCache?.CheckPlayerState();

            if(playerEnemyCache.playerIsStunned == true)
            {
                stateMachine.ChangeState(enemy.chargeState);
            }

            else if(playerEnemyCache.playerIsStunned == false && performLongRangeAction)
            {
                stateMachine.ChangeState(enemy.chargeState);
            }
        }

        else if (!isPlayerInMaxAgroRange)
        {
            playerEnemyCache?.CheckPlayerState();

            if(playerEnemyCache.playerIsStunned == true)
            {
                stateMachine.ChangeState(enemy.chargeState);
            }

            else if(playerEnemyCache.playerIsStunned == false && !isPlayerInMaxAgroRange)
            {
                stateMachine.ChangeState(enemy.lookForPlayerState);
            }
        }

        else if (!isDetectingLedge)
        {
            playerEnemyCache?.CheckPlayerState();

            if(playerEnemyCache.playerIsStunned == true)
            {   
                Movement?.Flip();
                stateMachine.ChangeState(enemy.chargeState);
            }

            else if(playerEnemyCache.playerIsStunned == false && !isDetectingLedge)
            {
                Movement?.Flip();
                stateMachine.ChangeState(enemy.lookForPlayerState);
            }
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}