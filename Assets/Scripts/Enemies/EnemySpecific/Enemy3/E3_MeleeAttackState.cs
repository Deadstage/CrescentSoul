using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E3_MeleeAttackState : MeleeAttackState
{
    private Enemy3 enemy;
    private GameObject attackCollider;

    public E3_MeleeAttackState(Entity etity, FiniteStateMachine stateMachine, string animBoolName, Transform attackPosition, D_MeleeAttack stateData, Enemy3 enemy, GameObject attackCollider) : base(etity, stateMachine, animBoolName, attackPosition, stateData)
    {
        this.enemy = enemy;
        this.attackCollider = attackCollider;
        this.attackCollider.SetActive(false);  // Ensure the attack collider is initially disabled
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
        attackCollider.SetActive(true);  // Enable the attack collider when the enemy starts the attack
    }

    public override void Exit()
    {
        base.Exit();
        attackCollider.SetActive(false);  // Disable the attack collider when the enemy finishes the attack
    }

    public override void FinishAttack()
    {
        base.FinishAttack();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (isAnimationFinished)
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

    public override void TriggerAttack()
    {
        base.TriggerAttack();
    }
}
