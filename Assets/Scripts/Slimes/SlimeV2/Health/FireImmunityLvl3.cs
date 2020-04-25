using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireImmunityLvl3 : HealthCallback
{

    public override void GetDMG(float _dmg)
    {
        base.GetDMG(_dmg*0.75f);
    }
}
