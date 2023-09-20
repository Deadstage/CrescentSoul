using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorTopButton : MonoBehaviour
{
    public Elevator elevator; // Reference to the Elevator script
    public Transform targetPosition; // The position the elevator should move to when this button is pressed

    private bool playerNearButton = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerNearButton = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerNearButton = false;
        }
    }

    public bool IsPlayerNearButton()
    {
        return playerNearButton;
    }

    public void PressButton()
    {
        elevator.MoveToPosition(targetPosition);
    }
}