using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireImmunity : HealthCallback {

    public override void GetDMG(float _dmg)
    {
        base.GetDMG(_dmg);
    }
}
