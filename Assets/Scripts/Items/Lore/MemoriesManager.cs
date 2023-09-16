using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MemoriesManager : MonoBehaviour
{
    public List<Memories> MemoriesList = new List<Memories>();

    private void Awake()
    {
        InitializeMemoriesList();
    }

    private void InitializeMemoriesList()
    {

        MemoriesList.Clear();

        // Create memories (do this for all memories)
        Memories coldMemory1 = new Memories { Type = "Cold", ID = 1, Description = "When we first met... You asked me if I knew the way...", isCollected = false };
        Memories coldMemory2 = new Memories { Type = "Cold", ID = 2, Description = "In truth, I didn't... But I wanted you to hope...", isCollected = false };
        Memories coldMemory3 = new Memories { Type = "Cold", ID = 3, Description = "We thought it was a never ending adventure...", isCollected = false };
        Memories coldMemory4 = new Memories { Type = "Cold", ID = 4, Description = "I miss the verdant forests... The grassy plains... The blue skies...", isCollected = false };

        Memories warmMemory1 = new Memories { Type = "Warm", ID = 1, Description = "Do you... Know the way...?", isCollected = false };
        Memories warmMemory2 = new Memories { Type = "Warm", ID = 2, Description = "The stars... Can you take me there...?", isCollected = false };

        Memories stoicMemory1 = new Memories { Type = "Stoic", ID = 1, Description = "A beautiful piece of work... Are you sure...?", isCollected = false };
        Memories stoicMemory2 = new Memories { Type = "Stoic", ID = 2, Description = "You must be mistaken... It's not befitting...", isCollected = false };

        Memories darkMemory1 = new Memories { Type = "Dark", ID = 1, Description = "Why do you look at me like that...? Don't I disgust you...?", isCollected = false };
        Memories darkMemory2 = new Memories { Type = "Dark", ID = 2, Description = "Born beneath the darkest star... My birthrite...", isCollected = false };

        Memories fadingMemory1 = new Memories { Type = "Fading", ID = 1, Description = "You're lost, aren't you...?", isCollected = false };
        Memories fadingMemory2 = new Memories { Type = "Fading", ID = 2, Description = "Me...? Just an observer...", isCollected = false };
        // ... (and so on)

        // Add memories to the list
        MemoriesList.Add(coldMemory1);
        MemoriesList.Add(coldMemory2);
        MemoriesList.Add(coldMemory3);
        MemoriesList.Add(coldMemory4);

        MemoriesList.Add(warmMemory1);
        MemoriesList.Add(warmMemory2);

        MemoriesList.Add(stoicMemory1);
        MemoriesList.Add(stoicMemory2);

        MemoriesList.Add(darkMemory1);
        MemoriesList.Add(darkMemory2);

        MemoriesList.Add(fadingMemory1);
        MemoriesList.Add(fadingMemory2);
        // ... (and so on)
    }


    public void AddMemory(Memories memory)
    {
        Memories existingMemory = MemoriesList.Find(m => m.ID == memory.ID && m.Type == memory.Type);
        if (existingMemory != null)
        {
            existingMemory.isCollected = true;
            Debug.Log("Memory added: " + memory.Description + ". Total memories: " + MemoriesList.Count(m => m.isCollected));
        }
        else
        {
            Debug.LogError("Memory with ID " + memory.ID + " and Type " + memory.Type + " not found in the list");
        }
    }

    public bool IsMemoryCollected(int id, string type)
    {
        return MemoriesList.Exists(memory => memory.ID == id && memory.Type == type && memory.isCollected);
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

    public List<string> GetCollectedMemoryIDs()
    {
        List<string> collectedMemoryIDs = new List<string>();
        foreach (Memories memory in MemoriesList)
        {
            if (memory.isCollected)
            {
                collectedMemoryIDs.Add(memory.Type + "_" + memory.ID);
            }
        }
        return collectedMemoryIDs;
    }

    public void SetCollectedMemoryIDs(List<string> collectedMemoryIDs)
    {
        foreach (string memoryID in collectedMemoryIDs)
        {
            string[] parts = memoryID.Split('_');
            if (parts.Length == 2)
            {
                string type = parts[0];
                int id = int.Parse(parts[1]);
                Memories memory = MemoriesList.Find(m => m.Type == type && m.ID == id);
                if (memory != null)
                {
                    memory.isCollected = true;
                }
                else
                {
                    Debug.LogError("Memory with ID " + id + " and Type " + type + " not found in the list");
                }
            }
        }
    }
}