using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlimeFight : MonoBehaviour
{
    private float currenthp;
    float Reload;
    float reload;

    [SerializeField]
    //private Canvas healthCanvas = null;
    public Image HealthBar;

    GameObject Enemy;

    List<GameObject> PotentialEnemies = new List<GameObject>();

    private void Start()
    {
        Reload = 1.0f / GetComponent<SlimeLevelsV2>().AttackSpeed;
        currenthp = GetComponent<SlimeLevelsV2>().Health;
    }

    private void OnDisable()
    {
        PotentialEnemies.Clear();
        Enemy = null;
    }

    private void Update()
    {
        HealthBar.fillAmount = currenthp / GetComponent<SlimeLevelsV2>().Health;
        if (reload > 0.0f)
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
            Enemy.GetComponent<SlimeFight>().currenthp -= GetComponent<SlimeLevelsV2>().Attack;
            Debug.Log(name + " " + Enemy.name);
            reload = Reload;
        }
        if (currenthp <= 0)
        {
            GetComponent<SlimeBehaviour>().ChangeState(SlimeBehaviour.State.Die);
        }

    }
    public void Respawn()
    {
        currenthp = GetComponent<SlimeLevelsV2>().Health;
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
}
