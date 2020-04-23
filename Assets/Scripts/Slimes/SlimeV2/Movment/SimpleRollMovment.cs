﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleRollMovment : MovmentCallback
{
    Vector3 ToCross = new Vector3(0, -1, 0);
    private Vector3 Target;

    List<SlimeBahaviourV2> enemyList = new List<SlimeBahaviourV2>();

    Rigidbody rigidbody;

    private void OnEnable()
    {
        rigidbody = GetComponent<Rigidbody>();
        int myID = GetComponent<SlimeBahaviourV2>().PlayerID;
        enemyList.Clear();
        foreach(SlimeBahaviourV2 k in GameObject.FindObjectsOfType<SlimeBahaviourV2>())
        {
            if (k.PlayerID != myID) { enemyList.Add(k); }
        }
    }

    void Update()
    {
        float MinimumDistance = float.MaxValue;
        Target = transform.position+Vector3.up;
        foreach (SlimeBahaviourV2 Slime in enemyList)
        {
            if (Slime.gameObject!=null)
            {
                float dist = Vector3.Distance(transform.position, Slime.transform.position);
                if (dist < MinimumDistance)
                {
                    Target = Slime.transform.position;
                    MinimumDistance = dist;
                }
            }
        }

        Vector3 root = Vector3.Cross((Target - transform.position*2), ToCross);
        rigidbody.AddTorque(root * (0.5f + Mathf.Pow(MinimumDistance, 1.7f)) * currentSpeed, ForceMode.Force);
    }
}