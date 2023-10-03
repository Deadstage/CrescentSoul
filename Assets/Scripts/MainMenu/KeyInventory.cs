using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyInventory : MonoBehaviour
{
    private List<KeyItem> keys = new List<KeyItem>();  // List to store the keys
    private Dictionary<string, KeyItem> keyDictionary = new Dictionary<string, KeyItem>(); // Dictionary to look up keys by color

    public List<KeyItem> GetAllKeys()
    {
        return new List<KeyItem>(keys);
    }

    public void AddKey(string keyColor, Sprite keySprite, string keyDescription)
    {
        KeyItem newItem = new KeyItem(keyColor, keySprite, keyDescription);
        keys.Add(newItem);
        keyDictionary[keyColor] = newItem;  // Add the key to the dictionary as well
    }

    public bool HasKey(string keyColor)
    {
        return keyDictionary.ContainsKey(keyColor);  // Use the dictionary for faster look-up
    }

    public KeyItem GetKeyByColor(string keyColor)
    {
        if (keyDictionary.TryGetValue(keyColor, out KeyItem keyItem))
        {
            return keyItem;
        }
        return null;
    }
}