using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stat
{
    [SerializeField]
    private float baseValue;

    public float GetValue()
    {
        return baseValue;
    }

}