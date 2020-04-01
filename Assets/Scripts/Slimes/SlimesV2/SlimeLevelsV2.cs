using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeLevelsV2 : MonoBehaviour
{
    List<Crystal> Crystals = new List<Crystal>();
    List<Color> Colors = new List<Color>();

    float Attack;
    float AttackSpeed;
    float Health;
    float MovmentSpeed;

    int[] Typ = new int[4];

    public void AddCrystal(Crystal crystal)
    {
        Typ[crystal.Type]++;
        Attack += crystal.AttackBonus;
        AttackSpeed += crystal.AttackSpeedBonus;
        Health += crystal.HealthBonus;
        MovmentSpeed += crystal.MovmentSpeedBonus;

        Colors.Add(crystal.gameObject.GetComponent<Renderer>().material.color);

        Color tmp = Color.black;
        foreach(Color k in Colors)
        {
            tmp += k;
        }
        tmp /= (float)Colors.Count;
        tmp.a = 0.5f;
        transform.GetChild(0).GetComponent<Renderer>().material.color = tmp;
        Destroy(crystal.gameObject);
    }
}
