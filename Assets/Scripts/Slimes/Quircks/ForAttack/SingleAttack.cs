using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleAttack : AttackCallback
{
    public override void Attack(GameObject Enemy)
    {
        base.Attack(Enemy);
        Enemy.GetComponent<SlimeBehaviour>().GetDMG(GetComponent<SlimeLevelsV2>().Attack);
    }
}
