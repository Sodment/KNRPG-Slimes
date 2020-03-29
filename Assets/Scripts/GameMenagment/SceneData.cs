using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneData : ScriptableObject
{
    List<GameObject> Slimes = new List<GameObject>();
    List<SlimeLevels> SlimeData = new List<SlimeLevels>();
    List<int> OwnerData = new List<int>();

    public void Save()
    {
        foreach (SlimeLevels k in GameObject.FindObjectsOfType<SlimeLevels>())
        {
            Slimes.Add(k.gameObject);
            SlimeData.Add(k);
        }
    }
}
