using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinTrigger : MonoBehaviour
{
    public int coinValue = 1;
    private CurrencyManager currencyManager;

    private void Start()
    {
        GameObject manager = GameObject.Find("CurrencyManager");
        if (manager != null)
        {
            currencyManager = manager.GetComponent<CurrencyManager>();
        }
        else
        {
            Debug.LogError("CurrencyManager not found.");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("PlayerCollector"))
        {
            if (currencyManager != null)
            {
                currencyManager.AddCoins(1);
                Destroy(transform.parent.gameObject);  // Assuming the CoinTrigger is a child of the actual coin GameObject
            }
            else
            {
                Debug.LogError("CurrencyManager is null.");
            }
        }
    }
}