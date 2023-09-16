using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TeleportGate : MonoBehaviour
{
    public PlayerInputHandler playerInputHandler;
    public TeleportManager teleportManager; // Reference to your TeleportManager script
    public TeleportMenu teleportMenu; // Reference to the TeleportMenu script

    private bool playerAtTeleportGate = false;

    private void Awake()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "CS") // replace with your actual game scene name
        {
            AssignReferences();
        }
    }

    private void AssignReferences()
    {
        if (playerInputHandler == null)
        {
            playerInputHandler = FindObjectOfType<PlayerInputHandler>();
            if (playerInputHandler == null)
            {
                Debug.LogError("PlayerInputHandler not found in the scene");
            }
        }

        if (teleportMenu == null)
        {
            TeleportMenu[] teleportMenus = FindObjectsOfType<TeleportMenu>(true);
            if (teleportMenus.Length > 0)
            {
                teleportMenu = teleportMenus[0];
            }
            else
            {
                Debug.LogError("TeleportMenu not found in the scene");
            }
        }
    }

    private void Update()
    {
        if (playerAtTeleportGate && playerInputHandler.InteractionInput)
        {
            Debug.Log("Interaction input detected in TeleportGate script");
            OpenTeleportMenu();
            playerInputHandler.UseInteractionInput();
        }
    }

    private void OpenTeleportMenu()
    {
        teleportMenu.ShowMenu(); // Open the teleport menu UI
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerAtTeleportGate = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerAtTeleportGate = false;
        }
    }
}