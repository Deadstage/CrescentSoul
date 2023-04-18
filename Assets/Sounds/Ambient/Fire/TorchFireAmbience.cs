using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorchFireAmbience : MonoBehaviour
{
    public Transform playerTransform;

    public float maxDistance = 10f;
    public float volumeMultiplier = 1f;

    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        float distance = Vector2.Distance(transform.position, playerTransform.position);

        if (distance > maxDistance)
        {
            audioSource.volume = 0f;
        }
        else
        {
            float volume = Mathf.SmoothStep(0f, 1f, 1f - (distance / maxDistance)) * volumeMultiplier;
            audioSource.volume = volume;
        }
    }
}
