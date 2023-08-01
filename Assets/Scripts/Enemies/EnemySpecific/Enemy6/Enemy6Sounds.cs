using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy6Sounds : MonoBehaviour
{
    public AudioSource enemy6Swing;
    public AudioSource enemy6Move;
    public AudioClip swingSound;
    public AudioClip[] moveClips;
    private AudioClip lastClip;


    public Transform playerTransform;

    public float maxDistance = 10f;
    public float swingVolumeMultiplier = 1f;
    public float moveVolumeMultiplier = 1f;

    void Update()
    {
        float distance = Vector2.Distance(transform.position, playerTransform.position);

        if (distance > maxDistance)
        {
            enemy6Swing.volume = 0f;
            enemy6Move.volume = 0f;
        }
        else
        {
            float volume = Mathf.SmoothStep(0f, 1f, 1f - (distance / maxDistance));
            enemy6Swing.volume = volume * swingVolumeMultiplier;
            enemy6Move.volume = volume * moveVolumeMultiplier;
        }
    }

    public void PlaySwingSound()
    {   
        enemy6Swing.PlayOneShot(swingSound);
    }

    void PlayMoveSound()
    {   
        AudioClip newClip = GetRandomClip(moveClips);
        while (newClip == lastClip && moveClips.Length > 1)
        {
            newClip = GetRandomClip(moveClips);
        }

        enemy6Move.clip = newClip;
        enemy6Move.Play();
        lastClip = newClip;
    }

    private AudioClip GetRandomClip(AudioClip[] clips)
    {
        return clips[Random.Range(0, clips.Length)];
    }
}
