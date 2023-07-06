using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy3Sounds : MonoBehaviour
{
    public AudioSource enemy3Kick;
    public AudioSource enemy3Move;
    public AudioSource enemy3Stunned;
    public AudioClip kickSound;
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
            enemy3Kick.volume = 0f;
            enemy3Stunned.volume = 0f;
            enemy3Move.volume = 0f;
        }
        else
        {
            float volume = Mathf.SmoothStep(0f, 1f, 1f - (distance / maxDistance));
            enemy3Kick.volume = volume * kickVolumeMultiplier;
            enemy3Stunned.volume = volume * stunnedVolumeMultiplier;
            enemy3Move.volume = volume * moveVolumeMultiplier;
        }
    }

    public void PlayKickSound()
    {   
        enemy3Kick.PlayOneShot(kickSound);
    }

    void PlayStunnedSound()
    {
        AudioClip newClip = GetRandomClip(stunnedClips);
        while (newClip == lastClip && stunnedClips.Length > 1)
        {
            newClip = GetRandomClip(stunnedClips);
        }

        enemy3Stunned.clip = newClip;
        enemy3Stunned.Play();
        lastClip = newClip;
    }

    void PlayMoveSound()
    {   
        AudioClip newClip = GetRandomClip(moveClips);
        while (newClip == lastClip && moveClips.Length > 1)
        {
            newClip = GetRandomClip(moveClips);
        }

        enemy3Move.clip = newClip;
        enemy3Move.Play();
        lastClip = newClip;
    }

    private AudioClip GetRandomClip(AudioClip[] clips)
    {
        return clips[Random.Range(0, clips.Length)];
    }
}