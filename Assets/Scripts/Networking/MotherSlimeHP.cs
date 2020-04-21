using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotherSlimeHP : MonoBehaviour
{
    public void GetDMG()
    {
        Destroy(transform.GetChild(0).gameObject);
        List<Transform> Crystals = new List<Transform>();
        Crystals.AddRange(transform.GetComponentsInChildren<Transform>());
        if (Crystals.Contains(transform))
        {
            Crystals.Remove(transform);
        }
        float AngleDiffrence = 360.0f / (Crystals.Count - 1) * Mathf.Deg2Rad;
        for (int i = 0; i < Crystals.Count; i++)
        {
            Crystals[i].localPosition = new Vector3(Mathf.Sin(AngleDiffrence * i), 0.0f, Mathf.Cos(AngleDiffrence * i));
        }
    }
}
