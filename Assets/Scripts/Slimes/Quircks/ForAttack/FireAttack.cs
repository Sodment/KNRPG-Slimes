using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireAttack : AttackCallback
{
    public override void Attack(GameObject Enemy)
    {
        if (Random.Range(0, 100) >= 50)
        {
            Enemy.AddComponent<Ignite>();
            Enemy.GetComponent<Ignite>().Prepare(2.0f, 5.0f);
        }
        Enemy.GetComponent<SlimeBehaviour>().GetDMG(GetComponent<SlimeLevelsV2>().Attack);
    }
}
