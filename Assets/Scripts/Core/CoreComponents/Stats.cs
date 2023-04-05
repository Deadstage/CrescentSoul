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
}

