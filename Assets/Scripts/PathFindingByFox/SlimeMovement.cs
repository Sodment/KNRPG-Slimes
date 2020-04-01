using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SlimeMovement : MonoBehaviour
{
    public int PlayerID;
    public float MovmentSpeed = 3;
    Node MyNode;
    Node NextNode;

    public bool Fight = false;
    bool ReadyToFight = false;
    SlimeMovement Target;

    List<SlimeMovement> Enemies = new List<SlimeMovement>();

    GridMenager GM;

    private void OnEnable()
    {
        GM = GameObject.FindObjectOfType<GridMenager>();
        MyNode = GM.GetNodFromWolrdPoint(transform.position);
        MyNode.Walkable = false;
        foreach (SlimeMovement k in GameObject.FindObjectsOfType <SlimeMovement>())
        {
            if (k.PlayerID != PlayerID)
            {
                Enemies.Add(k);
            }
        }
        GetNext();
    }

    private void OnDisable()
    {
        MyNode.Walkable = true;
        NextNode.Walkable = true;
        MyNode = GetComponent<DragSlime>().LastParent.GetComponent<Node>();
        NextNode = MyNode;
    }
    void Update()
    {
        if (Enemies.Count > 0)
        {
            if (Vector3.Distance(transform.position, NextNode.transform.position) <= MovmentSpeed * 2 * Time.deltaTime)
            {
                transform.position = NextNode.transform.position;
                MyNode.Walkable = true;
                MyNode = NextNode;
                Enemies.Clear();
                foreach (SlimeMovement k in GameObject.FindObjectsOfType<SlimeMovement>())
                {
                    if (k.PlayerID != PlayerID)
                    {
                        Enemies.Add(k);
                    }
                }
                if (Enemies.Count == 0) {Debug.Log("Brak Przeciwników"); return; }
                NextNode = GetNext();
            }
            else
            {
                transform.Translate((NextNode.transform.position - MyNode.transform.position).normalized * MovmentSpeed * Time.deltaTime, Space.World);
            }

            if(ReadyToFight && Vector3.Distance(transform.position, Target.transform.position) <= 1.1f)
            {
                Fight = true;
            }
        }
    }

    Node GetNext()
    {
        Fight = false;
        ReadyToFight = false;
        float MinDistance = float.MaxValue;
        Target = null;
        List<SlimeMovement> ToRemove = new List<SlimeMovement>();
        foreach (SlimeMovement k in Enemies)
        {
            if (k == null) { ToRemove.Add(k); continue; }
            float distance = Vector3.Distance(transform.position, k.transform.position);
            if (distance <= MinDistance) {Target = k; MinDistance = distance; }
        }
        foreach (SlimeMovement k in ToRemove)
        {
            if (Enemies.Contains(k))
            { Enemies.Remove(k); }
        }
        if (Target == null) { return NextNode; }
        if (Target.NextNode == null)
        { NextNode = GM.GetNextNode(MyNode, GM.GetNodFromWolrdPoint(Target.transform.position)); }
        else { NextNode = GM.GetNextNode(MyNode, Target.NextNode); }
        NextNode.Walkable = false;
        if(NextNode == MyNode)
        {
           // GetComponent<SlimeFightTmp>().Enemy = Target.GetComponent<SlimeFightTmp>();
            ReadyToFight = true;
        }
        return NextNode;
    }

    public void FreeNodes()
    {
        MyNode.Walkable = true;
        NextNode.Walkable = true;
    }

    /*public int PlayerID; //Później będzie zależne od photonview
    //[HideInInspector]
    public SlimeMovement Target;
    //[HideInInspector]
    public Node MyNode;
    //[HideInInspector]
    public Node NextNode;
    public bool Fight = false;

    public float Speed = 1;

    private void OnEnable()
    {
        GridMenager GM = GameObject.Find("Plansza").GetComponent<GridMenager>();
        MyNode = GM.GetNodFromWolrdPoint(transform.position);
        Target = FindEnemy();
        NextNode = MyNode;
    }

    private void Update()
    {
        if (Target == this) { Target = FindEnemy(); }
        if(Vector3.Distance(transform.position+Vector3.down*transform.position.y, NextNode.transform.position) > Speed*2.0f*Time.deltaTime)
        {
            transform.Translate((NextNode.transform.position - transform.position).normalized * Time.deltaTime * Speed, Space.World);
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
            if(GM.GetWeight(GM.GetNodFromWolrdPoint(transform.position), GM.GetNodFromWolrdPoint(k.transform.position)) < MinDist)
            {
                MinDist = GM.GetWeight(GM.GetNodFromWolrdPoint(transform.position), GM.GetNodFromWolrdPoint(k.transform.position));
                SM = k.GetComponent<SlimeMovement>();
            }
        }
        return SM;
    }*/
}
