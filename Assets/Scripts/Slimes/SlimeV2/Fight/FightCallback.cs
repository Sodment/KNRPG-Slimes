using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightCallback : MonoBehaviour
{
    protected float baseDMG;
    protected float modedDMG;

    float baseAttackSpeed;
    float baseReload;
    float modedReload;
    float reload;

    protected virtual void Awake()
    {
        foreach(FightCallback k in GetComponents<FightCallback>())
        {
            if(k!=this) { Destroy(k); }
        }
    }

    protected void Prepare()
    {
        baseDMG = GetComponent<SlimeLevelsV2>().Attack;
        modedDMG = baseDMG;
        baseAttackSpeed = GetComponent<SlimeLevelsV2>().AttackSpeed;
        baseReload = 1.0f / baseAttackSpeed;
        modedReload = baseReload;
    }

    public void UpdateMods()
    {
        modedReload = baseReload;
        modedDMG = baseDMG;
        foreach(FightMods mod in GetComponents<FightMods>())
        {
            mod.Calculate(ref modedReload, ref modedDMG);
        }
    }

    private void Update()
    {
        if (reload <= 0)
        {
            Attack();
        }
        else
        {
            reload -= Time.deltaTime;
        }
    }

    protected virtual void Attack()
    {
        reload = modedReload;
    }
}
