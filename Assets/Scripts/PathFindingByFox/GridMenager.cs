using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridMenager : MonoBehaviour
{
    public Node[,] GridNodes = new Node[8, 8];
    public GameObject NodePref;
    private void Awake()
    {
        for (int x = -4; x < 4; x++)
        {
            for (int y = -4; y < 4; y++)
            {
                GameObject GO = (GameObject)Instantiate(NodePref, new Vector3(x + 0.5f, 0.0f, y + 0.5f), Quaternion.identity);
                GO.transform.parent = this.transform;
                GridNodes[x + 4, y + 4] = GO.GetComponent<Node>();
                GridNodes[x + 4, y + 4].SetCoordinate(x + 4, y + 4);
            }
        }

    }

    public Node GetNodFromWolrdPoint(Vector3 Position)
    {
        int X = Mathf.RoundToInt(Position.x + 3.5f);
        int Y = Mathf.RoundToInt(Position.z + 3.5f);
        return GetNodeByIndex(X, Y); //Dodatkowe zabezpieczenie
    }

    public Node GetNodeByIndex(int X, int Y)
    {
        if (X < 0) { X = 0; }
        if (Y < 0) { Y = 0; }
        if(X>=GridNodes.GetLength(0)) { X = GridNodes.GetLength(0) - 1; }
        if (Y >= GridNodes.GetLength(1)) { Y = GridNodes.GetLength(1) - 1; }
        return GridNodes[X, Y];
    }


    public Node[] GetNeightbour(int X, int Y)
    {
        List<Node> Neightbours = new List<Node>();
        Node tmp = GetNodeByIndex(X, Y - 1);
        if (!Neightbours.Contains(tmp)) { Neightbours.Add(tmp); }
        tmp = GetNodeByIndex(X, Y + 1);
        if (!Neightbours.Contains(tmp)) { Neightbours.Add(tmp); }
        tmp = GetNodeByIndex(X - 1, Y);
        if (!Neightbours.Contains(tmp)) { Neightbours.Add(tmp); }
        tmp = GetNodeByIndex(X + 1, Y);
        if (!Neightbours.Contains(tmp)) { Neightbours.Add(tmp); }
        
        tmp = GetNodeByIndex(X, Y);
        if (Neightbours.Contains(tmp)) { Neightbours.Remove(tmp); }

        return Neightbours.ToArray();
    }

    Node GetClosestNode(Node MyNode, Node TargetNode)
    {
        float Weight = float.MaxValue;
        Node Target = TargetNode;
        foreach(Node k in GetNeightbour(TargetNode.GetX(), TargetNode.GetY()))
        {
            if(!k.Walkable || k.Unit!=null) { continue; }
            float currWeight = GetWeight(MyNode, k);
            if (currWeight < Weight)
            {
                Weight = currWeight;
                Target = k;
            }
        }
        return Target;
    }

    float GetWeight(Node A, Node B)
    {
        return Mathf.Abs(A.GetX() - B.GetX()) + Mathf.Abs(A.GetY() - B.GetY()) + Vector3.Distance(A.transform.position,B.transform.position);
    }

    public Node GetNextNode(Node StartNode, Node TargetNode)
    {
        // ... A oto i A*
        List<Node> openSet = new List<Node>();
        HashSet<Node> closedSet = new HashSet<Node>();
        openSet.Add(StartNode);
        TargetNode = GetClosestNode(StartNode, TargetNode);

        while (openSet.Count > 0)
        {
            Node node = openSet[0];
            for (int i = 1; i < openSet.Count; i++)
            {
                if (openSet[i].fCost < node.fCost || openSet[i].fCost == node.fCost)
                {
                    if (openSet[i].hCost < node.hCost)
                        node = openSet[i];
                }
            }

            openSet.Remove(node);
            closedSet.Add(node);

            if (node == TargetNode)
            {
                return RetracePath(StartNode, TargetNode);
            }

            foreach (Node neighbour in GetNeightbour(node.GetX(), node.GetY()))
            {
                if (!neighbour.Walkable || closedSet.Contains(neighbour))
                {
                    continue;
                }

                float newCostToNeighbour = node.gCost + GetWeight(node, neighbour);
                if (newCostToNeighbour < neighbour.gCost || !openSet.Contains(neighbour))
                {
                    neighbour.gCost = newCostToNeighbour;
                    neighbour.hCost = GetWeight(neighbour, TargetNode);
                    neighbour.parent = node;

                    if (!openSet.Contains(neighbour))
                        openSet.Add(neighbour);
                }
            }
        }

        Debug.Log("Something went wrong");
        return StartNode;
    }

    Node RetracePath(Node startNode, Node endNode)
    {
        List<Node> path = new List<Node>();
        Node currentNode = endNode;

        while (currentNode != startNode)
        {
            path.Add(currentNode);
            currentNode = currentNode.parent;
        }
        path.Reverse();

        if(path.Count>0)
        return path[0];
        else return startNode;

    }

    public SlimeMovement NearEnemy(Node MyNode, int PlayerIndex)
    {
        foreach(Node k in GetNeightbour(MyNode.GetX(), MyNode.GetY()))
        {
            if (k.Unit != null)
            {
                if (k.Unit.PlayerID != PlayerIndex) { k.Unit.Stop(); return k.Unit; }
            }
        }
        return null;
    }
}
