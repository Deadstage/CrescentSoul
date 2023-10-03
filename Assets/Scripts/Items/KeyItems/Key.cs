using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    public string keyColor;  // The color of the key (e.g., "Red", "Blue", etc.)
    public Sprite keySprite;  // The sprite of the key
    public string keyDescription;  // The description of the key

    private bool playerInKeyArea = false; // To check if the player is in the key's area
    public PlayerInputHandler playerInputHandler; // Reference to the PlayerInputHandler script

    private void Awake()
    {
        // Find the PlayerInputHandler script if it's not already set
        if (playerInputHandler == null)
        {
            playerInputHandler = GameObject.FindObjectOfType<PlayerInputHandler>();
        }
    }

    private void Start()
    {
        // Find the KeyInventory script
        KeyInventory keyInventory = GameObject.FindObjectOfType<KeyInventory>();

        if (keyInventory != null)
        {
            // Check if the player already has this key in their inventory
            if (keyInventory.HasKey(keyColor))
            {
                // Destroy the key GameObject if the player already has it
                Destroy(gameObject);
            }
        }
        else
        {
            Debug.LogError("KeyInventory not found");
        }
    }

    private void Update()
    {
        if (playerInKeyArea && playerInputHandler.InteractionInput)
        {
            // Find the KeyInventory script
            KeyInventory keyInventory = GameObject.FindObjectOfType<KeyInventory>();
            
            if (keyInventory != null)
            {
                // Add this key to the player's inventory
                keyInventory.AddKey(keyColor, keySprite, keyDescription);
            }
            
            // Destroy the key gameObject
            Destroy(gameObject);

            // Reset the interaction input
            playerInputHandler.UseInteractionInput();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInKeyArea = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInKeyArea = false;
        }
    }
}