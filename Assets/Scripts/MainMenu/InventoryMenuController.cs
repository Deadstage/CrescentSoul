using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class InventoryMenuController : MonoBehaviour
{
    public GameObject keyItemsScrollView; // Reference to the Key Items Scroll View GameObject
    public GameObject consumablesScrollView; // Reference to the Consumables Scroll View GameObject
    public GameObject notesScrollView; // Reference to the Notes Scroll View GameObject

    public GameObject keyItemPrefab; // Assign key item prefab
    public GameObject consumablePrefab; // Assign consumable prefab
    public GameObject notePrefab; // Assign note prefab

    public KeyInventory keyInventory; // Reference to the Key Inventory

    private string currentInventoryType; // The current inventory type being displayed

    void Awake()
    {
        // Manual assignment for keyItemsScrollView
        if (keyItemsScrollView == null)
        {
            keyItemsScrollView = GameObject.Find("Key View");
        }

        // Manual assignment for consumablesScrollView
        if (consumablesScrollView == null)
        {
            consumablesScrollView = GameObject.Find("Consumables View");
        }

        // Manual assignment for notesScrollView
        if (notesScrollView == null)
        {
            notesScrollView = GameObject.Find("Notes View");
        }
    }

    private void PopulateScrollView(List<KeyItem> items, GameObject scrollViewContent, string currentInventoryType)
    {
        // Clear any existing children of the scrollViewContent
        foreach (Transform child in scrollViewContent.transform)
        {
            Destroy(child.gameObject);
        }

        // Create and populate inventory items for each item in the list
        foreach (KeyItem keyItem in keyInventory.GetAllKeys())
        {
            GameObject itemPrefab = GetPrefabBasedOnInventoryType(currentInventoryType);
            GameObject inventoryItem = Instantiate(itemPrefab, scrollViewContent.transform);

            InventoryItemController itemController = inventoryItem.GetComponent<InventoryItemController>();
            if (itemController != null)
            {
                itemController.InitializeItem(keyItem.sprite, keyItem.color, keyItem.description);
            }
            else
            {
                Debug.LogError("InventoryItemController not found on prefab.");
            }
        }
    }

    public void OpenKeyItems()
    {
        OpenInventoryScrollView(keyItemsScrollView, "KeyItems");
    }

    public void OpenConsumables()
    {
        OpenInventoryScrollView(consumablesScrollView, "Consumables");
    }

    public void OpenNotes()
    {
        OpenInventoryScrollView(notesScrollView, "Notes");
    }

    private void OpenInventoryScrollView(GameObject scrollView, string inventoryType)
    {
        // Find the KeyInventory script if it's not already set
        if (keyInventory == null)
        {
            keyInventory = GameObject.FindObjectOfType<KeyInventory>();
            if (keyInventory != null)
            {
                Debug.Log("KeyInventory found and set.");
            }
            else
            {
                Debug.LogError("KeyInventory not found.");
            }
        }

        if (scrollView != null && keyInventory != null)
        {
            // Deactivate all inventory scroll views first
            keyItemsScrollView.SetActive(false);
            consumablesScrollView.SetActive(false);
            notesScrollView.SetActive(false);

            // Activate the selected inventory scroll view
            scrollView.SetActive(true);
            //Debug.Log($"{inventoryType} ScrollView active state: " + scrollView.activeSelf);

            currentInventoryType = inventoryType;

            // Get the items of the selected type and populate the scroll view
            List<KeyItem> items = new List<KeyItem>();
            if (inventoryType == "KeyItems")
            {
                items = keyInventory.GetAllKeys();
                //Debug.Log("Keys in inventory: " + string.Join(", ", items.Select(k => k.color)));  // This line will print the keys
            }
            // Add similar logic for Consumables and Notes here

            Transform contentTransform = scrollView.transform.Find("Viewport/Content");
            if (contentTransform != null)
            {
                GameObject scrollViewContent = contentTransform.gameObject;
                //Debug.Log("ScrollViewContent is not null.");
                PopulateScrollView(items, scrollViewContent, inventoryType);
            }
            else
            {
                //Debug.LogError("ScrollViewContent is null.");
                return;
            }
        }
        else
        {
            Debug.LogError($"{inventoryType} Inventory Scroll View or Key Inventory not assigned in InventoryMenuController");
        }
    }

    private GameObject GetPrefabBasedOnInventoryType(string inventoryType)
    {
        switch (inventoryType)
        {
            case "KeyItems":
                return keyItemPrefab;
            case "Consumables":
                return consumablePrefab;
            case "Notes":
                return notePrefab;
            default:
                return null; // or return a default prefab
        }
    }

    public void DeactivateAllViews()
    {
        keyItemsScrollView.SetActive(false);
        consumablesScrollView.SetActive(false);
        notesScrollView.SetActive(false);
    }

    // Call this method when the menu is opened to update the displayed items
    public void OnMenuOpened(string defaultInventoryType = "KeyItems")
    {
        switch (defaultInventoryType)
        {
            case "KeyItems":
                OpenKeyItems();
                break;
            case "Consumables":
                OpenConsumables();
                break;
            case "Notes":
                OpenNotes();
                break;
            default:
                Debug.LogError("Invalid default inventory type.");
                break;
        }
        UpdateDisplayedInventory();
    }

    public void UpdateDisplayedInventory()
    {
        if (!string.IsNullOrEmpty(currentInventoryType))
        {
            GameObject currentScrollView = null;
            switch (currentInventoryType)
            {
                case "KeyItems":
                    currentScrollView = keyItemsScrollView;
                    break;
                case "Consumables":
                    currentScrollView = consumablesScrollView;
                    break;
                case "Notes":
                    currentScrollView = notesScrollView;
                    break;
            }

            // Null check for currentScrollView
            if (currentScrollView != null)
            {
                //Debug.Log("currentScrollView is not null.");
                Transform contentTransform = currentScrollView.transform.Find("Viewport/Content");
                
                // Null check for contentTransform
                if (contentTransform != null)
                {
                    GameObject scrollViewContent = contentTransform.gameObject;
                    //Debug.Log("ScrollViewContent is not null.");
                    
                    List<KeyItem> items = new List<KeyItem>();
                    if (currentInventoryType == "KeyItems")
                    {
                        items = keyInventory.GetAllKeys();
                    }
                    // Add similar logic for Consumables and Notes here
                    PopulateScrollView(items, scrollViewContent, currentInventoryType);
                }
                else
                {
                    Debug.LogError("contentTransform is null.");
                }
            }
            else
            {
                Debug.LogError("currentScrollView is null.");
            }
        }
        else
        {
            Debug.LogError("currentInventoryType is empty or null.");
        }
    }
}