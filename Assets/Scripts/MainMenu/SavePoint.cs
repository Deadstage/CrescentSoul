using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePoint : MonoBehaviour
{
    public PlayerInputHandler playerInputHandler;
    public GameManager gameManager; // Reference to your GameManager script

    private bool playerInSavePoint = false;

    private void Update()
    {
        if (playerInSavePoint && playerInputHandler.InteractionInput)
        {
            gameManager.SaveGame(); // Calls the SaveGame method in the GameManager script
            playerInputHandler.UseInteractionInput();  // Reset the interaction input
            Debug.Log("Game saved at save point.");
        }
    }

    private void OnTriggerEnter2D(Collider2D other) // Updated to 2D
    {
        if (other.CompareTag("Player"))
        {
            playerInSavePoint = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other) // Updated to 2D
    {
        if (other.CompareTag("Player"))
        {
            playerInSavePoint = false;
        }
    }
}