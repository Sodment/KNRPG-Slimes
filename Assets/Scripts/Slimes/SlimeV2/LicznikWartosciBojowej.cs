using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LicznikWartosciBojowej : MonoBehaviour
{
    float MyAttack = 10;
    float MyAttackSpeed = 0.3F;
    float MyHealth = 50;

    float AvgAttack = 0;
    float AvgAttackSpeed = 0;

    public GameObject[] Crystals;

    public List<GameObject> BadanySlime = new List<GameObject>();

    public int BadanaProbka = 100;

    private void Start()
    {
        CalculateMySlime();
    }

    void CalculateMySlime()
    {
        foreach (GameObject k in BadanySlime)
        {
            MyAttack += k.GetComponent<Crystal>().AttackBonus;
            MyAttackSpeed += k.GetComponent<Crystal>().AttackSpeedBonus;
            MyHealth += k.GetComponent<Crystal>().HealthBonus;
        }

        for(int i=0; i<BadanaProbka; i++)
        {
            float TmpAttack = 10;
            float TmpAttackSpeed = 0.3F;
            
            for(int j=0; j<BadanySlime.Count; j++)
            {
                int rand = Random.Range(0, 4);
                TmpAttack += Crystals[rand].GetComponent<Crystal>().AttackBonus;
                TmpAttackSpeed += Crystals[rand].GetComponent<Crystal>().AttackSpeedBonus;
            }

            AvgAttack += TmpAttack;
            AvgAttackSpeed += TmpAttackSpeed;
        }

        AvgAttack /= BadanaProbka;
        AvgAttackSpeed /= BadanaProbka;

        float aliveTime = MyHealth / (AvgAttack * AvgAttackSpeed);

        float Value = aliveTime * MyAttack * MyAttackSpeed;

        Debug.Log(Value);
    }
}
