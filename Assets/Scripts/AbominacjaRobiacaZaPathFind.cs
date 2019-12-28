using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbominacjaRobiacaZaPathFind : MonoBehaviour
{
    Transform Target;
    public float Delay;
    public float StartDelay;
    Vector3 Direction;
    private void Start()
    {
        Material MyMaterial = GetComponent<Renderer>().sharedMaterial;
        GameObject[] Slimes = GameObject.FindGameObjectsWithTag("Slime");
        float Distance = float.MaxValue;
        foreach(GameObject k in Slimes)
        {
            if (k.GetComponent<Renderer>().sharedMaterial != MyMaterial)
            {
                if(Vector3.Distance(k.transform.position, transform.position) < Distance)
                {
                    Target = k.transform;
                    Distance = Vector3.Distance(k.transform.position, transform.position);
                }
            }
        }
       // delay = Delay;
    }
    void Update()
    {
        if (Vector3.Distance(Target.position, transform.position) > 1.2f)
        {
            if (StartDelay <= 0)
            {
                transform.position = new Vector3(Mathf.Round(transform.position.x * 10.0f) * 0.1f, transform.position.y, Mathf.Round(transform.position.z * 10.0f) * 0.1f);
                Direction = Vector3.zero;
                Vector3 Way = Target.position - transform.position;
                if (Mathf.Abs(Way.x) > Mathf.Abs(Way.z)) 
                {
                    Ray ray = new Ray(transform.position, Vector3.right * Mathf.Sign(Way.x));
                    if(!Physics.Raycast(ray, 1.0f))
                    { Direction = Vector3.right * Mathf.Sign(Way.x); }
                    else
                    {
                        ray = new Ray(transform.position, Vector3.forward * Mathf.Sign(Way.z));
                        if (!Physics.Raycast(ray, 1.0f))
                        { Direction = Vector3.forward * Mathf.Sign(Way.z); }
                    }
                }
                else 
                {
                    Ray ray = new Ray(transform.position, Vector3.forward * Mathf.Sign(Way.z));
                    if (!Physics.Raycast(ray, 1.0f))
                    { Direction= Vector3.forward * Mathf.Sign(Way.z); }
                    else
                    {
                        ray = new Ray(transform.position, Vector3.right * Mathf.Sign(Way.x));
                        if (!Physics.Raycast(ray, 1.0f))
                        { Direction = Vector3.right * Mathf.Sign(Way.x); }
                    }
                }
                transform.position += Direction;
                    StartDelay = Delay;
            }
            StartDelay -= Time.deltaTime;
        }
    }
}