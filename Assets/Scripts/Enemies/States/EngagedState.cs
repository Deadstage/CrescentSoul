using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EngagedState : State
{
    protected Movement Movement { get => movement ?? core.GetCoreComponent(ref movement); }
    private CollisionSenses CollisionSenses { get => collisionSenses ?? core.GetCoreComponent(ref collisionSenses); }

    private Movement movement;
    private CollisionSenses collisionSenses;

    protected D_EngagedState stateData;


    protected bool isEngaged;
    protected bool isGrounded;
    protected bool isMovementStopped;
    //protected bool performCloseRangeAction;
    //protected bool isPlayerInMinAgroRange;

    public EngagedState(Entity etity, FiniteStateMachine stateMachine, string animBoolName, D_EngagedState stateData) : base(etity, stateMachine, animBoolName)
    {
        this.stateData = stateData;
    }

    public override void DoChecks()
    {
        base.DoChecks();

        isGrounded = CollisionSenses.Ground;
        //performCloseRangeAction = entity.CheckPlayerInCloseRangeAction();
        //isPlayerInMinAgroRange = entity.CheckPlayerInMinAgroRange();
    }

    public override void Enter()
    {
        base.Enter();

        if (sceneDictionary.isEngaged == true)
        {
            isEngaged = true;
            isMovementStopped = false;
        }
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (isEngaged && !isMovementStopped)
        {
            isMovementStopped = true;
            Movement?.SetVelocityX(0f);
        }
        else if (sceneDictionary.isEngaged == false)
        {
            isEngaged = false;
            
            if(isEngaged == false)
            {
                base.Exit();
            }
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
