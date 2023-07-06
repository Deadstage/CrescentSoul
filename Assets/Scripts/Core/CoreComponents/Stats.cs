using System;
using UnityEngine;
using System.Collections;

public class Stats : CoreComponent
{
    [SerializeField] public float maxHealth;
    [SerializeField] public float maxStamina;
    public float currentHealth;
    public float currentStamina;

    public event Action HealthZero;
    public event Action StaminaZero;

    public event Action<float> OnHealthChange;
    public event Action<float> OnStaminaChange;

    public StaminaBar staminaBar;

    private WaitForSeconds regenTick = new WaitForSeconds(0.01f);
    private Coroutine regen;

    public AudioSource audioSource;
    public AudioClip[] hitSounds;

    protected override void Awake()
    {
        base.Awake();

        currentHealth = maxHealth;
        currentStamina = maxStamina;
    }

    public void DecreaseHealth(float amount)
    {
        currentHealth -= amount;
        OnHealthChange?.Invoke(currentHealth);

        // play a random hit sound
        if (hitSounds.Length > 0)
        {
            int index = UnityEngine.Random.Range(0, hitSounds.Length);
            AudioSource.PlayClipAtPoint(hitSounds[index], transform.position);
        }

        if(currentHealth <= 0)
        {
            currentHealth = 0;
            HealthZero?.Invoke();
            //Debug.Log("Health is zero!");
        }
        
    }

    public void InternalDecreaseStamina(float amount)
    {
        currentStamina -= amount;
        OnStaminaChange?.Invoke(currentStamina);

        if (currentHealth - amount >= 0)
        {

            if (regen != null)
                StopCoroutine(regen);

            regen = StartCoroutine(RegenStamina());
        }
    }

    public void ExternalDecreaseStamina(float amount)
    {
        currentStamina -= amount;
        OnStaminaChange?.Invoke(currentStamina);

        if (currentStamina <= 0)
        {
            currentStamina = 0;
            StaminaZero?.Invoke();
            //Debug.Log("Stamina is zero!");

        }

        if (currentHealth - amount >= 0)
        {

            if (regen != null)
                StopCoroutine(regen);

            regen = StartCoroutine(RegenStamina());
        }
    }

    public void IncreaseHealth(float amount)
    {
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        OnHealthChange?.Invoke(currentHealth);
    }

    public void IncreaseStamina(float amount)
    {
        currentStamina = Mathf.Clamp(currentStamina + amount, 0, maxStamina);
        OnStaminaChange?.Invoke(currentStamina);
    }

    private IEnumerator RegenStamina()
    {
        yield return new WaitForSeconds(2);

        while(currentStamina < maxStamina)
        {
            currentStamina += maxStamina / 150;
            staminaBar.slider.value = currentStamina;
            yield return regenTick;
        }
        regen = null;
    }

    public void ResetStamina()
    {
        currentStamina = 0;
        OnStaminaChange?.Invoke(currentStamina);

        StaminaZero?.Invoke();
        //Debug.Log("Stamina is zero!");

        if (regen != null)
            StopCoroutine(regen);

        regen = StartCoroutine(RegenStamina());
    }

    public void PlayAudioClip(AudioClip clip)
    {
        if(transform.parent != null && transform.parent.parent != null && transform.parent.parent.name == "Enemy1")
        {
            AudioSource.PlayClipAtPoint(clip, transform.parent.parent.position);
        }

        if(transform.parent != null && transform.parent.parent != null && transform.parent.parent.name == "Enemy2")
        {
            AudioSource.PlayClipAtPoint(clip, transform.parent.parent.position);
        }

        if(transform.parent != null && transform.parent.parent != null && transform.parent.parent.name == "Player")
        {
            AudioSource.PlayClipAtPoint(clip, transform.parent.parent.position);
        }
    }

    void DamagedSound()
    {
        if (audioSource != null && hitSounds != null && hitSounds.Length > 0)
        {
            int index = UnityEngine.Random.Range(0, hitSounds.Length);
            audioSource.PlayOneShot(hitSounds[index]);
            PlayAudioClip(hitSounds[index]);
        }
    }

}

