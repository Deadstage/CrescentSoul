using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using System;
using System.Globalization;
using System.IO;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private Transform respawnPoint;

    [SerializeField]
    private float respawnTime;

    private float respawnTimeStart;

    private bool respawn;

    private CinemachineVirtualCamera CVC;

    public static GameManager Instance { get; private set; }

    public Player player;

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

    private void Start()
    {
        CVC = GameObject.Find("CM vcam1").GetComponent<CinemachineVirtualCamera>();
        //Time.timeScale = 0.1f;
        //Time.timeScale = 0.5f;
        //Time.timeScale = 0.7f;
    }

    private void Update()
    {
        CheckRespawn();
    }

    public void Respawn()
    {
        respawnTimeStart = Time.time;
        respawn = true;
    }

    private void CheckRespawn()
    {
        if(Time.time >= respawnTimeStart + respawnTime && respawn)
        {
            GameObject playerGameObject = Instantiate(player.gameObject, respawnPoint);
            player = playerGameObject.GetComponent<Player>(); // Get the Player component
            CVC.m_Follow = playerGameObject.transform;
            respawn = false;
        }
    }

    public void SaveGame()
    {
        SaveData data = new SaveData();
        data.playerPosition = player.transform.position;
        data.playerHealth = player.stats.currentHealth; // Save the player's current health

        string json = JsonUtility.ToJson(data);
        string filePath = Path.Combine(Application.persistentDataPath, "savefile_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".json");
        File.WriteAllText(filePath, json);
    }

    public void LoadGame(string fileName)
    {
        StartCoroutine(LoadGameCoroutine(fileName));
    }

    private IEnumerator LoadGameCoroutine(string fileName)
    {
        string filePath = Path.Combine(Application.persistentDataPath, fileName);
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            // Wait for the game scene to be fully loaded
            yield return new WaitUntil(() => SceneManager.GetActiveScene().name == "CS"); // Replace with your actual game scene name

            // Now try to access the player object and its components
            if (player != null && player.stats != null)
            {
                player.transform.position = data.playerPosition;
                player.stats.currentHealth = data.playerHealth; // Load the player's health from the save data
            }
            else
            {
                Debug.LogError("Player object or stats component is null");
            }
        }
        else
        {
            Debug.LogWarning("No save file found at " + filePath);
        }
    }

}
