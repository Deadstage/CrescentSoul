using UnityEngine;
using System;
using System.Globalization;
using System.IO;
using UnityEngine.UI;

public class LoadMenu : MonoBehaviour
{
    private string saveFileDirectory;

    void Awake()
    {
        saveFileDirectory = Application.persistentDataPath;
    }

    public Transform contentPanel;
    public GameObject listItemPrefab;

    void Start()
    {
        ListSaveFiles();
    }

    void ListSaveFiles()
    {
        if (Directory.Exists(saveFileDirectory))
        {
            string[] saveFiles = Directory.GetFiles(saveFileDirectory, "savefile_*.json");

            foreach (string saveFile in saveFiles)
            {
                string fileName = Path.GetFileName(saveFile);
                string saveTime = fileName.Replace("savefile_", "").Replace(".json", "");
                DateTime saveDateTime = DateTime.ParseExact(saveTime, "yyyyMMddHHmmss", CultureInfo.InvariantCulture);

                GameObject newItem = Instantiate(listItemPrefab, contentPanel);
                newItem.GetComponentInChildren<Text>().text = "Save File: " + fileName + " | Save Time: " + saveDateTime.ToString();

                // Add a button click listener to load the save file when the item is clicked
                newItem.GetComponent<Button>().onClick.AddListener(() => GameManager.Instance.LoadGame(fileName)); // Use GameManager.Instance to call the LoadGame method
            }
        }
        else
        {
            Debug.LogWarning("No save files found");
        }
    }

    public void LoadSelectedSaveFile(string fileName)
    {
        GameManager.Instance.LoadGame(fileName);
    }
}