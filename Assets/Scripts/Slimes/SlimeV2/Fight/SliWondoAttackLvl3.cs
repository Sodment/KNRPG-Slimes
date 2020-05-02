using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliWondoAttackLvl3 : FightCallback
{
    GameObject target;
    List<GameObject> enemyList = new List<GameObject>();
    private int playerID;
    private int delay = 0;

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
                    delay--;
                    if (delay == 0)
                    {
                        delay = 10;
                        Vector3 dir = (target.transform.position - transform.position).normalized;
                        GameObject GO = (GameObject)Instantiate(Resources.Load("VisualEffects/Wind", typeof(GameObject)), transform.position, Quaternion.LookRotation(dir, Vector3.up));
                        Destroy(GO, 1.0f);
                        foreach (Collider k in Physics.OverlapBox(transform.position + dir, new Vector3(0.4f, 0.4f, 1), Quaternion.LookRotation(dir, Vector3.up)))
                        {
                            if (k.GetComponent<SlimeBahaviourV2>().PlayerID != playerID)
                            {
                                k.GetComponent<Rigidbody>().AddForce(dir * 10.0f, ForceMode.Impulse);
                            }
                        }
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
                    delay = 10;
                    Vector3 dir = (target.transform.position - transform.position).normalized;
                    GameObject GO = (GameObject)Instantiate(Resources.Load("VisualEffects/Wind", typeof(GameObject)), transform.position, Quaternion.LookRotation(dir, Vector3.up));
                    Destroy(GO, 1.0f);
                    foreach (Collider k in Physics.OverlapBox(transform.position + dir, new Vector3(0.4f, 0.4f, 1), Quaternion.LookRotation(dir, Vector3.up)))
                    {
                        if (k.GetComponent<SlimeBahaviourV2>().PlayerID != playerID)
                        {
                            k.GetComponent<Rigidbody>().AddForce(dir * 10.0f, ForceMode.Impulse);
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
