using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy4Sounds : MonoBehaviour
{
    public AudioSource enemy4Swing;
    public AudioSource enemy4Move;
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
            enemy4Swing.volume = 0f;
            enemy4Move.volume = 0f;
        }
        else
        {
            float volume = Mathf.SmoothStep(0f, 1f, 1f - (distance / maxDistance));
            enemy4Swing.volume = volume * swingVolumeMultiplier;
            enemy4Move.volume = volume * moveVolumeMultiplier;
        }
    }

    public void PlaySwingSound()
    {   
        enemy4Swing.PlayOneShot(swingSound);
    }

    void PlayMoveSound()
    {   
        AudioClip newClip = GetRandomClip(moveClips);
        while (newClip == lastClip && moveClips.Length > 1)
        {
            newClip = GetRandomClip(moveClips);
        }

        enemy4Move.clip = newClip;
        enemy4Move.Play();
        lastClip = newClip;
    }

    private AudioClip GetRandomClip(AudioClip[] clips)
    {
        return clips[Random.Range(0, clips.Length)];
    }
}
