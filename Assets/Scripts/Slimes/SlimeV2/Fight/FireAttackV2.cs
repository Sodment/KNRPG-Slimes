using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireAttackV2 : FightCallback 
{
    GameObject target;
    List<GameObject> enemyList = new List<GameObject>();
    private int playerID;

    protected override void Awake()
    {
        base.Awake();
        playerID = GetComponent<SlimeBahaviourV2>().PlayerID;
    }

    private void OnEnable()
    {
        playerID = GetComponent<SlimeBahaviourV2>().PlayerID;
        Prepare();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<SlimeBahaviourV2>() != null)
        {
            if (collision.gameObject.GetComponent<SlimeBahaviourV2>().PlayerID != playerID)
            {
                enemyList.Add(collision.gameObject);
            }
            if ((target == null || target.activeInHierarchy == false) && enemyList.Count > 0)
            {
                target = enemyList[0];
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (enemyList.Contains(collision.gameObject))
        {
            if (collision.gameObject == target || target.activeInHierarchy == false)
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
            if (!target.activeInHierarchy)
            {
                enemyList.Remove(target);
                target = null;
                if (enemyList.Count > 0)
                {
                    target = enemyList[0];
                    base.Attack();
                    target.GetComponent<HealthCallback>().GetDMG(modedDMG);
                    if (target.GetComponent<FireImmunity>() == null || target.GetComponent<FireImmunityLvl3>() == null)
                    {
                        Debug.Log("Podpalenie");
                        target.AddComponent<IgniteV2>();
                        target.GetComponent<IgniteV2>().Prepare(1.0f);
                    }
                }
            }
            else
            {
                base.Attack();
                target.GetComponent<HealthCallback>().GetDMG(modedDMG);
                if (target.GetComponent<FireImmunity>() == null || target.GetComponent<FireImmunityLvl3>() == null)
                {
                    Debug.Log("Podpalenie");
                    target.AddComponent<IgniteV2>();
                    target.GetComponent<IgniteV2>().Prepare(5.0f);
                }
            }
        }
    }

    private void OnDisable()
    {
        enemyList.Clear();
        target = null;
    }
}
