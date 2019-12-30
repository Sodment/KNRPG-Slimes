using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeMovement : MonoBehaviour
{
    public int PlayerID; //Później będzie zależne od photonview
    //[HideInInspector]
    public SlimeMovement Target;
    [HideInInspector]
    public Node MyNode;
    [HideInInspector]
    public Node NextNode;
    public bool Fight = false;

    public float Speed = 1;

    private void Start()
    {
        GridMenager GM = GameObject.Find("Plansza").GetComponent<GridMenager>();
        MyNode = GM.GetNodFromWolrdPoint(transform.position);
        NextNode = MyNode;
        Target = FindEnemy();
    }

    private void Update()
    {
        if (Target == this) { Target = FindEnemy(); }
        if(Vector3.Distance(transform.position+Vector3.down*transform.position.y, NextNode.transform.position) > Speed*2.0f*Time.deltaTime)
        {
            transform.Translate((NextNode.transform.position - MyNode.transform.position).normalized * Time.deltaTime * Speed, Space.World);
        }
    }

    public SlimeMovement FindEnemy()
    {
        GridMenager GM = GameObject.Find("Plansza").GetComponent<GridMenager>();
        float MinDist = float.MaxValue;
        SlimeMovement SM = this;
        foreach (GameObject k in GameObject.FindGameObjectsWithTag("Slime"))
        {
            if (k.GetComponent<SlimeMovement>() == null) { continue; }
            if(k.GetComponent<SlimeMovement>().PlayerID == PlayerID) { continue; }
            if (k.GetComponent<SlimeMovement>().MyNode == null) { continue; }
            if(GM.GetWeight(MyNode, k.GetComponent<SlimeMovement>().MyNode) < MinDist)
            {
                MinDist = GM.GetWeight(MyNode, k.GetComponent<SlimeMovement>().MyNode);
                SM = k.GetComponent<SlimeMovement>();
            }
        }
        return SM;
    }
}
