using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlimeHealth : MonoBehaviour
{
    float maxHP;
    float currentHP;
    GameObject healthCanvas;
    Image healthBar;

    float oneDivByMaxHP;

    public void Start()
    {
        foreach(SlimeHealth k in GetComponents<SlimeHealth>())
        {
            if (k != this)
                Destroy(k);
        }
        healthCanvas = GetComponent<SlimeBehaviour>().healthCanvas;
        healthBar = GetComponent<SlimeBehaviour>().healthBar;
    }

    public virtual void Prepare(float _maxHP)
    {
        maxHP = _maxHP;
        currentHP = maxHP;
        oneDivByMaxHP = 1.0f / maxHP;
        healthBar.fillAmount = 1.0f;
        healthCanvas.SetActive(false);
    }

    public virtual void GetDMG(float _dmg)
    {
        currentHP -= _dmg;
        if (currentHP != maxHP)
        {
            healthCanvas.SetActive(true);
            healthBar.fillAmount = currentHP * oneDivByMaxHP;
        }
        DeathTest();
    }

    protected virtual void DeathTest()
    {
        if (currentHP <= 0) 
        { GetComponent<SlimeBehaviour>().ChangeState(SlimeBehaviour.State.Die); }
    }
}
