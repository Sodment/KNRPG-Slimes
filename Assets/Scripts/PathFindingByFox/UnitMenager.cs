using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitMenager : MonoBehaviour
{
    List<SlimeMovement> Units1 = new List<SlimeMovement>();
    List<SlimeMovement> Units2 = new List<SlimeMovement>();
    GridMenager GM;

    public float DelayBetweenUnits = 0.2f;
    public float UnitsSpeed = 1.0f;
    private void Awake()
    {
        GM = GetComponent<GridMenager>();
        int ID1=0;
        int ID2=0;
        foreach(GameObject k in GameObject.FindGameObjectsWithTag("Slime"))
        {
            if (k.GetComponent<SlimeMovement>() == null) { continue; }
            SlimeMovement SM = k.GetComponent<SlimeMovement>();
            if (ID1 == 0) { ID1 = SM.PlayerID; }
            else if (ID2 == 0 && SM.PlayerID!=ID1) { ID2 = SM.PlayerID; }

            int CurrentID = SM.PlayerID;
            if (CurrentID == ID1) { Units1.Add(SM); }
            if (CurrentID == ID2) { Units2.Add(SM); }
        }
        DelayBetweenUnits = 1.0f / UnitsSpeed;
        StartCoroutine(Move());
    }

    public IEnumerator Move()
    {
        yield return new WaitForSecondsRealtime(1);
        while (true)
        {
            for (int i = 0; i < Mathf.Max(Units1.Count, Units2.Count); i++)
            {
                if (i < Units1.Count)
                {
                    if (!Units1[i].Fight)
                    {
                        Units1[i].FindEnemy();
                        GM.Move(Units1[i], Units1[i].Target.GetComponent<SlimeMovement>());
                        yield return new WaitForSecondsRealtime(DelayBetweenUnits);
                    }
                    
                }
                if (i < Units2.Count)
                {
                    if (!Units2[i].Fight)
                    {
                        Units2[i].FindEnemy();
                        GM.Move(Units2[i], Units2[i].Target.GetComponent<SlimeMovement>());
                        yield return new WaitForSecondsRealtime(DelayBetweenUnits);
                    }
                }
            }
            yield return new WaitForSecondsRealtime(1.0f);
        }
    }
}
