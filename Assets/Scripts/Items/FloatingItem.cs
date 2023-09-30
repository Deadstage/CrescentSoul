using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingItem : MonoBehaviour
{
    public float floatSpeed = 0.5f; // Speed of floating, adjust this value as needed
    public float floatHeight = 0.3f; // Height of floating, adjust this value as needed

    private Vector3 initialPosition;

    private void Start()
    {
        initialPosition = transform.position;
    }

    private void Update()
    {
        // Make the object float up and down
        transform.position = initialPosition + new Vector3(0, Mathf.Sin(Time.time * floatSpeed) * floatHeight, 0);
    }
}