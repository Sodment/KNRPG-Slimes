using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stun : Effect { 

    public void Prepare(float _druation)
    {
        druation = _druation;
        GetComponent<AbominacjaRobiacaZaPathFind>().velocity = 0;
        GetComponent<Rigidbody>().angularDrag = 100.0f;
        Destroy(this, druation);
    }

    private void OnDestroy()
    {
        GetComponent<Rigidbody>().angularDrag = 0.0f;
        GetComponent<AbominacjaRobiacaZaPathFind>().velocity = GetComponent<AbominacjaRobiacaZaPathFind>().defaultVelocity;
    }
}
