using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MemoryItem : MonoBehaviour
{
    public TMP_Text memoryNumberText; // Reference to the TextMeshPro text component for the memory number
    public TMP_Text memoryDescriptionText; // Reference to the TextMeshPro text component for the memory description
    public Image memoryImage; // Reference to the image component for the memory

    public void Setup(Memories memory)
    {
        memoryNumberText.text = "Memory " + memory.ID;
        memoryDescriptionText.text = memory.Description;

        if (memory.isCollected)
        {
            memoryImage.color = Color.white; // Set to normal color when collected
        }
        else
        {
            memoryImage.color = Color.black; // Set to black when not collected
        }
    }

    public void SetMemory(Memories memory)
    {
        if (memory != null)
        {
            memoryNumberText.text = "Memory " + memory.ID.ToString();
            memoryDescriptionText.text = memory.isCollected ? memory.Description : "You have not yet obtained this memory.";
            memoryImage.color = memory.isCollected ? Color.white : Color.black;
        }
        else
        {
            memoryNumberText.text = "";
            memoryDescriptionText.text = "";
            memoryImage.color = Color.black;
        }
    }
}