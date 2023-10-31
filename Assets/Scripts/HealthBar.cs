using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    Damageable playerDamageable;
    public TMP_Text textHealth;
    public Slider healthSlider;

    private void Awake()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player == null) Debug.Log("Apply a Player TAG to the current player.");
        playerDamageable = player.GetComponent<Damageable>();
    }

    // Start is called before the first frame update
    void Start()
    {
        healthSlider.value = CalculateSliderPercentage(playerDamageable.Health, playerDamageable.MaxHealth);
        textHealth.text = "Health " + playerDamageable.Health + " / " + playerDamageable.MaxHealth;
    }

    private void OnEnable()
    {
        playerDamageable.HealthChanged.AddListener(OnHealthChange);
    }

    private void OnDisable()
    {
        playerDamageable.HealthChanged.RemoveListener(OnHealthChange);
    }

    // Update is called once per frame
    private void OnHealthChange(float currentHealth, float maxHealth)
    {
        Debug.Log("LOG : " + healthSlider.value);
        healthSlider.value = CalculateSliderPercentage(currentHealth, maxHealth);
        textHealth.text = "Health " + currentHealth + " / " + maxHealth;
    }

    private float CalculateSliderPercentage(float currentHealth, float maxHealth)
    {
        return currentHealth / maxHealth;
    }
}
