using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickup : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            CurrencyManager currencyManager = FindObjectOfType<CurrencyManager>();
            if (currencyManager != null)
            {
                currencyManager.AddCoins(1); // Assuming each coin is worth 1
                Destroy(gameObject); // Destroy the coin after picking it up
            }
        }
    }
}