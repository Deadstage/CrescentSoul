using System;
using System.Globalization;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoadMenu : MonoBehaviour
{
    private string saveFileDirectory;
    private string selectedFileName;

    void Awake()
    {
        saveFileDirectory = Application.persistentDataPath;
    }

    public Transform contentPanel;
    public GameObject listItemPrefab;
    public Button loadButton;
    public Button deleteButton;

    public GameObject deleteConfirmationPanel;
    private string fileToDelete;

    void Start()
    {
        ListSaveFiles();
        loadButton.onClick.AddListener(() => LoadSelectedSaveFile(selectedFileName));
        deleteButton.onClick.AddListener(() => DeleteSelectedSaveFile(selectedFileName));
        loadButton.interactable = false;
        deleteButton.interactable = false;
    }

    void ListSaveFiles()
    {
        foreach (Transform child in contentPanel)
        {
            Destroy(child.gameObject);
        }

        if (Directory.Exists(saveFileDirectory))
        {
            string[] saveFiles = Directory.GetFiles(saveFileDirectory, "savefile_*.json");

            // Sort files numerically based on the save number
            Array.Sort(saveFiles, (x, y) => 
            {
                if (int.TryParse(x.Split('_')[1].Split('.')[0], out int xNum) && int.TryParse(y.Split('_')[1].Split('.')[0], out int yNum))
                {
                    return xNum - yNum;
                }
                else
                {
                    // Handle the case where one or both of the strings couldn't be parsed as integers
                    // (for example, by returning 0 to indicate that they are equal)
                    return 0;
                }
            });

            int saveNumber = 1;
            foreach (string saveFile in saveFiles)
            {
                string fileName = Path.GetFileName(saveFile);
                string saveTime = fileName.Split('_')[1].Replace(".json", "");
                DateTime saveDateTime = DateTime.ParseExact(saveTime, "yyyyMMddHHmmss", CultureInfo.InvariantCulture);
                GameObject newItem = Instantiate(listItemPrefab, contentPanel);
                newItem.GetComponentInChildren<TextMeshProUGUI>().text = "Save " + saveNumber + " | " + saveDateTime.ToString("dd/MM/yyyy - HH:mm:ss");
                newItem.GetComponent<Button>().onClick.AddListener(() => SelectSaveFile(fileName));
                saveNumber++;
            }
        }
        else
        {
            Debug.LogWarning("No save files found");
        }
    }

    public void SelectSaveFile(string fileName)
    {
        selectedFileName = fileName;
        loadButton.interactable = true;
        deleteButton.interactable = true;
    }

    public void LoadSelectedSaveFile(string fileName)
    {
        GameManager.Instance.LoadGame(fileName);
        Debug.Log("LoadSelectedSaveFile called with fileName: " + fileName);
    }

    public void DeleteSelectedSaveFile(string fileName)
    {
        if (!string.IsNullOrEmpty(selectedFileName))
        {
            fileToDelete = selectedFileName;
            deleteConfirmationPanel.SetActive(true);
        }
    }

    public void ConfirmDelete()
    {
        if (!string.IsNullOrEmpty(fileToDelete))
        {
            string filePath = Path.Combine(saveFileDirectory, fileToDelete);
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
                Debug.Log("File deleted: " + fileToDelete);
                ListSaveFiles();
            }
            else
            {
                Debug.LogWarning("File not found: " + fileToDelete);
            }
            fileToDelete = null;
        }
        deleteConfirmationPanel.SetActive(false);
    }

    public void CancelDelete()
    {
        fileToDelete = null;
        deleteConfirmationPanel.SetActive(false);
    }
}