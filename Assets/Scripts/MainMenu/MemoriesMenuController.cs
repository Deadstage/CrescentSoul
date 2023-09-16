using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MemoriesMenuController : MonoBehaviour
{
    public GameObject coldMemoriesScrollView; // Reference to the Cold Memories Scroll View GameObject
    public GameObject warmMemoriesScrollView; // Reference to the Warm Memories Scroll View GameObject
    public GameObject stoicMemoriesScrollView; // Reference to the Stoic Memories Scroll View GameObject
    public GameObject darkMemoriesScrollView; // Reference to the Dark Memories Scroll View GameObject
    public GameObject fadingMemoriesScrollView; // Reference to the Fading Memories Scroll View GameObject

    public GameObject coldMemoryPrefab; // Assign memory item prefab
    public GameObject warmMemoryPrefab;
    public GameObject stoicMemoryPrefab;
    public GameObject darkMemoryPrefab;
    public GameObject fadingMemoryPrefab;

    public MemoriesManager memoriesManager; // Reference to the Memories Manager

    private string currentMemoryType; // The current memory type being displayed

    private void PopulateScrollView(List<Memories> memories, GameObject scrollViewContent, string currentMemoryType)
    {
        // Clear any existing children of the scrollViewContent
        foreach (Transform child in scrollViewContent.transform)
        {
            Destroy(child.gameObject);
        }

        // Create and populate memory items for each memory in the list
        foreach (Memories memory in memories)
        {
            GameObject memoryPrefab = GetPrefabBasedOnMemoryType(currentMemoryType);
            GameObject memoryItem = Instantiate(memoryPrefab, scrollViewContent.transform);
            memoryItem.GetComponent<MemoryItem>().SetMemory(memory);
        }
    }

    public void OpenColdMemories()
    {
        OpenMemoriesScrollView(coldMemoriesScrollView, "Cold");
    }

    public void OpenWarmMemories()
    {
        OpenMemoriesScrollView(warmMemoriesScrollView, "Warm");
    }

    public void OpenStoicMemories()
    {
        OpenMemoriesScrollView(stoicMemoriesScrollView, "Stoic");
    }

    public void OpenDarkMemories()
    {
        OpenMemoriesScrollView(darkMemoriesScrollView, "Dark");
    }

    public void OpenFadingMemories()
    {
        OpenMemoriesScrollView(fadingMemoriesScrollView, "Fading");
    }

    private void OpenMemoriesScrollView(GameObject scrollView, string memoryType)
    {
        if (scrollView != null && memoriesManager != null)
        {
            // Deactivate all memory scroll views
            coldMemoriesScrollView.SetActive(false);
            warmMemoriesScrollView.SetActive(false);
            stoicMemoriesScrollView.SetActive(false);
            darkMemoriesScrollView.SetActive(false);
            fadingMemoriesScrollView.SetActive(false);

            // Activate the selected memory scroll view
            scrollView.SetActive(true);

            currentMemoryType = memoryType;

            //Debug.Log(memoryType + " Memories Scroll View activated");
            //Debug.Log(memoryType + " Memories Scroll View active state: " + scrollView.activeSelf);

            // Get the memories of the selected type and populate the scroll view
            List<Memories> memories = memoriesManager.GetMemoriesOfType(memoryType);
            
            // Get the "Content" game object of the scroll view
            GameObject scrollViewContent = scrollView.transform.Find("Viewport/Content").gameObject;
            
            PopulateScrollView(memories, scrollViewContent, memoryType);
        }
        else
        {
            Debug.LogError($"{memoryType} Memories Scroll View or Memories Manager not assigned in MemoriesMenuController");
        }
    }

    public void UpdateDisplayedMemories()
    {
        if (!string.IsNullOrEmpty(currentMemoryType))
        {
            GameObject currentScrollView = null;
            switch (currentMemoryType)
            {
                case "Cold":
                    currentScrollView = coldMemoriesScrollView;
                    break;
                case "Warm":
                    currentScrollView = warmMemoriesScrollView;
                    break;
                case "Stoic":
                    currentScrollView = stoicMemoriesScrollView;
                    break;
                case "Dark":
                    currentScrollView = darkMemoriesScrollView;
                    break;
                case "Fading":
                    currentScrollView = fadingMemoriesScrollView;
                    break;
            }

            if (currentScrollView != null)
            {
                GameObject scrollViewContent = currentScrollView.transform.Find("Viewport/Content").gameObject;
                List<Memories> memories = memoriesManager.GetMemoriesOfType(currentMemoryType);
                PopulateScrollView(memories, scrollViewContent, currentMemoryType); // Changed memoryType to currentMemoryType
            }
        }
    }

    private GameObject GetPrefabBasedOnMemoryType(string memoryType)
    {
        switch (memoryType)
        {
            case "Cold":
                return coldMemoryPrefab;
            case "Warm":
                return warmMemoryPrefab;
            case "Dark":
                return darkMemoryPrefab;
            case "Stoic":
                return stoicMemoryPrefab;
            case "Fading":
                return fadingMemoryPrefab;
            default:
                return null; // or return a default prefab
        }
    }

    public void DeactivateAllViews()
    {
        coldMemoriesScrollView.SetActive(false);
        warmMemoriesScrollView.SetActive(false);
        stoicMemoriesScrollView.SetActive(false);
        darkMemoriesScrollView.SetActive(false);
        fadingMemoriesScrollView.SetActive(false);
    }

    // Call this method when the menu is opened to update the displayed memories
    public void OnMenuOpened()
    {
        UpdateDisplayedMemories();
    }
}