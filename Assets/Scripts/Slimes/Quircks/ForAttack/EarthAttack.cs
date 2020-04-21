using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthAttack : AttackCallback
{
    public override void Attack(GameObject Enemy)
    {
        if (Random.Range(0, 100) >= 0)
        {
            Enemy.AddComponent<Stun>();
            Enemy.GetComponent<Stun>().Prepare(3.0f);
        }
        //Enemy.GetComponent<Rigidbody>().AddForce((Enemy.transform.position - transform.position+Vector3.up*0.2f) * 5.0f, ForceMode.Impulse);
        Enemy.GetComponent<SlimeBehaviour>().GetDMG(GetComponent<SlimeLevelsV2>().Attack);
    }
}
