using System;
using System.Globalization;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

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

    public TextMeshProUGUI corruptedSaveFileText;

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
            Array.Sort(saveFiles, (x, y) => 
            {
                string xSubString = x.Split('_')[1].Split('.')[0];
                string ySubString = y.Split('_')[1].Split('.')[0];

                if (int.TryParse(xSubString, out int xNum) && int.TryParse(ySubString, out int yNum))
                {
                    return xNum.CompareTo(yNum);
                }
                else
                {
                    return 0;
                }
            });

            int saveNumber = 1;
            foreach (string saveFile in saveFiles)
            {
                string fileName = Path.GetFileName(saveFile);
                DateTime saveDateTime = File.GetLastWriteTime(saveFile);

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

    public void ShowCorruptedSaveFileMessage()
    {
        corruptedSaveFileText.gameObject.SetActive(true);
        StartCoroutine(FadeOutCorruptedMessage());
    }

    private IEnumerator FadeOutCorruptedMessage()
    {
        // Assuming the text starts fully visible
        for (float t = 0; t <= 1; t += Time.deltaTime / 2) // Adjust 2 to change the fade duration
        {
            corruptedSaveFileText.color = new Color(corruptedSaveFileText.color.r, corruptedSaveFileText.color.g, corruptedSaveFileText.color.b, Mathf.Lerp(1, 0, t));
            yield return null;
        }
        corruptedSaveFileText.gameObject.SetActive(false);
    }

}