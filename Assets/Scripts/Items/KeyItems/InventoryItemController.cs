using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryItemController : MonoBehaviour
{
    public Image itemImage; // Reference to the Image component
    public TextMeshProUGUI itemNameText; // Reference to the TMP Text component for the item name
    public TextMeshProUGUI itemDescriptionText; // Reference to the TMP Text component for the item description

    public void InitializeItem(Sprite sprite, string name, string description)
    {
        itemImage.sprite = sprite;
        itemNameText.text = name;
        itemDescriptionText.text = description;
    }
}