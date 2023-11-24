using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy7Sounds : MonoBehaviour
{
    public AudioSource enemy7Swing;
    public AudioSource enemy7Move;
    public AudioSource enemy7Stunned;
    public AudioClip swingSound;
    public AudioClip[] moveClips;
    public AudioClip[] stunnedClips;
    private AudioClip lastClip;


    public Transform playerTransform;

    public float maxDistance = 10f;
    public float kickVolumeMultiplier = 1f;
    public float moveVolumeMultiplier = 1f;
    public float stunnedVolumeMultiplier = 1f;

    void Update()
    {
        float distance = Vector2.Distance(transform.position, playerTransform.position);

        if (distance > maxDistance)
        {
            enemy7Swing.volume = 0f;
            enemy7Stunned.volume = 0f;
            enemy7Move.volume = 0f;
        }
        else
        {
            float volume = Mathf.SmoothStep(0f, 1f, 1f - (distance / maxDistance));
            enemy7Swing.volume = volume * kickVolumeMultiplier;
            enemy7Stunned.volume = volume * stunnedVolumeMultiplier;
            enemy7Move.volume = volume * moveVolumeMultiplier;
        }
    }

    public void PlaySwingSound()
    {   
        enemy7Swing.PlayOneShot(swingSound);
    }

    void PlayStunnedSound()
    {
        AudioClip newClip = GetRandomClip(stunnedClips);
        while (newClip == lastClip && stunnedClips.Length > 1)
        {
            newClip = GetRandomClip(stunnedClips);
        }

        enemy7Stunned.clip = newClip;
        enemy7Stunned.Play();
        lastClip = newClip;
    }

    void PlayMoveSound()
    {   
        AudioClip newClip = GetRandomClip(moveClips);
        while (newClip == lastClip && moveClips.Length > 1)
        {
            newClip = GetRandomClip(moveClips);
        }

        enemy7Move.clip = newClip;
        enemy7Move.Play();
        lastClip = newClip;
    }

    private AudioClip GetRandomClip(AudioClip[] clips)
    {
        return clips[Random.Range(0, clips.Length)];
    }
}