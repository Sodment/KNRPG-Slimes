using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public Transform[] Slots;
    public GameObject[] Items;

    List<GameObject> Current;
    List<GameObject> CurrentDrop;
    List<GameObject> Droped;

    private void Start()
    {
        Current = new List<GameObject>();
        CurrentDrop = new List<GameObject>();
        Droped = new List<GameObject>();
        ReRoll();
    }

    public void ReRoll()
    {
        foreach(GameObject k in Droped)
        {
            k.GetComponent<DragableObject>().Start();
        }
        Droped.Clear();
        foreach(GameObject k in CurrentDrop)
        {
            Destroy(k);
        }
        CurrentDrop.Clear();
        foreach(Transform k in Slots)
        {
            GameObject GO = (GameObject)Instantiate(Items[Random.Range(0, Items.Length)], k.position + Vector3.up, Quaternion.identity);
            CurrentDrop.Add(GO);
            GO.GetComponent<DragableObject>().SetOwner(this);
        }
        Current = CurrentDrop;
    }

    public void RemoveObject(GameObject k )
    {
        CurrentDrop.Remove(k);
        if (Droped.Contains(k)) { return; }
        Droped.Add(k);
    }

    public void AddObject(GameObject k)
    {
        if (CurrentDrop.Contains(k)) { return; }
        if (Current.Contains(k))
        {
            CurrentDrop.Add(k);
            Droped.Remove(k);
        }
    }
}
