using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParryHitboxHandler : MonoBehaviour
{
    private AggressiveStance stance;
    public AudioSource parrySound;
    public GameObject parryParticlePrefab;
    public Transform parryParticlePoint;
    public GameObject screenFlashObject; // Added this
    private ScreenFlash screenFlash; // Changed this to private

    private void Awake()
    {
        stance = GetComponentInParent<AggressiveStance>();
        if (screenFlashObject != null)
        {
            screenFlash = screenFlashObject.GetComponent<ScreenFlash>(); // Get the ScreenFlash component from the object
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the collided object is an enemy attack
        if (collision.CompareTag("EnemyAttack"))
        {
            // Get the Enemy3 component from the parent of the collided object
            Enemy3 enemy = collision.transform.parent.GetComponent<Enemy3>();
            if (enemy != null)
            {
                // If the enemy exists, call the Stun method
                enemy.stateMachine.ChangeState(enemy.stunState);
                enemy.stunState.SetStunDuration(3f);  // Stun the enemy for X seconds
            }
            else
            {
                Debug.Log("Did not find enemy on parent of collided object.");
            }

            // Instantiate the parry effect prefab at the ParryParticlePoint's position with a random rotation
            if(parryParticlePoint != null)
            {
                Instantiate(parryParticlePrefab, parryParticlePoint.position, Quaternion.Euler(0f, 0f, Random.Range(0f, 360f)));
            }
            else
            {
                Debug.Log("ParryParticlePoint reference missing.");
            }

            // Play the parry sound
            if (parrySound != null)
            {
                parrySound.Play();
            }

            // Flash the screen
            if (screenFlash != null)
            {
                screenFlash.FlashScreen();
            }
        }
    }
}