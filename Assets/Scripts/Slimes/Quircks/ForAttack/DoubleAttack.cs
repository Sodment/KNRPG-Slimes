using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleAttack : AttackCallback
{
    public override void Attack(GameObject Enemy)
    {
        Enemy.GetComponent<SlimeBehaviour>().GetDMG(GetComponent<SlimeLevelsV2>().Attack*2);
    }
}