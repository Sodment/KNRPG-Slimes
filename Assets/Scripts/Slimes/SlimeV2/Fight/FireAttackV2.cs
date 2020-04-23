﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireAttackV2 : FightCallback 
{
    GameObject target;
    List<GameObject> enemyList = new List<GameObject>();
    private int playerID;

    private void Awake()
    {
        playerID = GetComponent<SlimeBahaviourV2>().PlayerID;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<SlimeBahaviourV2>() != null)
        {
            if (collision.gameObject.GetComponent<SlimeBahaviourV2>().PlayerID != playerID)
            {
                enemyList.Add(collision.gameObject);
            }
            if (target == null) { target = enemyList[0]; }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (enemyList.Contains(collision.gameObject))
        {
            if (collision.gameObject == target)
            {
                target = null;
            }
            enemyList.Remove(collision.gameObject);
            if (target == null && enemyList.Count > 0)
            {
                target = enemyList[0];
            }
        }
    }

    protected override void Attack()
    {
        if (target != null)
        {
            base.Attack();
            target.GetComponent<HealthCallback>().GetDMG(modedDMG);
            if (Random.Range(0, 100) < 1000) 
            {
                target.AddComponent<IgniteV2>();
                target.GetComponent<IgniteV2>().Prepare(1.0f);
            }
        }
    }

    private void OnDisable()
    {
        enemyList.Clear();
        target = null;
    }
}