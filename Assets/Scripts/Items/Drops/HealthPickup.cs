using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    public enum HealthType { Small, Medium, Large }
    public HealthType healthType;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Stats playerStats = collision.GetComponentInChildren<Stats>();
            if (playerStats != null)
            {
                float healAmount = 0f;

                switch (healthType)
                {
                    case HealthType.Small:
                        healAmount = playerStats.maxHealth * 0.1f;
                        break;
                    case HealthType.Medium:
                        healAmount = playerStats.maxHealth * 0.2f;
                        break;
                    case HealthType.Large:
                        healAmount = playerStats.maxHealth * 0.3f;
                        break;
                }

                playerStats.IncreaseHealth(healAmount);
                Destroy(gameObject); // Destroy the health item after use
            }
        }
    }
}