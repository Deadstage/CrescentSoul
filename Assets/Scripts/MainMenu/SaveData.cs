using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    public Vector3 playerPosition;
    public float playerHealth;
    public string saveTime; // to store the save timestamp in a readable format
    public int saveNumber; // to store the save number
    public List<string> collectedMemoryIDs = new List<string>(); // to store the collected memory IDs
}