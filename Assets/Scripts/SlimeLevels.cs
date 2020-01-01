using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeLevels : MonoBehaviour
{
    List<int> Crystals = new List<int>();
    public int LVL = 1;
    public int Type = 0;

    public Color[] IlustrateColor;

    public void AddCrystal(int type)
    {
        Crystals.Add(type);
        if(Crystals.FindAll(k=> k == type).Count >= 3)
        {
            Type = type;
            LVL++;
            GetComponent<Renderer>().material.color = IlustrateColor[Type];
            Crystals.Clear();
        }
    }

}
