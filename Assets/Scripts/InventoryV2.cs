using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.IO;

public class InventoryV2 : MonoBehaviour
{
    public  Transform[] Slots;
    public  GameObject[] Gems;
    public  Transform World;
    private List<GameObject> CurrentInventory = new List<GameObject>();
    private List<GameObject> Dropped = new List<GameObject>();
    private GameObject CurrentDragged;
    private GameObject DropZone;
    private PhotonView MyView;

    public void ReRoll()
    {
        foreach(GameObject k in Dropped)
        {
            MyView.RPC("RPC_InstantiateObject", RpcTarget.All, k.name, k.transform.position, k.transform.rotation.eulerAngles);
        }
        Dropped.Clear();
        foreach(GameObject k in CurrentInventory)
        {
            Destroy(k);
        }
        CurrentInventory.Clear();

        foreach(Transform k in Slots)
        {
            GameObject GO = (GameObject)Instantiate(Gems[Random.Range(0, Gems.Length)], Camera.main.ScreenToWorldPoint(k.position)+Vector3.down*50.0f, k.rotation);
            GO.GetComponent<DragableObject>().TargetPos = Camera.main.ScreenToWorldPoint(k.position) + Camera.main.transform.forward;
            GO.transform.parent = Camera.main.transform;
            CurrentInventory.Add(GO);
        }

    }

    public void StartDrag(int Slot)
    {
        CurrentInventory[Slot].transform.parent = World;
        CurrentDragged = CurrentInventory[Slot];
    }

    private void Update()
    {
        if (CurrentDragged != null)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit HitInfo;
            Physics.Raycast(ray, out HitInfo);
           // if (!Physics.Raycast(ray, out HitInfo)) { Debug.DrawRay(ray.origin, ray.direction, Color.red); Debug.Break(); }
            CurrentDragged.GetComponent<DragableObject>().TargetPos = HitInfo.point+Vector3.up*0.5f;
            
            if (HitInfo.collider.gameObject != DropZone || DropZone==null)
            {
                DownLight();
                DropZone = HitInfo.collider.gameObject;
                HighLight();
            }

            if (Input.GetMouseButtonUp(0))
            {
                StopDrag();
            }
        }
        foreach(GameObject k in CurrentInventory)
        {
            if (CurrentDragged == k) { continue; }
            if (Dropped.Contains(k)) { continue; }
            Vector3 Pos = Camera.main.ScreenToWorldPoint(Slots[CurrentInventory.IndexOf(k)].position) + Camera.main.transform.forward;
            k.GetComponent<DragableObject>().TargetPos = Pos;
            if (Vector3.Distance(k.transform.position, k.GetComponent<DragableObject>().TargetPos) <= 0.01f)
            {
                k.transform.position = Pos;
            }
        }
    }

    void StopDrag()
    {
        if (DropZone != null)
        {
            CurrentDragged.GetComponent<DragableObject>().TargetPos = DropZone.transform.position + Vector3.up;
            DownLight();
            DropZone = null;
            Dropped.Add(CurrentDragged);
            CurrentDragged.transform.parent = World;
            CurrentDragged = null;
        }
        else
        {
            DownLight();
            CurrentDragged.GetComponent<DragableObject>().TargetPos = Camera.main.ScreenToWorldPoint(Slots[CurrentInventory.IndexOf(CurrentDragged)].position) + Camera.main.transform.forward;
            CurrentDragged.transform.parent = Slots[CurrentInventory.IndexOf(CurrentDragged)];
            if (Dropped.Contains(CurrentDragged)) { Dropped.Remove(CurrentDragged); }
            CurrentDragged.transform.parent = Camera.main.transform;
            CurrentDragged = null;
            DropZone = null;
        }
    }

    void HighLight()
    {
        if(DropZone==null) { return; }
        if (DropZone.GetComponent<Renderer>() != null)
        {
            DropZone.GetComponent<Renderer>().material.color *= 1.2f;
        }
        else { DropZone = null; }
    }

    void DownLight()
    {
        if (DropZone == null) { return; }
        if (DropZone.GetComponent<Renderer>() != null)
        {
            DropZone.GetComponent<Renderer>().material.color *= (1.0f/1.2f);
        }
    }

    public void SetView(PhotonView _View)
    {
        if (MyView == null)
        {
            MyView = _View;
            ReRoll();
        }
    }
}
