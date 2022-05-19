using System;
using UnityEngine;

public class Stats : CoreComponent
{
    [SerializeField] public float maxHealth;
    public float currentHealth;

    public event Action HealthZero;

    public event Action<float> OnHealthChange;

    protected override void Awake()
    {
        base.Awake();

        currentHealth = maxHealth;
    }

    public void DecreaseHealth(float amount)
    {
        currentHealth -= amount;
        OnHealthChange?.Invoke(currentHealth);

        if(currentHealth <= 0)
        {
            currentHealth = 0;
            HealthZero?.Invoke();
            Debug.Log("Health is zero!");
        }
    }

    public void IncreaseHealth(float amount)
    {
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        OnHealthChange?.Invoke(currentHealth);
    }
}
