using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeMovement : MonoBehaviour
{
    public int PlayerID; //Później będzie zależne od photonview
    int MyIndex;
    public Transform Target; //Później będzie szukał automatycznie
    int TargeIndex;
    Node MyNode;
    Node NextNode;

    GridMenager GM;
    bool Fight = false;


    private void Start()
    {
        GM = GameObject.Find("Plansza").GetComponent<GridMenager>();
        MyNode = GM.GetNodFromWolrdPoint(transform.position);
        FindEnemy();
    }

    void FindEnemy()
    {
        //A tu szukanie przeciwnika
        NextNode = GM.GetNextNode(MyNode, GM.GetNodFromWolrdPoint(Target.position));
    }

    private void Update()
    {
        if(Vector3.Distance(transform.position+new Vector3(0, -transform.position.y, 0), NextNode.transform.position) > 0.05f)
        {
            transform.Translate((NextNode.transform.position - MyNode.transform.position).normalized * Time.deltaTime, Space.World);
        }
        else if (MyNode != GM.GetNodFromWolrdPoint(transform.position) || (MyNode==NextNode && !Fight))
        {
            CalculateNextMove();
        }
    }

    public void CalculateNextMove()
    {
        MyNode.Walkable = true;
        MyNode.Unit = null;
        MyNode = GM.GetNodFromWolrdPoint(transform.position);
        MyNode.Walkable = false; //Co prawda GM to zrobi, ale nie zaszkodzi
        MyNode.Unit = this;
        SlimeMovement Enemy = GM.NearEnemy(MyNode, PlayerID);
        transform.position = new Vector3(MyNode.transform.position.x, transform.position.y, MyNode.transform.position.z);
        if (Enemy == null)
        {
            NextNode = GM.GetNextNode(MyNode, GM.GetNodFromWolrdPoint(Target.position));
            NextNode.Walkable = false;
        }
        else { Target = Enemy.transform; Stop(); }
    }

    public void Stop()
    {
        Fight = true;
        NextNode.Walkable = true;
        NextNode = MyNode;
        transform.position = new Vector3(MyNode.transform.position.x, transform.position.y, MyNode.transform.position.z);
        MyNode.Walkable = false;
    }

}
