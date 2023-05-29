using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParryHitboxHandler : MonoBehaviour
{
    private AggressiveStance stance;

    private void Awake()
    {
        stance = GetComponentInParent<AggressiveStance>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the collided object is an enemy attack
        if (collision.CompareTag("EnemyAttack"))
        {
            // Call a method in the AggressiveStance script to handle the parry
            stance.ParryDetected(collision);
        }

        // Get the Enemy3 component from the collided object
        Enemy3 enemy = collision.GetComponent<Enemy3>();
        if (enemy != null)
        {
            // If the enemy exists, call the Stun method
            enemy.stateMachine.ChangeState(enemy.stunState);
            enemy.stunState.SetStunDuration(3f);  // Stun the enemy for 3 seconds
        }
    }
}