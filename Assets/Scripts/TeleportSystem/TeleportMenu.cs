using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TeleportMenu : MonoBehaviour
{
    public GameObject roomButtonPrefab; // The prefab for the room buttons
    public Transform roomListContainer; // The container for the room buttons

    private TeleportManager teleportManager;
    public PlayerInputHandler playerInputHandler;

    private string selectedRoomId;
    private Vector2 selectedSpawnPoint;

    [SerializeField]
    private Button teleportButton;

    private void Start()
    {
        teleportManager = FindObjectOfType<TeleportManager>();
        if (teleportManager == null)
        {
            Debug.LogError("TeleportManager not found");
        }
        else
        {
            //Debug.Log("TeleportManager found");
        }

        playerInputHandler = FindObjectOfType<PlayerInputHandler>();
        if (playerInputHandler == null)
        {
            Debug.LogError("PlayerInputHandler not found");
        }
        else
        {
            //Debug.Log("PlayerInputHandler found");
        }

        // Set up the teleport button listener here instead of in ShowMenu
        if (teleportButton != null)
        {
            teleportButton.onClick.RemoveAllListeners();
            teleportButton.onClick.AddListener(TeleportToSelectedRoom);
        }
        else
        {
            Debug.LogError("TeleportButton is not assigned in the inspector");
        }
    }

    public void ShowMenu()
    {
        teleportManager = FindObjectOfType<TeleportManager>();
        if (teleportManager == null)
        {
            Debug.LogError("TeleportManager is null");
            return;
        }

        // Get the list of unlocked rooms
        var unlockedRooms = teleportManager.GetUnlockedRooms();
        if (unlockedRooms == null)
        {
            Debug.LogError("Unlocked rooms list is null");
            return;
        }

        //Debug.Log("ShowMenu called, unlockedRooms data: " + JsonUtility.ToJson(unlockedRooms)); // Log to check the data when showing the menu

        // Clear any existing buttons
        foreach (Transform child in roomListContainer)
        {
            Destroy(child.gameObject);
        }

        // Create a button for each unlocked room
        foreach (var room in unlockedRooms)
        {
            if (room.roomId == null || room.spawnPoint == null)
            {
                Debug.LogError("Room ID or spawn point is null");
                continue;
            }

            GameObject buttonObj = Instantiate(roomButtonPrefab, roomListContainer);
            buttonObj.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = room.roomId; // Assuming the room ID is a suitable display name
            buttonObj.GetComponent<Button>().onClick.AddListener(() => SelectRoom(room.roomId, room.spawnPoint));
        }

        // Show the menu
        gameObject.SetActive(true);
        playerInputHandler.isTeleportMenuOpen = true;
    }

    public void SelectRoom(string roomId, Vector2 spawnPoint)
    {
        // Store the selected room's information
        selectedRoomId = roomId;
        selectedSpawnPoint = spawnPoint;
    }

    public void TeleportToSelectedRoom()
    {
        if (!string.IsNullOrEmpty(selectedRoomId))
        {
            // Teleport the player to the selected room
            GameObject player = GameObject.FindGameObjectWithTag("Player");

            if (player != null)
            {
                player.transform.position = selectedSpawnPoint;
            }
            else
            {
                Debug.LogError("Player object not found");
            }

            HideMenu();
        }
        else
        {
            Debug.LogError("No room selected");
        }
    }

    public void HideMenu()
    {
        gameObject.SetActive(false);
        playerInputHandler.isTeleportMenuOpen = false;
    }
}