using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using System;
using System.Globalization;
using System.IO;
using UnityEngine.SceneManagement;
using System.Security.Cryptography;
using System.Text;

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
            SceneManager.sceneLoaded += OnSceneLoaded;  // Subscribe to the sceneLoaded event
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        CVC = GameObject.Find("CM vcam1").GetComponent<CinemachineVirtualCamera>();

        // Find and assign the Player object reference when the game scene is loaded
        StartCoroutine(AssignPlayerReference());

        //Time.timeScale = 0.1f;
        //Time.timeScale = 0.5f;
        //Time.timeScale = 0.7f;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "CS")  // Replace with your actual game scene name
        {
            StartCoroutine(AssignPlayerReference());
        }
    }

    private IEnumerator AssignPlayerReference()
    {
        while (player == null)
        {
            player = FindObjectOfType<Player>();
            yield return new WaitForSeconds(0.1f);
        }
        //Debug.Log("Player reference assigned");
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
        if (player == null)
        {
            Debug.LogError("Player object reference not found");
            return;
        }

        SaveData data = new SaveData();
        data.playerPosition = player.transform.position;
        data.playerHealth = player.stats.currentHealth;

        // Get the list of collected memory IDs and save it to SaveData
        MemoriesManager memoriesManager = FindObjectOfType<MemoriesManager>();
        if (memoriesManager != null)
        {
            data.collectedMemoryIDs = memoriesManager.GetCollectedMemoryIDs();
        }
        else
        {
            Debug.LogError("MemoriesManager not found");
        }

            // Get the list of unlocked rooms and save it to SaveData
        TeleportManager teleportManager = FindObjectOfType<TeleportManager>();
        if (teleportManager != null)
        {
            data.unlockedRooms = teleportManager.GetUnlockedRooms();
            //Debug.Log("SaveGame called, unlockedRooms data: " + JsonUtility.ToJson(data.unlockedRooms)); // Log to check the data when saving
        }
        else
        {
            Debug.LogError("TeleportManager not found");
        }

        // Get the CurrencyManager and save the current coin count
        CurrencyManager currencyManager = FindObjectOfType<CurrencyManager>();
        if (currencyManager != null)
        {
            data.currentCoins = currencyManager.currentCoins;
        }
        else
        {
            Debug.LogError("CurrencyManager not found");
        }

        int highestSaveNumber = GetHighestSaveNumber();
        data.saveNumber = highestSaveNumber + 1;

        data.saveTime = DateTime.Now.ToString("dd/MM/yyyy - HH:mm:ss");

        string json = JsonUtility.ToJson(data);
        string hash = GenerateSHA256String(json);
        string filePath = Path.Combine(Application.persistentDataPath, "savefile_" + data.saveNumber + ".json");
        File.WriteAllText(filePath, json);
        File.WriteAllText(filePath + ".hash", hash);
    }

    private int GetHighestSaveNumber()
    {
        int highestSaveNumber = 0;
        string[] saveFiles = Directory.GetFiles(Application.persistentDataPath, "savefile_*.json");
        foreach (string saveFile in saveFiles)
        {
            string fileName = Path.GetFileName(saveFile);
            string saveNumberStr = fileName.Replace("savefile_", "").Replace(".json", "");
            int saveNumber;
            if (int.TryParse(saveNumberStr, out saveNumber))
            {
                highestSaveNumber = Math.Max(highestSaveNumber, saveNumber);
            }
        }
        return highestSaveNumber;
    }

    public void LoadGame(string fileName)
    {
        Debug.Log("GameManager LoadGame called with fileName: " + fileName);

        string filePath = Path.Combine(Application.persistentDataPath, fileName);
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            string hash = File.ReadAllText(filePath + ".hash");
            string newHash = GenerateSHA256String(json);

            if (newHash == hash)
            {
                SaveData data = JsonUtility.FromJson<SaveData>(json);
                Debug.Log("LoadGame called, SaveData: " + json); // Log to check the data when loading

                Debug.Log("Save data read successfully: " + json);

                SceneManager.LoadScene("CS", LoadSceneMode.Single);
                SceneManager.sceneLoaded += (scene, mode) => 
                {
                    player = FindObjectOfType<Player>();

                    if (player != null && player.stats != null)
                    {
                        player.transform.position = data.playerPosition;
                        player.stats.currentHealth = data.playerHealth; 
                        Debug.Log("Player data loaded successfully");

                        // Load the list of collected memory IDs and update MemoriesManager
                        MemoriesManager memoriesManager = FindObjectOfType<MemoriesManager>();
                        if (memoriesManager != null)
                        {
                            memoriesManager.SetCollectedMemoryIDs(data.collectedMemoryIDs);
                        }
                        else
                        {
                            Debug.LogError("MemoriesManager not found");
                        }

                        // Load the list of unlocked rooms and update TeleportManager
                        TeleportManager teleportManager = FindObjectOfType<TeleportManager>();
                        if (teleportManager != null)
                        {
                            teleportManager.SetUnlockedRooms(data.unlockedRooms);
                        }
                        else
                        {
                            Debug.LogError("TeleportManager not found");
                        }

                            // Load the player's coin count and update CurrencyManager
                            CurrencyManager currencyManager = FindObjectOfType<CurrencyManager>();
                            if (currencyManager != null)
                            {
                                currencyManager.currentCoins = data.currentCoins;
                                currencyManager.UpdateCoinUI();  // Update the UI to reflect the loaded coin count
                            }
                            else
                            {
                                Debug.LogError("CurrencyManager not found");
                            }
                    }
                    else
                    {
                        Debug.LogError("Player object or stats component is null");
                    }
                };
            }
            else
            {
                Debug.LogError("Data has been tampered with");
                LoadMenu loadMenu = FindObjectOfType<LoadMenu>();
                if (loadMenu != null)
                {
                    loadMenu.ShowCorruptedSaveFileMessage();
                }
            }
        }
        else
        {
            Debug.LogWarning("No save file found at " + filePath);
        }
    }

    private IEnumerator LoadGameCoroutine(string fileName)
    {
        string filePath = Path.Combine(Application.persistentDataPath, fileName);
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            yield return new WaitUntil(() => SceneManager.GetActiveScene().name == "CS"); 

            if (player != null && player.stats != null)
            {
                player.transform.position = data.playerPosition;
                player.stats.currentHealth = data.playerHealth; 
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

    

    private string GenerateSHA256String(string inputString)
    {
        using (SHA256 sha256 = SHA256.Create())
        {
            byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(inputString));
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < bytes.Length; i++)
            {
                builder.Append(bytes[i].ToString("x2"));
            }
            return builder.ToString();
        }
    }

    
}