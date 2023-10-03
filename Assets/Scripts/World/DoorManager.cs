using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorManager : MonoBehaviour
{
    private List<string> openedDoors = new List<string>(); // List to store the IDs of opened doors

    // Call this method when a door is opened
    public void RegisterOpenedDoor(Door door)
    {
        if (door != null && !string.IsNullOrEmpty(door.doorColor))
        {
            if (!openedDoors.Contains(door.doorColor))
            {
                openedDoors.Add(door.doorColor);
            }
        }
    }

    // Get the list of opened doors
    public List<string> GetOpenedDoors()
    {
        return new List<string>(openedDoors);
    }

    // Set the list of opened doors (used when loading a game)
    public void SetOpenedDoors(List<string> loadedOpenedDoors)
    {
        openedDoors = new List<string>(loadedOpenedDoors);
    }

    // Call this method to update the state of all doors based on the loaded data
    public void UpdateDoorStates()
    {
        Door[] allDoors = FindObjectsOfType<Door>();
        foreach (Door door in allDoors)
        {
            if (openedDoors.Contains(door.doorColor))
            {
                door.OpenDoor();
            }
        }
    }
}