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

    private void Start()
    {
        PlayerID = GetComponent<SlimeBehaviour>().PlayerID;
        RB = GetComponent<Rigidbody>();
    }

    void Update()
    {
        float MinimumDistance = float.MaxValue;
        Target = transform.position;
        Vector3 Predirect = Vector3.zero;
        foreach(GameObject Slime in GameObject.FindGameObjectsWithTag("Slime"))
        {
            if (Slime.GetComponent<AbominacjaRobiacaZaPathFind>().PlayerID != PlayerID)
            {
                float dist = Vector3.Distance(transform.position, Slime.transform.position);
                if (dist < MinimumDistance)
                {
                    Target = Slime.transform.position;
                    Predirect= Slime.GetComponent<Rigidbody>().velocity * 10.0f * Time.deltaTime;
                    MinimumDistance = dist;
                }
            }
        }
        if (Target == transform.position)
        {
            //GetComponent<SlimeBehaviour>().ChangeState(SlimeBehaviour.State.Prepare);
            RB.velocity = Vector3.zero;
            return;
        }
        Vector3 root = Vector3.Cross((Target+Predirect - transform.position), ToCross);
        RB.AddTorque(root*(0.5f+Mathf.Pow(MinimumDistance,1.7f)), ForceMode.Force);
        RB.AddForce(-transform.position.normalized*Mathf.Pow(Vector3.Magnitude(transform.position),0.7f)+ToCross);
    }
}