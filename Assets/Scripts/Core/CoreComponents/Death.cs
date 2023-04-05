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

    private void OnEnable()
    {
        Stats.HealthZero += Die;
    }

    private void OnDisable()
    {
        Stats.HealthZero -= Die;
    }

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
        // else
        // {
        //     core.transform.parent.gameObject.SetActive(false);
        // }

    }
}
