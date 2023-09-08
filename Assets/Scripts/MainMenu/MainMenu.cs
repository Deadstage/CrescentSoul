using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.Globalization;
using System.IO;

public class MainMenu : MonoBehaviour
{
    private string saveFileDirectory;

    void Awake()
    {
        saveFileDirectory = Application.persistentDataPath;
    }
    
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }

    public void OpenLoadMenu()
    {
        // This method should be called when you open the load menu
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

                // Now you have the filename and the save time. You can display these in your LoadMenu UI.
                // For example, using Debug.Log here, but you'd use UI elements in your actual game.
                Debug.Log("Save File: " + fileName + " | Save Time: " + saveDateTime.ToString());
            }
        }
        else
        {
            Debug.LogWarning("No save files found");
        }
    }

}
