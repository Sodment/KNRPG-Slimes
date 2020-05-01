using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovmentCallback : MonoBehaviour
{
    private float defaultSpeed = 1.0f;
    protected float currentSpeed = 1.0f;

    private void Awake()
    {
        foreach (MovmentCallback k in GetComponents<MovmentCallback>())
        {
            if (k != this) { Destroy(k); }
        }
    }
    public void UpdateMods()
    {
        float currentSpeed = defaultSpeed;
        foreach(MovmentMods k in GetComponents<MovmentMods>())
        {
            currentSpeed = k.Calculate(currentSpeed);
        }
    }
}
