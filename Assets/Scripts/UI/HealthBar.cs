using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public HealthBar healthBar;
    private Core core;
    private Player player;
    public Slider slider;
    private Stats Stats { get => stats ?? core.GetCoreComponent(ref stats); }
    private Stats stats;

    private void Start()
    {
        GameObject.Find("Player");
    }

    private void UpdateHealthBar(float value)
    {
        slider.maxValue = stats.maxHealth;
        slider.value = stats.currentHealth;
    }

  private void OnEnable()
    {
    stats.OnHealthChange += UpdateHealthBar;
    }

    private void OnDisable()
    {
    stats.OnHealthChange -= UpdateHealthBar;
    }
}
