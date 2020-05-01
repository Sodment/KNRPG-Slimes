using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthCallback : MonoBehaviour
{
    private float maxHealth;
    private float calculateMaxHealth;
    protected float currentHealth;

    protected GameObject healthCanvas;
    protected Image healthImg;

    private void Awake()
    {
        foreach (HealthCallback k in GetComponents<HealthCallback>())
        {
            if (k != this) { Destroy(k); }
        }
        Prepare();
    }

    protected void Prepare()
    {
        maxHealth = GetComponent<SlimeLevelsV2>().Health;
        currentHealth = maxHealth;
        calculateMaxHealth = maxHealth; 
        healthCanvas=GetComponent<SlimeBahaviourV2>().healthCanvas;
        healthImg = GetComponent<SlimeBahaviourV2>().healthBar;
    }

    private void OnDisable()
    {
        calculateMaxHealth = maxHealth;
        currentHealth = calculateMaxHealth;
        healthCanvas.SetActive(false);
        healthImg.fillAmount = 1.0f;
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
            healthImg.fillAmount = 1.0f;
            healthCanvas.SetActive(false);
            GetComponent<SlimeBahaviourV2>().ChangeState(SlimeBahaviourV2.State.Die);
        }
        else if (currentHealth < calculateMaxHealth)
        {
            healthCanvas.SetActive(true);
            healthImg.fillAmount = currentHealth / calculateMaxHealth;
        }
    }
}
