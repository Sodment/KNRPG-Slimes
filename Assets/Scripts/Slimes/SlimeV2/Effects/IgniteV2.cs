using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IgniteV2 : Effects
{
    public override void Prepare(float _druation, Mods[] _causingMods = null)
    {
        base.Prepare(_druation, _causingMods);
        GameObject VisualEffect = (GameObject)Instantiate(Resources.Load("VisualEffects/OnFire", typeof(GameObject)) as GameObject);
        VisualEffect.transform.parent = transform.GetChild(0);
        VisualEffect.transform.localPosition = Vector3.zero;
        VisualEffect.transform.localRotation = Quaternion.Euler(-90,0,0);
        Destroy(VisualEffect, _druation);
        Destroy(this, _druation);

        StartCoroutine(Burn(_druation));
    }

    IEnumerator Burn(float _druation)
    {
        HealthCallback hp = GetComponent<HealthCallback>();
        for (int i=0; i < _druation*10; i++)
        {
            hp.GetDMG(1.0f);
            yield return new WaitForSeconds(_druation * 0.1f);
        }
        Destroy(this);
    }

    private void OnDisable()
    {
        Destroy(this);
    }
}
