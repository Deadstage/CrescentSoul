using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MemoriesManager : MonoBehaviour
{
    public List<Memories> MemoriesList = new List<Memories>();

    private void Start()
    {
        // Create memories (you would do this for all your memories)
        Memories coldMemory1 = new Memories { Type = "Cold", ID = 1, Description = "This is a cold memory of number 1", isCollected = false };
        Memories coldMemory2 = new Memories { Type = "Cold", ID = 2, Description = "This is a cold memory of number 2", isCollected = false };
        Memories coldMemory3 = new Memories { Type = "Cold", ID = 3, Description = "This is a cold memory of number 3", isCollected = false };
        // ... (and so on)

        // Add memories to the list
        MemoriesList.Add(coldMemory1);
        MemoriesList.Add(coldMemory2);
        // ... (and so on)
    }


    public void AddMemory(Memories memory)
    {
        if (!IsMemoryCollected(memory.ID))
        {
            memory.isCollected = true;
            MemoriesList.Add(memory);
            Debug.Log("Memory added: " + memory.Description + ". Total memories: " + MemoriesList.Count);
        }
        else
        {
            Debug.Log("Memory already collected");
        }
    }

    public bool IsMemoryCollected(int id)
    {
        return MemoriesList.Exists(memory => memory.ID == id && memory.isCollected);
    }

    public List<Memories> GetMemoriesOfType(string type)
    {
        List<Memories> memoriesOfType = new List<Memories>();
        foreach (Memories memory in MemoriesList)
        {
            if (memory.Type == type)
            {
                memoriesOfType.Add(memory);
            }
        }
        return memoriesOfType;
    }
}