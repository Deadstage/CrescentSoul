using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E5_PlayerDetectedState : PlayerDetectedState
{
    protected new Movement Movement { get => movement ?? core.GetCoreComponent(ref movement); }

    private Movement movement;

    private Enemy5 enemy;

    //Player State Detection
    public PlayerEnemyCache playerEnemyCache;
    

    public E5_PlayerDetectedState(Entity etity, FiniteStateMachine stateMachine, string animBoolName, D_PlayerDetected stateData, Enemy5 enemy) : base(etity, stateMachine, animBoolName, stateData)
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
                stateMachine.ChangeState(enemy.moveState);
            }
        }

        // else if (playerEnemyCache.playerIsStunned == true)
        // {
        //     stateMachine.ChangeState(enemy.chargeState);
        // }

        //------------------------------------------------------------------

        // else if (isPlayerStunned == true && performCloseRangeAction)
        // {
        //     stateMachine.ChangeState(enemy.chargeState);
        // }
        // else if (isPlayerStunned == true && performLongRangeAction)
        // {
        //     stateMachine.ChangeState(enemy.chargeState);
        // }
        // else if (isPlayerStunned == true && !isPlayerInMaxAgroRange)
        // {
        //     stateMachine.ChangeState(enemy.chargeState);
        // }
        // else if (isPlayerStunned == true && !isDetectingLedge)
        // {
        //     movement?.Flip();
        //     stateMachine.ChangeState(enemy.chargeState);
        // }

        //------------------------------------------------------------------

        // else if (isPlayerStunned == false && performCloseRangeAction)
        // {
        //     stateMachine.ChangeState(enemy.meleeAttackState);
        // }
        // else if (isPlayerStunned == false && performLongRangeAction)
        // {
        //     stateMachine.ChangeState(enemy.chargeState);
        // }
        // else if (isPlayerStunned == false && !isPlayerInMaxAgroRange)
        // {
        //     stateMachine.ChangeState(enemy.lookForPlayerState);
        // }
        // else if (isPlayerStunned == false && !isDetectingLedge)
        // {
        //     Movement?.Flip();
        //     stateMachine.ChangeState(enemy.moveState);
        // }

    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
