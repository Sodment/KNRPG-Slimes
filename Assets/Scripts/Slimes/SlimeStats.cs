using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlimeStats : MonoBehaviour
{
    public Stats health;
    public float maxHealth;
    public float currentHealth { get; private set; }
    public Stats damage;
    public Stats atackSpeed;
    public Stats defense;
    public Image HealthBar;

    void Awake()
    {
        maxHealth = health.GetValue();
        currentHealth = maxHealth;
    }

    public void TakeDamage(float rawdamage)
    {
        float rec_damage = damage.GetValue() - (float)(defense.GetValue() * 0.65);
        currentHealth -= rec_damage;
        HealthBar.fillAmount = currentHealth / health.GetValue();

        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}
