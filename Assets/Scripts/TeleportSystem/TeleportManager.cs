using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq; // Don't forget to add this to use the LINQ Any method

public class TeleportManager : MonoBehaviour
{
    public static TeleportManager Instance;

    private List<UnlockedRoom> unlockedRooms = new List<UnlockedRoom>();

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
        if (!unlockedRooms.Any(ur => ur.roomId == roomId))
        {
            unlockedRooms.Add(new UnlockedRoom { roomId = roomId, spawnPoint = spawnPoint });
            //Debug.Log("Room unlocked: " + roomId);
        }
        else
        {
            //Debug.Log("Room already unlocked: " + roomId); // Log to check if the room is already unlocked
        }
    }

    public void SetUnlockedRooms(List<UnlockedRoom> unlockedRoomsData)
    {
        unlockedRooms = new List<UnlockedRoom>(unlockedRoomsData);
        Debug.Log("SetUnlockedRooms called with data: " + JsonUtility.ToJson(unlockedRoomsData)); // Log to check the data being set
    }

    public List<UnlockedRoom> GetUnlockedRooms()
    {
        //Debug.Log("GetUnlockedRooms called, returning data: " + JsonUtility.ToJson(unlockedRooms)); // Log to check the data being returned
        return new List<UnlockedRoom>(unlockedRooms);
    }
}