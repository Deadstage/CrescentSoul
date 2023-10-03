using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class KeyItem
{
    public string color;
    public Sprite sprite;
    public string description;

    public KeyItem(string color, Sprite sprite, string description)
    {
        this.color = color;
        this.sprite = sprite;
        this.description = description;
    }
}