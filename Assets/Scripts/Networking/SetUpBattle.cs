using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class SetUpBattle : MonoBehaviour
{
    public int SetUpDruation = 30;
    Text Timer;
    InstantiateObjects ObjMenager;
    void Start()
    {
        Timer = GameObject.Find("Timer").GetComponent<Text>();
        ObjMenager = GetComponent<InstantiateObjects>();
        StartCoroutine(Prepare());
    }

    IEnumerator Prepare()
    {
        while (SetUpDruation >= 0)
        {
            Timer.text = SetUpDruation.ToString();
            SetUpDruation--;
            yield return new WaitForSecondsRealtime(1);
        }

        foreach(GameObject k in GameObject.FindGameObjectsWithTag("Slime"))
        {
            if (!k.transform.parent.GetComponent<Node>()) { continue; }
            if (k.GetComponent<SlimeMovement>()) { continue; }
            ObjMenager.InstantiateObject(k);
            Destroy(k);
        }
    }
}
