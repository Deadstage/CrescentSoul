using UnityEngine;
using System.IO;

public class LoadMenu : MonoBehaviour
{
    private string saveFileDirectory = Application.persistentDataPath;

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