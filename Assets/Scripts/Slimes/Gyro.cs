using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gyro : MonoBehaviour
{
    AbominacjaRobiacaZaPathFind TargetData;
    Vector3 Up = Vector3.up;

    private void Awake()
    {
        TargetData = transform.parent.gameObject.GetComponent<AbominacjaRobiacaZaPathFind>();
    }
    void Update()
    {
        transform.rotation = Quaternion.LookRotation(TargetData.Target - transform.position, Up);
    }
}
