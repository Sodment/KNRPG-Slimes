using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightMods : Mods
{
    private void Awake()
    {
        GetComponent<FightCallback>().UpdateMods();
    }
    public virtual void Calculate(ref float _reload, ref float dmg)
    {
        
    }

    private void OnDestroy()
    {
        GetComponent<FightCallback>().UpdateMods();
    }
}
