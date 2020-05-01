using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StunV2 : Effects {

    public void Prepare(float _time)
    {
        if (GetComponent<SlimeBahaviourV2>().GetState() == SlimeBahaviourV2.State.Fight)
        {
            GetComponent<SlimeBahaviourV2>().ChangeState(SlimeBahaviourV2.State.Stun);
        }
        Destroy(this, _time);
        GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
    }

    private void OnDestroy()
    {
        if (GetComponent<SlimeBahaviourV2>().GetState() == SlimeBahaviourV2.State.Stun)
        {
            GetComponent<SlimeBahaviourV2>().ChangeState(SlimeBahaviourV2.State.Fight);
        }
    }
}
