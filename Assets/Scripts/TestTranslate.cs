using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestTranslate : MonoBehaviour
{
    void Update()
    {
        transform.Translate(new Vector3(Time.deltaTime, 0, 0), Space.World);
    }
}
