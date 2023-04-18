using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2Sounds : MonoBehaviour
{
    public AudioSource enemy2Swipe;
    public AudioSource enemy2Move;
    public AudioSource enemy2Dodge;
    public AudioClip swipeSound;
    public AudioClip[] moveClips;
    public AudioClip dodgeSound;
    private AudioClip lastClip;


    public Transform playerTransform;

    public float maxDistance = 10f;
    public float swipeVolumeMultiplier = 1f;
    public float moveVolumeMultiplier = 1f;
    public float dodgeVolumeMultiplier = 1f;

    void Update()
    {
        float distance = Vector2.Distance(transform.position, playerTransform.position);

        if (distance > maxDistance)
        {
            enemy2Swipe.volume = 0f;
            enemy2Dodge.volume = 0f;
            enemy2Move.volume = 0f;
        }
        else
        {
            float volume = Mathf.SmoothStep(0f, 1f, 1f - (distance / maxDistance));
            enemy2Swipe.volume = volume * swipeVolumeMultiplier;
            enemy2Dodge.volume = volume * dodgeVolumeMultiplier;
            enemy2Move.volume = volume * moveVolumeMultiplier;
        }
    }

    public void PlaySwipeSound()
    {   
        enemy2Swipe.PlayOneShot(swipeSound);
    }

    void PlayDodgeSound()
    {
        enemy2Dodge.PlayOneShot(dodgeSound);
    }

    void PlayMoveSound()
    {   
        AudioClip newClip = GetRandomClip(moveClips);
        while (newClip == lastClip && moveClips.Length > 1)
        {
            newClip = GetRandomClip(moveClips);
        }

        enemy2Move.clip = newClip;
        enemy2Move.Play();
        lastClip = newClip;
    }

    private AudioClip GetRandomClip(AudioClip[] clips)
    {
        return clips[Random.Range(0, clips.Length)];
    }
}