using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ignite : Effect
{
    float dmg;
    float delay;
    GameObject VisualEffect;

    public void Prepare(float _dmg, float _druation)
    {
        foreach(Ignite k in gameObject.GetComponents<Ignite>())
        {
            if (k != this)
            {
                k.StopAllCoroutines();
                Destroy(k);
            }
        }
        druation = _druation;
        dmg = _dmg;
        GameObject GO = (GameObject)Instantiate(Resources.Load("VisualEffects/OnFire"));
        VisualEffect = GO;
        GO.transform.parent = transform.GetChild(0);
        GO.transform.localPosition = Vector3.zero;
        delay = druation * 0.1f;
        StartCoroutine(Burn());
    }

    IEnumerator Burn()
    {
        SlimeBehaviour tmp = GetComponent<SlimeBehaviour>();
        for(int i=0; i<10; i++)
        {
            tmp.GetDMG(dmg);
            yield return new WaitForSeconds(delay);
        }      
        Destroy(this);
    }

    void OnDestroy()
    {
        StopCoroutine(Burn());
        if (VisualEffect == null) VisualEffect = gameObject.transform.GetChild(0).Find("OnFire(Clone)").gameObject;
        Destroy(VisualEffect);
    }
}
