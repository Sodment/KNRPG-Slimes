using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitMenager : MonoBehaviour
{
    private void Awake()
    {
        StartCoroutine(PrepareUnits());
    }

    IEnumerator PrepareUnits()
    {
        foreach(GameObject k in GameObject.FindGameObjectsWithTag("Slime"))
        {
            k.GetComponent<SlimeMovement>().enabled = true;
            yield return new WaitForEndOfFrame();
        }
    }
}
