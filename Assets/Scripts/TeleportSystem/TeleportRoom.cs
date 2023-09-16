using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportRoom : MonoBehaviour
{
    public string roomId; // Unique identifier for this room
    public string roomName; // A friendly name for this room to display in the UI
    public GameObject teleportSpawnPoint; // The GameObject where the player will spawn when they teleport to this room
    public TeleportManager teleportManager; // Reference to the TeleportManager

    private void Start()
    {
        if (teleportSpawnPoint == null)
        {
            Debug.LogError("TeleportSpawnPoint is not assigned on " + gameObject.name);
        }

        if (teleportManager == null)
        {
            Debug.LogError("TeleportManager is not assigned on " + gameObject.name);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Notify the TeleportManager that this room has been unlocked
            teleportManager.UnlockRoom(roomId, teleportSpawnPoint.transform.position);
        }
    }
}