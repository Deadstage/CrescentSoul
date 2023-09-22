using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorBottomButton : MonoBehaviour
{
    public Elevator elevator; // Reference to the Elevator script
    public Transform targetPosition; // The position the elevator should move to when this button is pressed

    public PlayerInputHandler playerInputHandler;
    private bool playerInButtonArea = false;

    public Animator buttonAnimator; // Reference to the Animator component

    private void Awake()
    {
        playerInputHandler = FindObjectOfType<PlayerInputHandler>();

        // If you didn't set the Animator in the Unity Editor, you can get it like this
        if (buttonAnimator == null)
        {
            buttonAnimator = GetComponent<Animator>();
        }
    }

    private void Update()
    {
        if (playerInButtonArea && playerInputHandler.InteractionInput)
        {
            elevator.MoveToPosition(targetPosition);
            playerInputHandler.UseInteractionInput(); // Reset the interaction input

            // Trigger the button animation
            buttonAnimator.SetTrigger("ButtonPressed"); // Assuming "ButtonPressed" is the name of your trigger in the Animator Controller
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInButtonArea = true;
            Debug.Log("Player entered the middle button area");  // Debug log added
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInButtonArea = false;
            Debug.Log("Player exited the middle button area");  // Debug log added
        }
    }
}