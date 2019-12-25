using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbominacjaRobiacaZaPathFind : MonoBehaviour
{
    public Transform Target;
    public float Delay;
    float delay;
    private void Start()
    {
        delay = Delay;
    }
    void Update()
    {
        if(Vector3.Distance(Target.position, transform.position) > 1.2f)
        {
            if (delay <= 0)
            {
                Vector3 Way = Target.position - transform.position;
                if (Mathf.Abs(Way.x) > Mathf.Abs(Way.z)) { transform.position += Vector3.right * Mathf.Sign(Way.x); }
                else { transform.position += Vector3.forward * Mathf.Sign(Way.z); }
                delay = Delay;
            }
            delay -= Time.deltaTime;
        }
    }
}