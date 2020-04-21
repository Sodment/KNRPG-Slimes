using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class AbominacjaRobiacaZaPathFind : MonoBehaviour
{
    Rigidbody RB;
    int PlayerID;
    Vector3 ToCross = new Vector3(0, -1, 0);
    [HideInInspector]
    public Vector3 Target;

    public float velocity = 1.0f;
    public float defaultVelocity = 1.0f;

    private void Start()
    {
        PlayerID = GetComponent<SlimeBehaviour>().PlayerID;
        RB = GetComponent<Rigidbody>();
    }

    void Update()
    {
        float MinimumDistance = float.MaxValue;
        Target = transform.position;
        foreach(GameObject Slime in GameObject.FindGameObjectsWithTag("Slime"))
        {
            if (Slime.GetComponent<AbominacjaRobiacaZaPathFind>().PlayerID != PlayerID)
            {
                float dist = Vector3.Distance(transform.position, Slime.transform.position);
                if (dist < MinimumDistance)
                {
                    Target = Slime.transform.position;
                    MinimumDistance = dist;
                }
            }
        }
        if (Target == transform.position)
        {
            RB.velocity = Vector3.zero;
            return;
        }
        Vector3 root = Vector3.Cross((Target - transform.position*2.0f), ToCross);
        RB.AddTorque(root*(0.5f+Mathf.Pow(MinimumDistance,1.7f))*velocity, ForceMode.Force);
    }
}