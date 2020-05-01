using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireAttackLvl2 : FightCallback
{
    GameObject target;
    List<GameObject> enemyList = new List<GameObject>();
    private int playerID;

    private void Awake()
    {
        playerID = GetComponent<SlimeBahaviourV2>().PlayerID;
    }

    private void OnEnable()
    {
        playerID = GetComponent<SlimeBahaviourV2>().PlayerID;
        Prepare();
        baseDMG += 2;
        modedDMG += 2;
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
                    foreach (Collider k in Physics.OverlapSphere(target.transform.position, 1.5f))
                    {
                        if (k.gameObject.GetComponent<SlimeBahaviourV2>().PlayerID == playerID) continue;
                        else
                        {
                            if (k.gameObject.GetComponent<FireImmunity>() || k.gameObject.GetComponent<FireImmunityLvl3>())
                            {
                                k.GetComponent<HealthCallback>().GetDMG(modedDMG * (Vector2.Distance(k.transform.position, target.transform.position) * 0.3f));
                            }
                            else
                            {
                                k.GetComponent<HealthCallback>().GetDMG(modedDMG * (Vector2.Distance(k.transform.position, target.transform.position) * 0.3f));
                                target.AddComponent<IgniteV2>();
                                target.GetComponent<IgniteV2>().Prepare(1.0f);
                            }
                        }
                    }
                }
            }
            else
            {
                base.Attack();
                foreach (Collider k in Physics.OverlapSphere(target.transform.position, 1.5f))
                {
                    if (k.gameObject.GetComponent<SlimeBahaviourV2>().PlayerID == playerID) continue;
                    else
                    {
                        if (k.gameObject.GetComponent<FireImmunity>() || k.gameObject.GetComponent<FireImmunityLvl3>())
                        {
                            k.GetComponent<HealthCallback>().GetDMG(modedDMG * (Vector2.Distance(k.transform.position, target.transform.position) * 0.3f));
                        }
                        else
                        {
                            k.GetComponent<HealthCallback>().GetDMG(modedDMG * (Vector2.Distance(k.transform.position, target.transform.position) * 0.3f));
                            target.AddComponent<IgniteV2>();
                            target.GetComponent<IgniteV2>().Prepare(1.0f);
                        }
                    }
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
