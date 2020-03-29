using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonContainer : MonoBehaviour
{
    public GameObject ContainObject;
    Text[] Counter = new Text[4];

    private void Awake()
    {
        Counter[0] = transform.GetChild(0).GetComponent<Text>();
        Counter[1] = transform.GetChild(1).GetComponent<Text>();
        Counter[2] = transform.GetChild(2).GetComponent<Text>();
        Counter[3] = transform.GetChild(3).GetComponent<Text>();
    }

    public void RefreshData()
    {
        if (ContainObject != null)
        {
            SlimeLevels Data = ContainObject.GetComponent<SlimeLevels>();
            for(int i=0; i<4; i++)
            {
                Counter[i].text = Data.GetCrystalCount(i);
            }
        }
        else
        {
            for (int i = 0; i < 4; i++)
            {
                Counter[i].text = "";
            }
        }
    }
}
