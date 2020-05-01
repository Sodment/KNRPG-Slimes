using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShildLvl2 : HealthCallback
{
    float currenthp = 40;
    bool used = false;

    private void OnEnable()
    {
        used = false;
        currenthp = 40;
        Prepare();
    }
    public override void GetDMG(float _dmg)
    {
        currenthp -= _dmg;
        if(currenthp<=0)
        {
            healthImg.color = new Color(0.1033843f, 0.8679245f, 0.07778566f, 0.8196079f);
            healthImg.fillAmount = 1.0f;
            base.GetDMG(-currenthp);
            Special();
            currenthp = 0;
        }
        else
        {
            healthImg.color = new Color(0.5f, 0.5f, 0.5f, 1.0f);
            healthCanvas.SetActive(true);
            healthImg.fillAmount = currenthp * 0.025f; 
        }
    }

    void Special()
    {
        if (used) return;

        foreach(Collider k in Physics.OverlapSphere(transform.position, 2.5f))
        {
            if (k.GetComponent<SlimeBahaviourV2>() == null) continue;
            if(k.GetComponent<SlimeBahaviourV2>().PlayerID != GetComponent<SlimeBahaviourV2>().PlayerID)
            {
                k.gameObject.AddComponent<StunV2>();
                k.gameObject.GetComponent<StunV2>().Prepare(5.0f);
            }
        }
        used = true;
    }
}
