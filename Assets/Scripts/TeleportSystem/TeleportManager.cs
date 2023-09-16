using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportManager : MonoBehaviour
{
    public static TeleportManager Instance;

    private Dictionary<string, Vector2> unlockedRooms = new Dictionary<string, Vector2>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void UnlockRoom(string roomId, Vector2 spawnPoint)
    {
        if (!unlockedRooms.ContainsKey(roomId))
        {
            unlockedRooms.Add(roomId, spawnPoint);
            Debug.Log("Room unlocked: " + roomId);
        }
    }

    public Dictionary<string, Vector2> GetUnlockedRooms()
    {
        return new Dictionary<string, Vector2>(unlockedRooms);
    }
}