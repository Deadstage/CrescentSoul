using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorSensor : MonoBehaviour
{
    public Elevator elevator; // Reference to the Elevator script

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Player entered the elevator sensor area");
            elevator.PlayerEnteredElevator();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Player exited the elevator sensor area");
            elevator.PlayerExitedElevator();
        }
    }
}