using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TenWyjebistyKurwaSkokDoAssasinow : MovmentCallback
{
    Vector3 ToCross = new Vector3(0, -1, 0);
    private Vector3 Target;

    List<SlimeBahaviourV2> enemyList = new List<SlimeBahaviourV2>();

    Rigidbody rigidbody;

    bool jump = true;

    private void OnEnable()
    {
        rigidbody = GetComponent<Rigidbody>();
        int myID = GetComponent<SlimeBahaviourV2>().PlayerID;
        enemyList.Clear();
        foreach (SlimeBahaviourV2 k in GameObject.FindObjectsOfType<SlimeBahaviourV2>())
        {
            if (k.PlayerID != myID) { enemyList.Add(k); }
        }
        jump = true;
    }

    void Update()
    {
        if (jump)
        {
            HyperJump();
        }
        float MinimumDistance = float.MaxValue;
        Target = transform.position + Vector3.up;
        foreach (SlimeBahaviourV2 Slime in enemyList)
        {
            if (Slime.gameObject != null)
            {
                float dist = Vector3.Distance(transform.position, Slime.transform.position);
                if (Slime.gameObject.GetComponent<ShildLvl2>())
                {
                    dist = 0;
                }
                if (dist < MinimumDistance)
                {
                    Target = Slime.transform.position;
                    MinimumDistance = dist;
                }
            }
        }

        Vector3 root = Vector3.Cross((Target - transform.position * 2), ToCross);
        rigidbody.AddTorque(root * (0.5f + Mathf.Pow(MinimumDistance, 1.7f)) * currentSpeed, ForceMode.Force);
    }

    void HyperJump()
    {
        float Dist = Mathf.Abs(transform.position.z) + 3.5f;
        float g = 9.81f;
        float v = 100.0f;
        float alpha = Mathf.PI * 0.5f + Mathf.Asin(g * Dist / v) * 0.5f;

        Debug.Log(alpha);
        Debug.DrawLine(transform.position, transform.position + new Vector3(0, Mathf.Sin(alpha), Mathf.Cos(alpha) * (Mathf.Sign(transform.position.z))), Color.green, 30);

        rigidbody.AddForce(new Vector3(0, Mathf.Sin(alpha), Mathf.Cos(alpha) * (Mathf.Sign(transform.position.z))) * 10.0f, ForceMode.Impulse);

        jump = false;
    
    }
}
