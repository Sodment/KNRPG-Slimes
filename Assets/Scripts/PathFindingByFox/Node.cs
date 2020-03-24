using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    int X;
    int Y;
    public bool Walkable = true;

    public float gCost;
    public float hCost;

    public SlimeMovement Unit;


    public float fCost
    {
        get
        {
            return gCost + hCost;
        }
    }

    public Node parent;

    public void SetCoordinate(int x, int y)
    {
        X = x;
        Y = y;
    }

    public int GetX()
    {
        return X;
    }

    public int GetY()
    {
        return Y;
    }
}
