using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using System;
using System.Globalization;
using System.IO;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private Transform respawnPoint;
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private float respawnTime;

    private float respawnTimeStart;

    private bool respawn;

    private CinemachineVirtualCamera CVC;

    public static GameManager Instance { get; private set; }

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
            var playerTemp = Instantiate(player, respawnPoint);
            CVC.m_Follow = playerTemp.transform;
            respawn = false;
        }
    }

    public void SaveGame()
    {
        SaveData data = new SaveData();
        data.playerPosition = player.transform.position; 
        // if transform.position gives a Vector3, use new Vector2(player.transform.position.x, player.transform.position.y)

        string json = JsonUtility.ToJson(data);
        System.IO.File.WriteAllText("savefile_{timestamp}.json", json);
    }

    public void LoadGame(string fileName)
    {
        string filePath = Path.Combine(Application.persistentDataPath, fileName);
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            SaveData data = JsonUtility.FromJson<SaveData>(json);
            player.transform.position = data.playerPosition;
        }
        else
        {
            Debug.LogWarning("No save file found at " + filePath);
        }
    }
}
