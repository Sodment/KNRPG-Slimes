using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeLevelsV2 : MonoBehaviour
{
   // List<Crystal> Crystals = new List<Crystal>(); Z tego by była tylko długość... dokładnie taka sama jak z colors
    List<Color> Colors = new List<Color>();

    Color[] CrystalColor = new Color[4];

    public float Attack = 10;
    public float AttackSpeed = 0.3F;
    public float Health = 50;
    public float MovmentSpeed = 0.3F;

    int[] Typ = new int[4];

    public void Start()
    {
        GameObject Crystal = Resources.Load("Crystals/EarthCrystalV2") as GameObject;
        CrystalColor[0] = Crystal.GetComponent<Renderer>().sharedMaterial.color;
        Crystal = Resources.Load("Crystals/FireCrystalV2") as GameObject;
        CrystalColor[1] = Crystal.GetComponent<Renderer>().sharedMaterial.color;
        Crystal = Resources.Load("Crystals/WaterCrystalV2") as GameObject;
        CrystalColor[2] = Crystal.GetComponent<Renderer>().sharedMaterial.color;
        Crystal = Resources.Load("Crystals/WindCrystalV2") as GameObject;
        CrystalColor[3] = Crystal.GetComponent<Renderer>().sharedMaterial.color;
    }

    public void AddCrystal(Crystal crystal)
    {
        Typ[crystal.Type]++;
        Attack += crystal.AttackBonus;
        AttackSpeed += crystal.AttackSpeedBonus;
        Health += crystal.HealthBonus;
        MovmentSpeed += crystal.MovmentSpeedBonus;

        Colors.Add(CrystalColor[crystal.Type]);

        Vector3 ColorVec = Vector3.zero;
        foreach(Color k in Colors)
        {
            ColorVec += new Vector3(k.r, k.g, k.b);
        }
        ColorVec.Normalize();
        ColorVec /= Mathf.Max(ColorVec.x, ColorVec.y, ColorVec.z);
        transform.GetChild(0).GetComponent<Renderer>().material.color = new Color(ColorVec.x, ColorVec.y, ColorVec.z,0.5f);
    }

    public int[] Crystals()
    {
        List<int> CrystalList = new List<int>();
        
        for(int i=0; i<4; i++)
        {
            for(int type=0; type<Typ[i]; type++)
            {
                CrystalList.Add(i);
            }
        }
        return CrystalList.ToArray();
    }
}
