using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crystal : MonoBehaviour
{
    public int Type;

    void OnEnable()
    {
        if (transform.parent.GetComponent<SlimeLevels>())
        { 
            transform.parent.GetComponent<SlimeLevels>().AddCrystal(Type);
            Destroy(this.gameObject);
        }
    }
}
