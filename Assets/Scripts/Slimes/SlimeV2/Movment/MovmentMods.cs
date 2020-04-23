using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovmentMods : Mods
{
    private void Awake()
    {
        GetComponent<MovmentCallback>().UpdateMods();
    }
    public virtual float Calculate(float _inputValue)
    {
        return _inputValue;
    }

    private void OnDestroy()
    {
        GetComponent<MovmentCallback>().UpdateMods();
    }
}
