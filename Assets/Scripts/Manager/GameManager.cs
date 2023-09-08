using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

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

    public void LoadGame()
    {
        if (System.IO.File.Exists("savefile_{timestamp}.json"))
        {
            string json = System.IO.File.ReadAllText("savefile_{timestamp}.json");
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            player.transform.position = data.playerPosition; 
            // if transform.position expects a Vector3, use new Vector3(data.playerPosition.x, data.playerPosition.y, player.transform.position.z)

        }
        else
        {
            Debug.LogWarning("No save file found");
        }
    }
}
