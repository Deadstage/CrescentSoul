using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E2_EngagedState : EngagedState
{
    private Enemy2 enemy;
    public E2_EngagedState(Entity etity, FiniteStateMachine stateMachine, string animBoolName, D_EngagedState stateData, Enemy2 enemy) : base(etity, stateMachine, animBoolName, stateData)
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
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (!isEngaged)
        {
            enemy.lookForPlayerState.SetTurnImmediately(true);
            stateMachine.ChangeState(enemy.lookForPlayerState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

}