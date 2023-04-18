using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutdoorWindAmbience : MonoBehaviour
{
    public AudioSource audioSource;

    private bool playerInside = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            audioSource.Play();
            playerInside = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            audioSource.Stop();
            playerInside = false;
        }
    }

    private void Update()
    {
        if (playerInside && !audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }
}
