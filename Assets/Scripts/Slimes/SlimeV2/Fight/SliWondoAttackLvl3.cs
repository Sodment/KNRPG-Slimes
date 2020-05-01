using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliWondoAttackLvl3 : FightCallback
{
    GameObject target;
    List<GameObject> enemyList = new List<GameObject>();
    private int playerID;
    private int delay = 0;

    private void Awake()
    {
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
                target = null;
                enemyList.Remove(target);
                if (enemyList.Count > 0)
                {
                    target = enemyList[0];
                    base.Attack();
                    target.GetComponent<HealthCallback>().GetDMG(modedDMG);
                    delay--;
                    if (delay == 0)
                    {
                        foreach (Collider k in Physics.OverlapBox(transform.position + (target.transform.position - transform.position).normalized, new Vector3(0.4f, 0.4f, 1), Quaternion.LookRotation(target.transform.position - transform.position)))
                        {
                            if (k.GetComponent<SlimeBahaviourV2>().PlayerID != playerID)
                            {
                                k.GetComponent<Rigidbody>().AddForce((target.transform.position - transform.position).normalized * 4.0f, ForceMode.Impulse);
                            }
                        }
                        delay = 5;
                    }
                }
            }
            else
            {
                base.Attack();
                target.GetComponent<HealthCallback>().GetDMG(modedDMG);
                delay--;
                if (delay == 0)
                {
                    foreach (Collider k in Physics.OverlapBox(transform.position + (target.transform.position - transform.position).normalized, new Vector3(0.4f, 0.4f, 1), Quaternion.LookRotation(target.transform.position - transform.position)))
                    {
                        if (k.GetComponent<SlimeBahaviourV2>().PlayerID != playerID)
                        {
                            k.GetComponent<Rigidbody>().AddForce((target.transform.position - transform.position).normalized * 4.0f, ForceMode.Impulse);
                        }
                    }
                    delay = 5;
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
