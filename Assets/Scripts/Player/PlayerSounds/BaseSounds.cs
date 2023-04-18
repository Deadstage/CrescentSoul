using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseSounds : MonoBehaviour
{
    public AudioSource player;
    public AudioClip[] scytheSounds;

    public void PlayScytheSound(int index)
    {
        if (index >= 0 && index < scytheSounds.Length)
        {
            player.clip = scytheSounds[index];
            player.Play();
        }
    }
}
