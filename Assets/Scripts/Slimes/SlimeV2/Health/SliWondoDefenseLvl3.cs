using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliWondoDefenseLvl3 : HealthCallback
{
    public override void GetDMG(float _dmg)
    {
        if (Random.Range(0, 100) > 40)
        { base.GetDMG(_dmg); }
    }
}