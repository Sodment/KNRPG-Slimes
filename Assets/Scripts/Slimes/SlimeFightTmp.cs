using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlimeFightTmp : MonoBehaviour
{
    public static float HP = 20;
    private float currentHP = HP;

    public float AttackSpeed = 1;
    public float dmg = 5;
    float Reload;
    float reload;

    [SerializeField]
    private Canvas healthCanvas=null;
    public Image HealthBar;

    GameObject Enemy;

    List<GameObject> PotentialEnemies = new List<GameObject>();

    private void Start()
    {
        Reload = 1.0f / AttackSpeed;
        healthCanvas.enabled = false;
    }

    private void OnDisable()
    {
        PotentialEnemies.Clear();
        healthCanvas.enabled = false;
        Enemy = null;
    }

    private void Update()
    {
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
            Enemy.SendMessage("GetDMG", dmg);
            reload = Reload;
        }

    }

    public void GetDMG(float dmg)
    {
        currentHP -= dmg;
        HealthBar.fillAmount = currentHP/HP;
        if (HealthBar.fillAmount != 1.0f)
        {
            healthCanvas.enabled = true;
        }
        else
        {
            healthCanvas.enabled = false;
        }
        if (currentHP <= 0) {
            GetComponent<SlimeBehaviour>().ChangeState(SlimeBehaviour.State.Die);
        }
    }

    public void Respawn()
    {
        currentHP = HP;
        GetDMG(0);
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
