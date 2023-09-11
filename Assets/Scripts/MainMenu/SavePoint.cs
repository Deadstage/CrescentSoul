using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePoint : MonoBehaviour
{
    public PlayerInputHandler playerInputHandler;
    public GameManager gameManager; // Reference to your GameManager script
    public GameObject saveMenuUI; // Reference to the new save menu UI

    private bool playerInSavePoint = false;

    private void Update()
    {
        if (playerInSavePoint && playerInputHandler.InteractionInput)
        {
            OpenSaveMenu();
            playerInputHandler.UseInteractionInput();
        }
    }

    private void OpenSaveMenu()
    {
        saveMenuUI.SetActive(true); // Open the save menu UI
        playerInputHandler.isSaveMenuOpen = true; // Disable player movement and attacks
    }

    public void CloseSaveMenu()
    {
        saveMenuUI.SetActive(false); // Close the save menu UI
        playerInputHandler.isSaveMenuOpen = false; // Enable player movement and attacks
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