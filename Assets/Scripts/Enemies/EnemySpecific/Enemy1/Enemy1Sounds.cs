using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1Sounds : MonoBehaviour
{
    public AudioSource enemy1Swipe;
    public AudioSource enemy1Move;
    public AudioSource enemy1Drag;
    public AudioClip swipeSound;
    public AudioClip[] moveClips;
    public AudioClip[] dragClips;
    private AudioClip lastClip;


    public Transform playerTransform;

    public float maxDistance = 10f;
    public float swipeVolumeMultiplier = 1f;
    public float moveVolumeMultiplier = 1f;
    public float dragVolumeMultiplier = 1f;

    void Update()
    {
        float distance = Vector2.Distance(transform.position, playerTransform.position);

        if (distance > maxDistance)
        {
            enemy1Swipe.volume = 0f;
            enemy1Drag.volume = 0f;
            enemy1Move.volume = 0f;
        }
        else
        {
            float volume = Mathf.SmoothStep(0f, 1f, 1f - (distance / maxDistance));
            enemy1Swipe.volume = volume * swipeVolumeMultiplier;
            enemy1Drag.volume = volume * dragVolumeMultiplier;
            enemy1Move.volume = volume * moveVolumeMultiplier;
        }
    }

    public void PlaySwipeSound()
    {   
        enemy1Swipe.PlayOneShot(swipeSound);
    }

    void PlayDragSound()
    {
        AudioClip newClip = GetRandomClip(dragClips);
        while (newClip == lastClip && dragClips.Length > 1)
        {
            newClip = GetRandomClip(dragClips);
        }

        enemy1Drag.clip = newClip;
        enemy1Drag.Play();
        lastClip = newClip;
    }

    void PlayMoveSound()
    {   
        AudioClip newClip = GetRandomClip(moveClips);
        while (newClip == lastClip && moveClips.Length > 1)
        {
            newClip = GetRandomClip(moveClips);
        }

        enemy1Move.clip = newClip;
        enemy1Move.Play();
        lastClip = newClip;
    }

    private AudioClip GetRandomClip(AudioClip[] clips)
    {
        return clips[Random.Range(0, clips.Length)];
    }
}
