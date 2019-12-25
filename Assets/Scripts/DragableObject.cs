using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragableObject : MonoBehaviour
{
    public Vector3 TargetPos;
    private void Update()
    {
        if (Vector3.Distance(transform.position, TargetPos) > 0.01f)
        { transform.Translate((TargetPos - transform.position) * Time.deltaTime * 5.0f, Space.World); }
        else
        {
            transform.position = TargetPos;
        }
    }
}