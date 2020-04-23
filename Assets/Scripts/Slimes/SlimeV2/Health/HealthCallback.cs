using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthCallback : MonoBehaviour
{
    private float maxHealth;
    private float calculateMaxHealth;
    protected float currentHealth;



    private void OnEnable()
    {
        maxHealth = GetComponent<SlimeLevelsV2>().Health;
        currentHealth = maxHealth;
        calculateMaxHealth = maxHealth; 
        GetComponent<SlimeBahaviourV2>().healthCanvas.SetActive(true);
        GetComponent<SlimeBahaviourV2>().healthBar.fillAmount = currentHealth / calculateMaxHealth;
    }

    private void OnDisable()
    {
        calculateMaxHealth = maxHealth;
        currentHealth = calculateMaxHealth;
        GetComponent<SlimeBahaviourV2>().healthCanvas.SetActive(false);
        GetComponent<SlimeBahaviourV2>().healthBar.fillAmount = 1.0f;
    }

    public void UpdateMods()
    {
        foreach(HealthMods mod in gameObject.GetComponents<HealthMods>())
        {
            mod.Calculate(ref currentHealth, ref calculateMaxHealth);
        }
        DeathTest();
    }

    public virtual void GetDMG(float _dmg)
    {
        currentHealth -= _dmg;
        DeathTest();
    }

    void DeathTest()
    {
        if (currentHealth <= 0)
        {
            currentHealth = maxHealth;
            calculateMaxHealth = maxHealth;
            GetComponent<SlimeBahaviourV2>().healthBar.fillAmount = 1.0f;
            GetComponent<SlimeBahaviourV2>().healthCanvas.SetActive(false);
            GetComponent<SlimeBahaviourV2>().ChangeState(SlimeBahaviourV2.State.Die);
        }
        else if (currentHealth < calculateMaxHealth)
        {
            GetComponent<SlimeBahaviourV2>().healthCanvas.SetActive(true);
            GetComponent<SlimeBahaviourV2>().healthBar.fillAmount = currentHealth / calculateMaxHealth;
        }
    }
}
