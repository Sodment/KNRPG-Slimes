using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridMenager : MonoBehaviour
{
    public GameObject NodePref;
    private void Awake()
    {
        for (int x = -4; x < 4; x++)
        {
            for (int y = -4; y < 4; y++)
            {
                GameObject GO = (GameObject)Instantiate(NodePref, new Vector3(x + 0.5f, 0.0f, y + 0.5f), Quaternion.identity);
                GO.transform.parent = this.transform;
                GO.GetComponent<Renderer>().material.SetTextureScale("_MainTex", new Vector2(0.125f, 0.125f));
                GO.GetComponent<Renderer>().material.SetTextureOffset("_MainTex", new Vector2(0.125f * (x+4), 0.125f * (y+4)));
            }
        }
    }

}
