using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlimeFight : MonoBehaviour
{
    float Reload;
    float reload;

    protected GameObject Enemy;

    List<GameObject> PotentialEnemies = new List<GameObject>();


    private void Start()
    {
        Reload = 1.0f / GetComponent<SlimeLevelsV2>().AttackSpeed;
    }

    private void OnDisable()
    {
        PotentialEnemies.Clear();
        Enemy = null;
    }

    private void Update()
    {
        if (reload > 0.0f && GetComponent<Stun>()==null)
        {
            reload -= Time.deltaTime;
        }

        if (Enemy!=null && Enemy.activeSelf == false)
        {
            if (PotentialEnemies.Contains(Enemy))
            {
                PotentialEnemies.Remove(Enemy);
            }

            if (PotentialEnemies.Count > 0)
            {
                Enemy = PotentialEnemies[0];
            }
            else { Enemy = null; }
        }

        if (Enemy != null && reload<=0.0f)
        {
            Attack();
            reload = Reload;
        }
    }
    public void Respawn()
    {
        Effect[] tmp = GetComponents<Effect>();
        foreach(Effect k in tmp)
        {
            Destroy(k);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<SlimeBehaviour>())
        {
            if (collision.gameObject.GetComponent<SlimeBehaviour>().PlayerID != GetComponent<SlimeBehaviour>().PlayerID)
            {
                PotentialEnemies.Add(collision.gameObject);
                if (Enemy == null)
                { Enemy = collision.gameObject; }
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (PotentialEnemies.Contains(collision.gameObject))
        {
            PotentialEnemies.Remove(collision.gameObject);
        }

        if (collision.gameObject == Enemy)
        {
            if (PotentialEnemies.Count > 0)
            {
                Enemy = PotentialEnemies[0];
            }
            else { Enemy = null; }
        }
    }

    void Attack()
    {
        GetComponent<AttackCallback>().Attack(Enemy); 
    }
}
