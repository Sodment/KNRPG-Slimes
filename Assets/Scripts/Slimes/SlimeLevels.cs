using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlimeLevels : MonoBehaviour
{
    List<int> Crystals = new List<int>();
    public int LVL = 1;
    public int Type = 0;

    public Color[] IlustrateColor;
    Canvas Stats;
    Text[] Counter = new Text[4];

    private void Awake()
    {
        Stats = GetComponentInChildren<Canvas>();
        Counter[0] = Stats.transform.GetChild(0).GetComponent<Text>();
        Counter[1] = Stats.transform.GetChild(1).GetComponent<Text>();
        Counter[2] = Stats.transform.GetChild(2).GetComponent<Text>();
        Counter[3] = Stats.transform.GetChild(3).GetComponent<Text>();
    }

    public void AddCrystal(int type)
    {
        Crystals.Add(type);
        int[] types = new int[4];
        types[0] = Crystals.FindAll(k => k == 1).Count;
        types[1] = Crystals.FindAll(k => k == 2).Count;
        types[2] = Crystals.FindAll(k => k == 3).Count;
        types[3] = Crystals.FindAll(k => k == 4).Count;

        if (types[0] == types[1] && types[1] == types[2] && types[2] == types[3])
        {
            Type = 0;
        }
        else
        {
            int Max = 0;
            int curr = 0;
            for(int i=0; i<4; i++)
            {
                if (types[i] > Max) { Max = types[i]; curr = i + 1; }
            }
            Type = curr;
        }
            
        GetComponent<Renderer>().material.color = IlustrateColor[Type];
        OnMouseEnter();
    }

    public string GetCrystalCount(int type)
    {
        return Crystals.FindAll(k => k == type + 1).Count.ToString();
    }


    private void OnMouseEnter()
    {
        ShowStats();
    }

    private void OnMouseExit()
    {
        HidenStats();
    }

    void ShowStats()
    {
        Stats.enabled = true;
        Counter[0].text = GetCrystalCount(0);
        Counter[1].text = GetCrystalCount(1);
        Counter[2].text = GetCrystalCount(2);
        Counter[3].text = GetCrystalCount(3);
    }

    void HidenStats()
    {
        Stats.enabled = false;
    }
}
