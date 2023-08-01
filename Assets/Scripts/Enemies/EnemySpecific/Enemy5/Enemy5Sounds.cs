using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy5Sounds : MonoBehaviour
{
    public AudioSource enemy5Attack;
    public AudioSource enemy5Move;
    public AudioClip attackSound;
    public AudioClip[] moveClips;
    private AudioClip lastClip;


    public Transform playerTransform;

    public float maxDistance = 10f;
    public float attackVolumeMultiplier = 1f;
    public float moveVolumeMultiplier = 1f;

    void Update()
    {
        float distance = Vector2.Distance(transform.position, playerTransform.position);

        if (distance > maxDistance)
        {
            enemy5Attack.volume = 0f;
            enemy5Move.volume = 0f;
        }
        else
        {
            float volume = Mathf.SmoothStep(0f, 1f, 1f - (distance / maxDistance));
            enemy5Attack.volume = volume * attackVolumeMultiplier;
            enemy5Move.volume = volume * moveVolumeMultiplier;
        }
    }

    public void PlayAttackSound()
    {   
        enemy5Attack.PlayOneShot(attackSound);
    }

    void PlayMoveSound()
    {   
        AudioClip newClip = GetRandomClip(moveClips);
        while (newClip == lastClip && moveClips.Length > 1)
        {
            newClip = GetRandomClip(moveClips);
        }

        enemy5Move.clip = newClip;
        enemy5Move.Play();
        lastClip = newClip;
    }

    private AudioClip GetRandomClip(AudioClip[] clips)
    {
        return clips[Random.Range(0, clips.Length)];
    }
}
