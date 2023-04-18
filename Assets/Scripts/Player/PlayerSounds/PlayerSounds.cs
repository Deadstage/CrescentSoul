using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSounds : MonoBehaviour
{   
    public AudioSource playerAudio1;
    public AudioSource playerAudio2;
    public AudioSource playerAudio3;
    public AudioSource playerAudio4;
    public AudioSource playerAudio5;
    public AudioSource playerAudio6;
    public AudioSource playerAudio7;

    public AudioClip[] moveClips;
    public AudioClip[] armorMoveClips;
    public AudioClip[] crawlClips;
    public AudioClip jumpSound;
    public AudioClip backdashSound;

    private AudioClip lastClip;

    private bool canPlayBackdashSound = true;
    public float backdashSoundCooldown = 2f;
    private float backdashSoundCooldownTimer;

    private void Update()
    {
        if (!canPlayBackdashSound)
        {
            backdashSoundCooldownTimer += Time.deltaTime;
            if (backdashSoundCooldownTimer >= backdashSoundCooldown)
            {
                canPlayBackdashSound = true;
                backdashSoundCooldownTimer = 0f;
            }
        }
    }

    void PlayMoveSound()
    {   
        AudioClip newClip = GetRandomClip(moveClips);
        while (newClip == lastClip && moveClips.Length > 1)
        {
            newClip = GetRandomClip(moveClips);
        }

        playerAudio1.clip = newClip;
        playerAudio1.Play();
        lastClip = newClip;
    }

    void PlayArmorMoveSounds() 
    {
        AudioClip newClip = GetRandomClip(armorMoveClips);
        while (newClip == lastClip && armorMoveClips.Length > 1)
        {
            newClip = GetRandomClip(armorMoveClips);
        }

        playerAudio2.clip = newClip;
        playerAudio2.Play();
        lastClip = newClip;
    }

    void PlayJumpStepSound()
    {
        AudioClip newClip = GetRandomClip(moveClips);
        while (newClip == lastClip && moveClips.Length > 1)
        {
            newClip = GetRandomClip(moveClips);
        }

        playerAudio3.clip = newClip;
        playerAudio3.Play();
        lastClip = newClip;
    }

    void PlayJumpArmorSound()
    {
        AudioClip newClip = GetRandomClip(armorMoveClips);
        while (newClip == lastClip && armorMoveClips.Length > 1)
        {
            newClip = GetRandomClip(armorMoveClips);
        }

        playerAudio4.clip = newClip;
        playerAudio4.Play();
        lastClip = newClip;

    }

    void PlayJumpSound()
    {
        playerAudio5.PlayOneShot(jumpSound);
    }

    void PlayCrawlSound()
    {
        AudioClip newClip = GetRandomClip(crawlClips);
        while (newClip == lastClip && crawlClips.Length > 1)
        {
            newClip = GetRandomClip(crawlClips);
        }

        playerAudio6.clip = newClip;
        playerAudio6.Play();
        lastClip = newClip;
    }

    void PlayBackdashSound()
    {
        if (canPlayBackdashSound)
        {
            playerAudio7.PlayOneShot(backdashSound);
            canPlayBackdashSound = false;
        }
    }


    private AudioClip GetRandomClip(AudioClip[] clips)
    {
        return clips[Random.Range(0, clips.Length)];
    }
}