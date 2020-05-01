using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shild : HealthCallback
{
    float currenthp = 20;

    private void OnEnable()
    {
        currenthp = 20;
    }
    public override void GetDMG(float _dmg)
    {
        currenthp -= _dmg;
        if (currenthp <= 0)
        {
            base.GetDMG(-currenthp);
            currenthp = 0;
        }
    }
}
