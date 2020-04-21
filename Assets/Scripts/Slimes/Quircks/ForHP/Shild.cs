using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shild : SlimeHealth
{
    float shildHP;

    public override void Prepare(float _maxHP)
    {
        base.Prepare(_maxHP);
        shildHP = 10.0f;
    }

    public override void GetDMG(float _dmg)
    {
        shildHP -= _dmg;
        if (shildHP < 0.0f)
        { 
            base.GetDMG(-shildHP);
            shildHP = 0;
        }
    }
}
