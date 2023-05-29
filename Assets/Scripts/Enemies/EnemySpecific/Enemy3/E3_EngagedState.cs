using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E3_EngagedState : EngagedState
{
    private Enemy3 enemy;
    public E3_EngagedState(Entity etity, FiniteStateMachine stateMachine, string animBoolName, D_EngagedState stateData, Enemy3 enemy) : base(etity, stateMachine, animBoolName, stateData)
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
