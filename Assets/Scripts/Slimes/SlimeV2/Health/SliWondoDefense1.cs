using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliWondoDefense1 : HealthCallback
{

    public override void GetDMG(float _dmg)
    {
        if (Random.Range(0, 100) > 25)
        { base.GetDMG(_dmg); }
    }
}