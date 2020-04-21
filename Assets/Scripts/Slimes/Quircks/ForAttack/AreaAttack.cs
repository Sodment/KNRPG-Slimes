using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaAttack : AttackCallback
{
    public override void Attack(GameObject Target)
    {
        base.Attack(Target);
        int EnemyID = Target.GetComponent<SlimeBehaviour>().PlayerID;
        foreach(Collider k in Physics.OverlapSphere(Target.transform.position, 1.5f))
        {
            SlimeBehaviour slimeBehaviour = k.GetComponent<SlimeBehaviour>();
            if (slimeBehaviour != null)
            {
                if (slimeBehaviour.PlayerID == EnemyID)
                { 
                    slimeBehaviour.GetDMG(GetComponent<SlimeLevelsV2>().Attack * (1.0f - (Vector3.Distance(k.transform.position, Target.transform.position) * 0.66667f))); 
                }
            }
        }
    }
}
