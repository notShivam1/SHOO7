using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class healthbar : MonoBehaviour
{
    Image healthBar;
    public float maxHealth = 100f;
    public float health;
    void Start()
    {
        healthBar = GetComponent<Image>();
        health = maxHealth;
    }
    void Update()
    {
        healthBar.fillAmount = health / maxHealth;
    }

    public void heathManager(float damage)      
    {
        health -= damage;
    }
}
