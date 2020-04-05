using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats
{
    [SerializeField]
    private int baseValue = 0 ;

    public int GetValue()
    {
        return baseValue;
    }

}