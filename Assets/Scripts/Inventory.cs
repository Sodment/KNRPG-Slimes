using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Inventory : MonoBehaviour
{
    public Transform[] Slots;
    public GameObject[] Items;

    List<GameObject> Current;
    List<GameObject> Droped;

    PhotonView Owner;

    private void Start()
    {
        Owner = GetComponent<PhotonView>();
        Current = new List<GameObject>();
        Droped = new List<GameObject>();
        ReRoll();
    }

    private void ReRoll()
    {
        foreach(GameObject k in Droped)
        {
            //k.GetComponent<DragableObject>().Start();
            k.GetComponent<InstantiateObjects>().InstantiateObject(k);
        }

        foreach(GameObject k in Current)
        {
            if (Droped.Contains(k)) { continue; }
            Debug.Log("Destroy");
            Destroy(k);
        }

        Current.Clear();
        Droped.Clear();
        foreach(Transform k in Slots)
        {
            GameObject GO = (GameObject)Instantiate(Items[Random.Range(0, Items.Length)], k.position + Vector3.up, Quaternion.identity);
            Current.Add(GO);
           // GO.GetComponent<DragableObject>().SetOwner(this);
            GO.GetComponent<InstantiateObjects>().SetOwner(Owner);
        }
    }

    public void RemoveObject(GameObject k )
    {
        if (Droped.Contains(k)) { return; }
        Droped.Add(k);
    }

    public void AddObject(GameObject k)
    {
        Droped.Remove(k);
    }
}
