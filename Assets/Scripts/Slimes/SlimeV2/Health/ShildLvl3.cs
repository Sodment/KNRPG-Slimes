using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShildLvl3 : HealthCallback
{
    float currenthp = 60;
    bool used = false;

    private void OnEnable()
    {
        used = false;
        currenthp = 60;
    }
    public override void GetDMG(float _dmg)
    {
        currenthp -= _dmg;
        if (currenthp <= 0)
        {
            base.GetDMG(-currenthp);
            Special();
            currenthp = 0;
        }
    }

    void Special()
    {
        if (used) return;

        foreach (Collider k in Physics.OverlapSphere(transform.position, 2.5f))
        {
            if (k.GetComponent<SlimeBahaviourV2>().PlayerID != GetComponent<SlimeBahaviourV2>().PlayerID)
            {
                k.gameObject.AddComponent<Stun>(); //Będzie lepsze (działające) rozwiązanie
            }
        }
        used = true;
    }
}
