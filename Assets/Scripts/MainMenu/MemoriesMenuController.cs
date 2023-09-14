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

    public GameObject memoryItemPrefab; // Assign your memory item prefab here

    public MemoriesManager memoriesManager; // Reference to the Memories Manager

    private void PopulateScrollView(List<Memories> memories, GameObject scrollViewContent)
    {
        // First, clear any existing children of the scrollViewContent
        foreach (Transform child in scrollViewContent.transform)
        {
            Destroy(child.gameObject);
        }

        // Now, create and populate memory items for each memory in the list
        foreach (Memories memory in memories)
        {
            GameObject memoryItem = Instantiate(memoryItemPrefab, scrollViewContent.transform);
            memoryItem.GetComponent<MemoryItem>().Setup(memory);
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

            // Get the memories of the selected type and populate the scroll view
            List<Memories> memories = memoriesManager.GetMemoriesOfType(memoryType);
            PopulateScrollView(memories, scrollView);
        }
        else
        {
            Debug.LogError($"{memoryType} Memories Scroll View or Memories Manager not assigned in MemoriesMenuController");
        }
    }

    // This method would populate the scroll view with the given list of memories
    // private void PopulateScrollView(List<Memories> memories, GameObject scrollView)
    // {
    //     // Your code here to populate the scroll view
    // }
}