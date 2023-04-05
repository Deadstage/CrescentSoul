using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaBar : MonoBehaviour
{
    private Core core;
    private GameObject player;

    public Slider slider;
    public StaminaBar staminaBar;

    private Stats Stats { get => stats ?? core.GetCoreComponent(ref stats); }
    public Stats stats;

    void Awake()
    {
        //Debug.Log(stats);

        player = GameObject.Find("Player");
        player.GetComponentInChildren<Core>();
        stats.OnStaminaChange += UpdateStaminaBar;

    }

    private void UpdateStaminaBar(float value)
    {
        slider.maxValue = stats.maxStamina;
        slider.value = stats.currentStamina;
    }

    private void OnEnable()
    {
        stats.OnStaminaChange += UpdateStaminaBar;
    }

    private void OnDisable()
    {
        stats.OnStaminaChange -= UpdateStaminaBar;
    }
}
