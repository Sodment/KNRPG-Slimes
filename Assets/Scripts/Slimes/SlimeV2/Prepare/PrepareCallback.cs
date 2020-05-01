using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrepareCallback : MonoBehaviour
{
    private void Awake()
    {
        foreach (PrepareCallback k in GetComponents<PrepareCallback>())
        {
            if (k != this) { Destroy(k); }
        }
    }
}
