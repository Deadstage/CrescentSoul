using UnityEngine;

public class MemoryObject : MonoBehaviour
{
    public Memories MemoryData; // The data for this memory
    private bool playerInRange = false; // To check if the player is in range to collect the memory

    private PlayerInputHandler playerInputHandler;
    private MemoriesManager memoriesManager;

    private void Start()
    {
        playerInputHandler = FindObjectOfType<PlayerInputHandler>();
        memoriesManager = FindObjectOfType<MemoriesManager>();

        if (MemoryData == null)
        {
            Debug.LogError("MemoryData is not assigned on " + gameObject.name);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }

    private void Update()
    {
        if (playerInRange)
        {
            if (playerInputHandler != null)
            {
                if (playerInputHandler.InteractionInput)
                {
                    //Debug.Log("Interaction input detected");
                    CollectMemory();
                    playerInputHandler.UseInteractionInput(); // Reset the interaction input
                }
                else
                {
                    //Debug.Log("Interaction input not detected");
                }
            }
            else
            {
                //Debug.Log("PlayerInputHandler not found");
            }
        }
    }

    private void CollectMemory()
    {
        if (memoriesManager != null && MemoryData != null)
        {
            if (!memoriesManager.IsMemoryCollected(MemoryData.ID))
            {
                Debug.Log("Memory collected");
                memoriesManager.AddMemory(MemoryData);
                gameObject.SetActive(false); // Deactivate this GameObject to indicate the memory has been collected
            }
            else
            {
                Debug.Log("Memory already collected");
            }
        }
        else
        {
            if (memoriesManager == null)
            {
                Debug.Log("MemoriesManager not found");
            }
            if (MemoryData == null)
            {
                Debug.Log("MemoryData is null");
            }
        }
    }
}