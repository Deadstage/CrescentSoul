using System.Collections;
using UnityEngine;

public class Door : MonoBehaviour
{
    public string doorColor;  // The color of the door (e.g., "Red", "Blue", etc.)
    public GameObject openPosition; // The position the door should move to when it is open
    public float moveSpeed = 10f; // The speed at which the door should move
    public float tolerance = 0.1f; // The tolerance to consider the door has reached the target position

    private bool playerInDoorArea = false; // To check if the player is in the door's area
    public PlayerInputHandler playerInputHandler; // Reference to the PlayerInputHandler script

    private Coroutine moveCoroutine;
    public GameObject doorSensor; // Reference to the DoorSensor GameObject for This Door

    private void Awake()
    {
        // Find the PlayerInputHandler script if it's not already set
        if (playerInputHandler == null)
        {
            playerInputHandler = GameObject.FindObjectOfType<PlayerInputHandler>();
        }
    }

    public void PlayerEnteredDoorArea()
    {
        playerInDoorArea = true;
    }

    public void PlayerExitedDoorArea()
    {
        playerInDoorArea = false;
    }

    private void Update()
    {
        if (playerInDoorArea && playerInputHandler.InteractionInput)
        {
            // Find the KeyInventory script
            KeyInventory keyInventory = GameObject.FindObjectOfType<KeyInventory>();
            
            if (keyInventory != null && keyInventory.HasKey(doorColor))
            {
                // Open the door
                OpenDoor();
                
                // Reset the interaction input
                playerInputHandler.UseInteractionInput();
            }
        }
    }

    public void OpenDoor()
    {
        if (moveCoroutine != null)
        {
            StopCoroutine(moveCoroutine);
        }
        moveCoroutine = StartCoroutine(MoveToPosition(openPosition.transform.position));

        // Disable the DoorSensor
        if (doorSensor != null)
        {
            doorSensor.SetActive(false);
        }

        // Register this door as opened
        DoorManager doorManager = FindObjectOfType<DoorManager>();
        if (doorManager != null)
        {
            doorManager.RegisterOpenedDoor(this);
        }
    }

    private IEnumerator MoveToPosition(Vector3 targetPosition)
    {
        while (Vector3.Distance(transform.position, targetPosition) > tolerance)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
            yield return null;
        }
        moveCoroutine = null;
    }
}