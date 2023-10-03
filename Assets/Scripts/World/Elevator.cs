using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    public Transform topPosition;
    public Transform middlePosition;
    public Transform bottomPosition;

    public ElevatorTopGate topGate; // Reference to the top gate script
    public ElevatorMiddleGate middleGate; // Reference to the middle gate script

    private Transform currentTargetPosition;
    private bool bottomLevelUnlocked = false;
    private bool playerOnElevator = false;

    public float moveSpeed = 10f;

    public GameObject player; // Reference to the player GameObject
    private bool elevatorCalled = false;  // New variable

    private bool isMoving = false;

    private Vector3 lastPosition;  // Add this variable to keep track of the last position

    public void PlayerEnteredElevator()
    {
        playerOnElevator = true;
        player.transform.parent = transform; // Set the elevator as the player's parent
        elevatorCalled = true; // Add this line
    }

    public void PlayerExitedElevator()
    {
        playerOnElevator = false;
        player.transform.parent = null; // Detach the player from the elevator
    }

    public bool HasReachedDestination()
    {
        return Vector3.Distance(transform.position, currentTargetPosition.position) < 0.1f;
    }

    private void Start()
    {
        currentTargetPosition = middlePosition; // Assuming the elevator starts at the middle position
    }

    private void Update()
    {
        if (elevatorCalled)  // Just check for elevatorCalled
        {
            MoveElevator();
        }
    }

    private void MoveElevator()
    {
        transform.position = Vector3.MoveTowards(transform.position, currentTargetPosition.position, moveSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, currentTargetPosition.position) < 0.1f)
        {
            // The elevator has reached the target position
            isMoving = false;
            elevatorCalled = false;  // Reset this back to false
            //Debug.Log("Elevator reached destination. Resetting elevatorCalled to false.");  // Debug log here
            OpenCloseGates();
        }
        else
        {
            isMoving = true;
        }
    }


    public void MoveToPosition(Transform targetPosition)
    {
        if (!isMoving)
        {
            currentTargetPosition = targetPosition;
            isMoving = true;
            elevatorCalled = true;  // Set this to true when button is pressed
        }
    }


    public void MoveUp()
    {
        if (playerOnElevator && !isMoving)
        {
            if (currentTargetPosition == middlePosition)
            {
                currentTargetPosition = topPosition;
            }
            else if (currentTargetPosition == bottomPosition)
            {
                currentTargetPosition = middlePosition;
            }
            // If it's already at the top position, it won't change the target position
            elevatorCalled = true;  // Add this line
        }
    }

    public void MoveDown()
    {
        if (playerOnElevator && !isMoving)
        {
            if (currentTargetPosition == middlePosition)
            {
                if (bottomLevelUnlocked && !isMoving)
                {
                    currentTargetPosition = bottomPosition;
                }
                // If bottom level is not unlocked, it won't move down from the middle position
            }
            else if (currentTargetPosition == topPosition)
            {
                currentTargetPosition = middlePosition;
            }
            // If it's already at the bottom position, it won't change the target position
            elevatorCalled = true;  // Add this line
        }
    }

    public void UnlockBottomLevel()
    {
        bottomLevelUnlocked = true;
    }

    public bool IsPlayerOnElevator()
    {
        return playerOnElevator;
    }

    private void OpenCloseGates()
    {
        StartCoroutine(DelayedOpenCloseGates());
    }

    private IEnumerator DelayedOpenCloseGates()
    {
        yield return new WaitForSeconds(0.7f);

        if (currentTargetPosition == topPosition)
        {
            topGate.OpenGate();
            middleGate.CloseGate();
        }
        else if (currentTargetPosition == middlePosition)
        {
            topGate.CloseGate();
            middleGate.OpenGate();
        }
        else if (currentTargetPosition == bottomPosition)
        {
            topGate.CloseGate();
            middleGate.CloseGate();
        }
    }
}