using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IgniteV2 : Effects
{
    float totalDMG=20;
    float oneDMG;
    public override void Prepare(float _druation, Mods[] _causingMods = null)
    {
        base.Prepare(_druation, _causingMods);
        GameObject VisualEffect = (GameObject)Instantiate(Resources.Load("VisualEffects/OnFire", typeof(GameObject)) as GameObject);
        VisualEffect.transform.parent = transform.GetChild(0);
        VisualEffect.transform.localPosition = Vector3.zero;
        VisualEffect.transform.localRotation = Quaternion.Euler(Vector3.zero);
        Destroy(VisualEffect, _druation);
        Destroy(this, _druation);
        oneDMG = totalDMG * 0.1f;

        StartCoroutine(Burn(_druation));
    }

    IEnumerator Burn(float _druation)
    {
        HealthCallback hp = GetComponent<HealthCallback>();
        for (int i=0; i < 10; i++)
        {
            hp.GetDMG(oneDMG);
            yield return new WaitForSeconds(_druation * 0.1f);
        }
        Destroy(this);
    }
}
