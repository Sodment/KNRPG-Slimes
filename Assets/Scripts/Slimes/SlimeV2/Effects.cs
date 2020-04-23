using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effects : MonoBehaviour
{
    Mods[] causingMods = null;
    float druation;

    public virtual void Prepare(float _druation, Mods[] _causingMods = null)
    {
        druation = _druation;
        causingMods = _causingMods;
        Destroy(this, druation);
        if(causingMods!=null)
        for(int i=0; i<causingMods.Length; i++)
        {
            Mods tmp = gameObject.AddComponent<Mods>();
            tmp = causingMods[i];
            Destroy(tmp, druation);
        }
    }
}
