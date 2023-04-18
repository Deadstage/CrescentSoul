using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneSound : MonoBehaviour
{
    public AudioSource bjDeepAudio;
    public AudioSource bjLightAudio;
    public AudioSource SwallowAudio;
    public AudioSource SalivaAudio;
    public AudioSource dickInsertionAudio;
    public AudioSource dickRepeatAudio;
    public AudioSource dickStrokeAudio;
    public AudioSource cumAudio;
    public AudioSource pussyAudio;
    public AudioSource bodyCollideNakedAudio;
    public AudioSource bodyCollideClothedAudio;
    public AudioSource bodyHitHardAudio;
    public AudioSource bodyHitSoftAudio;
    public AudioSource bodySlapHardAudio;
    public AudioSource bodySlapSoftAudio;
    public AudioSource bodyGrabNakedAudio;
    public AudioSource bodyGrabClothedAudio;
    public AudioSource bodyRubNakedAudio;
    public AudioSource bodyRubClothedAudio;
    public AudioSource kissAudio;
    public AudioSource deepKissAudio;

    public AudioClip[] bjDeepClips;
    public AudioClip[] bjLightClips;
    public AudioClip[] swallowClips;
    public AudioClip[] salivaClips;
    public AudioClip[] dickInsertionClips;
    public AudioClip[] dickRepeatClips;
    public AudioClip[] dickStrokeClips;
    public AudioClip[] cumClips;
    public AudioClip[] pussyClips;
    public AudioClip[] bodyCollideNakedClips;
    public AudioClip[] bodyCollideClothedClips;
    public AudioClip[] bodyHitHardClips;
    public AudioClip[] bodyHitSoftClips;
    public AudioClip[] bodySlapHardClips;
    public AudioClip[] bodySlapSoftClips;
    public AudioClip[] bodyGrabNakedClips;
    public AudioClip[] bodyGrabClothedClips;
    public AudioClip bodyRubNakedClip;
    public AudioClip bodyRubClothedClip;
    public AudioClip[] kissClips;
    public AudioClip[] deepKissClips;

    private AudioClip lastClip;

    void PlayBJDeepSounds()
    {
        AudioClip newClip = GetRandomClip(bjDeepClips);
        while (newClip == lastClip && bjDeepClips.Length > 1)
        {
            newClip = GetRandomClip(bjDeepClips);
        }

        bjDeepAudio.clip = newClip;
        bjDeepAudio.Play();
        lastClip = newClip;

    }

    void PlayBJLightSounds()
    {
        AudioClip newClip = GetRandomClip(bjLightClips);
        while (newClip == lastClip && bjLightClips.Length > 1)
        {
            newClip = GetRandomClip(bjLightClips);
        }

        bjLightAudio.clip = newClip;
        bjLightAudio.Play();
        lastClip = newClip;

    }

    void PlaySwallowSounds()
    {   
        AudioClip newClip = GetRandomClip(swallowClips);
        while (newClip == lastClip && swallowClips.Length > 1)
        {
            newClip = GetRandomClip(swallowClips);
        }

        SwallowAudio.clip = newClip;
        SwallowAudio.Play();
        lastClip = newClip;

    }

    void PlaySalivaSounds()
    {   
        AudioClip newClip = GetRandomClip(salivaClips);
        while (newClip == lastClip && salivaClips.Length > 1)
        {
            newClip = GetRandomClip(salivaClips);
        }

        SalivaAudio.clip = newClip;
        SalivaAudio.Play();
        lastClip = newClip;

    }

    void PlayDickInsertionSounds()
    {
        AudioClip newClip = GetRandomClip(dickInsertionClips);
        while (newClip == lastClip && dickInsertionClips.Length > 1)
        {
            newClip = GetRandomClip(dickInsertionClips);
        }

        dickInsertionAudio.clip = newClip;
        dickInsertionAudio.Play();
        lastClip = newClip;

    }

    void PlayDickRepeatSounds()
    {   
        AudioClip newClip = GetRandomClip(dickRepeatClips);
        while (newClip == lastClip && dickRepeatClips.Length > 1)
        {
            newClip = GetRandomClip(dickRepeatClips);
        }

        dickRepeatAudio.clip = newClip;
        dickRepeatAudio.Play();
        lastClip = newClip;

    }

    void PlayDickStrokeSounds()
    {
        AudioClip newClip = GetRandomClip(dickStrokeClips);
        while (newClip == lastClip && dickStrokeClips.Length > 1)
        {
            newClip = GetRandomClip(dickStrokeClips);
        }

        dickStrokeAudio.clip = newClip;
        dickStrokeAudio.Play();
        lastClip = newClip;
    }

    void PlayCumSounds()
    {   
        AudioClip newClip = GetRandomClip(cumClips);
        while (newClip == lastClip && cumClips.Length > 1)
        {
            newClip = GetRandomClip(cumClips);
        }

        cumAudio.clip = newClip;
        cumAudio.Play();
        lastClip = newClip;

    }

    void PlayPussySounds()
    {
        AudioClip newClip = GetRandomClip(pussyClips);
        while (newClip == lastClip && pussyClips.Length > 1)
        {
            newClip = GetRandomClip(pussyClips);
        }

        pussyAudio.clip = newClip;
        pussyAudio.Play();
        lastClip = newClip;

    }

    void PlayBodyCollideNakedSounds()
    {
        AudioClip newClip = GetRandomClip(bodyCollideNakedClips);
        while (newClip == lastClip && bodyCollideNakedClips.Length > 1)
        {
            newClip = GetRandomClip(bodyCollideNakedClips);
        }

        bodyCollideNakedAudio.clip = newClip;
        bodyCollideNakedAudio.Play();
        lastClip = newClip;

    }

    void PlayBodyCollideClothedSounds()
    {   
        AudioClip newClip = GetRandomClip(bodyCollideClothedClips);
        while (newClip == lastClip && bodyCollideClothedClips.Length > 1)
        {
            newClip = GetRandomClip(bodyCollideClothedClips);
        }

        bodyCollideClothedAudio.clip = newClip;
        bodyCollideClothedAudio.Play();
        lastClip = newClip;

    }

    void PlayBodyHitHardSounds()
    {   
        AudioClip newClip = GetRandomClip(bodyHitHardClips);
        while (newClip == lastClip && bodyHitHardClips.Length > 1)
        {
            newClip = GetRandomClip(bodyHitHardClips);
        }

        bodyHitHardAudio.clip = newClip;
        bodyHitHardAudio.Play();
        lastClip = newClip;

    }

    void PlayBodyHitSoftSounds()
    {   
        AudioClip newClip = GetRandomClip(bodyHitSoftClips);
        while (newClip == lastClip && bodyHitSoftClips.Length > 1)
        {
            newClip = GetRandomClip(bodyHitSoftClips);
        }

        bodyHitSoftAudio.clip = newClip;
        bodyHitSoftAudio.Play();
        lastClip = newClip;

    }

    void PlayBodySlapHardSounds()
    {   
        AudioClip newClip = GetRandomClip(bodySlapHardClips);
        while (newClip == lastClip && bodySlapHardClips.Length > 1)
        {
            newClip = GetRandomClip(bodySlapHardClips);
        }

        bodySlapHardAudio.clip = newClip;
        bodySlapHardAudio.Play();
        lastClip = newClip;

    }

    void PlaySlapSoftSounds()
    {   
        AudioClip newClip = GetRandomClip(bodySlapSoftClips);
        while (newClip == lastClip && bodySlapSoftClips.Length > 1)
        {
            newClip = GetRandomClip(bodySlapSoftClips);
        }

        bodySlapSoftAudio.clip = newClip;
        bodySlapSoftAudio.Play();
        lastClip = newClip;

    }

    void PlayBodyGrabNakedSounds()
    {   
        AudioClip newClip = GetRandomClip(bodyGrabNakedClips);
        while (newClip == lastClip && bodyGrabNakedClips.Length > 1)
        {
            newClip = GetRandomClip(bodyGrabNakedClips);
        }

        bodyGrabNakedAudio.clip = newClip;
        bodyGrabNakedAudio.Play();
        lastClip = newClip;

    }

    void PlayBpdyGrabClothedSounds()
    {   
        AudioClip newClip = GetRandomClip(bodyGrabClothedClips);
        while (newClip == lastClip && bodyGrabClothedClips.Length > 1)
        {
            newClip = GetRandomClip(bodyGrabClothedClips);
        }

        bodyGrabClothedAudio.clip = newClip;
        bodyGrabClothedAudio.Play();
        lastClip = newClip;

    }

    void PlayBodyRubNakedSound()
    {  
        bodyRubNakedAudio.PlayOneShot(bodyRubNakedClip);
    }

    void PlayBodyRubClothedSound()
    {   
        bodyRubClothedAudio.PlayOneShot(bodyRubClothedClip);
    }

    void PlayKissSounds()
    {
        AudioClip newClip = GetRandomClip(kissClips);
        while (newClip == lastClip && kissClips.Length > 1)
        {
            newClip = GetRandomClip(kissClips);
        }

        kissAudio.clip = newClip;
        kissAudio.Play();
        lastClip = newClip;

    }

    void PlayDeepKissSounds()
    {   
        AudioClip newClip = GetRandomClip(deepKissClips);
        while (newClip == lastClip && kissClips.Length > 1)
        {
            newClip = GetRandomClip(deepKissClips);
        }

        deepKissAudio.clip = newClip;
        deepKissAudio.Play();
        lastClip = newClip;

    }

    private AudioClip GetRandomClip(AudioClip[] clips)
    {
        return clips[Random.Range(0, clips.Length)];
    }
}