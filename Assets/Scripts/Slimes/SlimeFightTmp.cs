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
    public Image HealthBar;
    float wait;
    [SerializeField]
    private Canvas healthCanvas;

    public SlimeFightTmp Enemy;
    SlimeMovement Movement;

    private void Start()
    {
        Movement = GetComponent<SlimeMovement>();
        wait = 1.0f / AttackSpeed;
        healthCanvas.enabled = false;
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
        if(HealthBar.fillAmount != 1)
        {
            healthCanvas.enabled = true;
        }
    }

    public void GetDMG(float dmg)
    {
        currentHP -= dmg;
        HealthBar.fillAmount = currentHP/HP;
        if (currentHP <= 0) {
            GetComponent<SlimeMovement>().FreeNodes();
            Destroy(gameObject);
        }
    }
}
