using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeFightTmp : MonoBehaviour
{
    public float HP = 20;
    public float AttackSpeed = 1;
    public float dmg = 5;

    float wait;

    public SlimeFightTmp Enemy;
    SlimeMovement Movement;

    private void Start()
    {
        Movement = GetComponent<SlimeMovement>();
        wait = 1.0f / AttackSpeed;
    }

    private void Update()
    {
        if (Movement.Fight && Enemy != null)
        {
            if (wait <= 0) 
            {
                Enemy.GetDMG(dmg);
                wait= 1.0f / AttackSpeed;
            }
            else { wait -= Time.deltaTime; }
        }
    }

    public void GetDMG(float dmg)
    {
        HP -= dmg;
        if (HP <= 0) {
            GetComponent<SlimeMovement>().FreeNodes();
            Destroy(gameObject);
        }
    }
}
