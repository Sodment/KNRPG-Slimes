using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Atletyka : HealthCallback {

    public override void GetDMG(float _dmg)
    {
        if (Random.Range(0, 100) > 15)
        { base.GetDMG(_dmg); }
    }
}
