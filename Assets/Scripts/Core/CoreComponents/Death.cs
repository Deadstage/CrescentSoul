using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death : CoreComponent
{
    public bool isDead;
    private Stats Stats { get => stats ?? core.GetCoreComponent(ref stats); }
    private Stats stats;

    private Movement Movement { get => movement ?? core.GetCoreComponent(ref movement); }
    private Movement movement;

    private Combat Combat { get => combat ?? core.GetCoreComponent(ref combat); }
    private Combat combat;

    private Rigidbody2D deathRB;


    public override void Init(Core core)
    {
        base.Init(core);

        Stats.HealthZero += Die;
    }

    // private void OnEnable()
    // {
    //     if (Stats != null)
    //     {
    //         Stats.HealthZero += Die;
    //     }
    // }

    // private void OnDisable()
    // {
    //     if (Stats != null)
    //     {
    //         Stats.HealthZero -= Die;
    //     }
    // }

    public void Die()
    {
        isDead = true;
        Combat.canImmuneKnockback = true;
        deathRB = core.transform.parent.gameObject.GetComponent<Rigidbody2D>();
        deathRB.constraints = RigidbodyConstraints2D.FreezePosition;

        BoxCollider2D[] colliders = core.transform.parent.gameObject.GetComponentsInChildren<BoxCollider2D>();

        foreach(BoxCollider2D collider2D in colliders)
        {
            collider2D.enabled = false;
        }

        Movement.SetVelocityX(0f);

        Enemy1 enemy1 = core.transform.parent.gameObject.GetComponent<Enemy1>();
        if (enemy1 != null)
        {   
            Combat.canImmuneKnockback = true;
            enemy1.stateMachine.ChangeState(enemy1.deadState);
            Movement.SetVelocityX(0f);
        }

        Enemy2 enemy2 = core.transform.parent.gameObject.GetComponent<Enemy2>();
        if (enemy2 != null){
            Combat.canImmuneDamage = true;
            enemy2.stateMachine.ChangeState(enemy2.deadState);
            Movement.SetVelocityX(0f);
        }

        Enemy3 enemy3 = core.transform.parent.gameObject.GetComponent<Enemy3>();
        if (enemy3 != null){
            Combat.canImmuneDamage = true;
            enemy3.stateMachine.ChangeState(enemy3.deadState);
            Movement.SetVelocityX(0f);
        }

        Enemy4 enemy4 = core.transform.parent.gameObject.GetComponent<Enemy4>();
        if (enemy4 != null){
            Combat.canImmuneDamage = true;
            enemy4.stateMachine.ChangeState(enemy4.deadState);
            Movement.SetVelocityX(0f);
        }

        Enemy5 enemy5 = core.transform.parent.gameObject.GetComponent<Enemy5>();
        if (enemy5 != null){
            Combat.canImmuneDamage = true;
            enemy5.stateMachine.ChangeState(enemy5.deadState);
            Movement.SetVelocityX(0f);
        }

        Enemy6 enemy6 = core.transform.parent.gameObject.GetComponent<Enemy6>();
        if (enemy6 != null){
            Combat.canImmuneDamage = true;
            enemy6.stateMachine.ChangeState(enemy6.deadState);
            Movement.SetVelocityX(0f);
        }

    }
}
