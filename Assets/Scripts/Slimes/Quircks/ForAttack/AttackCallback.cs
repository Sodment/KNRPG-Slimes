using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCallback : MonoBehaviour
{
    private void Start()
    {
        foreach(AttackCallback k in gameObject.GetComponents<AttackCallback>())
        {
            if (k != this) { Destroy(k); }
        }
    }

    public virtual void Attack(GameObject Target)
    {
    }
}
