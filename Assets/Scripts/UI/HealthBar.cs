using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private Core core;
    private GameObject player;

    public Slider slider;
    public HealthBar healthBar;

    private Stats Stats { get => stats ?? core.GetCoreComponent(ref stats); }
    public Stats stats;

    void Awake()
    {
        Debug.Log(stats);

        player = GameObject.Find("Player");
        player.GetComponentInChildren<Core>();
        stats.OnHealthChange += UpdateHealthBar;

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
