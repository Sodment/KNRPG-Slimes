using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthMods : Mods
{
    private void Awake()
    {
        GetComponent<HealthCallback>().UpdateMods();
    }
    public virtual void Calculate(ref float _currentHP, ref float _maxHP)
    {

    }

    private void OnDestroy()
    {
        GetComponent<HealthCallback>().UpdateMods();
    }
}
