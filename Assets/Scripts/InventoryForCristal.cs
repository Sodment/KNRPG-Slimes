using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryForCristal : MonoBehaviour
{
    DragableObject DragObject;
    GameObject DropZone;
    CrystalContainer Container;

    Transform Pointer;
    bool DropZoneIsUI;

    public GameObject[] Crystals;

    private void Start()
    {
        Pointer = new GameObject().transform;
        foreach (CrystalContainer k in transform.GetComponentsInChildren<CrystalContainer>())
        {
            k.ContainObject = (GameObject)Instantiate(Crystals[Random.Range(0, Crystals.Length)]);
            k.ContainObject.GetComponent<DragableObject>().Parent = k.transform;
            k.ContainObject.GetComponent<DragableObject>().LastParent = k.transform;
            k.ContainObject.GetComponent<DragableObject>().InUI = true;
            k.ContainObject.transform.parent = Camera.main.transform;
            k.ContainObject.transform.position = Camera.main.ScreenToWorldPoint(k.transform.position) + Camera.main.transform.forward;
        }
    }

    public void ReRoll()
    {
        foreach (CrystalContainer k in transform.GetComponentsInChildren<CrystalContainer>())
        {
            Destroy(k.ContainObject);
            k.ContainObject = (GameObject)Instantiate(Crystals[Random.Range(0, Crystals.Length)]);
            k.ContainObject.GetComponent<DragableObject>().Parent = k.transform;
            k.ContainObject.GetComponent<DragableObject>().LastParent = k.transform;
            k.ContainObject.GetComponent<DragableObject>().InUI = true;
            k.ContainObject.transform.parent = Camera.main.transform;
            k.ContainObject.transform.position = Camera.main.ScreenToWorldPoint(k.transform.position) + Camera.main.transform.forward;
        }
    }

    private void Update()
    {
        if (DragObject != null)
        {
            if (DropZone != null)
            {
                Debug.Log(DropZone.name);
            }
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit HitInfo;
            if (Physics.Raycast(ray, out HitInfo))
            {
                Pointer.position = HitInfo.point - Vector3.up * 0.5f;
                if (HitInfo.collider.gameObject != DropZone && !DropZoneIsUI)
                {
                    DownLight(DropZone);
                    HighLight(HitInfo.collider.gameObject);
                }
            }
            if (Input.GetMouseButtonUp(0))
            {
                StopDrag();
            }
        }
    }

    public void StartDrag(CrystalContainer BC)
    {
        if (!BC.ContainObject) { return; }
        BC.ContainObject.GetComponent<DragableObject>().enabled = true;
        DragObject = BC.ContainObject.GetComponent<DragableObject>();
        BC.ContainObject = null;
        DragObject.gameObject.layer = 2;
        DragObject.InUI = false;
        DragObject.Parent = Pointer;
    }

    public void StartDrag(GameObject GO)
    {
        GO.GetComponent<DragableObject>().enabled = true;
        DragObject = GO.GetComponent<DragableObject>();
        DragObject.InUI = false;
        DragObject.Parent = Pointer;
        GO.layer = 2;
    }

    public void SetDropZone(GameObject DZ)
    {
        if (DZ.GetComponent<CrystalContainer>().ContainObject != null) { return; }
        if (DropZone != null) { DownLight(DropZone); }
        DropZone = DZ;
        DropZoneIsUI = true;
    }

    public void RemoveDropZone(GameObject DZ)
    {
        DropZone = null;
        DropZoneIsUI = false;
    }

    void StopDrag()
    {
        DragObject.gameObject.layer = 0;

        if (DropZone != null)
        {
            DragObject.transform.parent = DropZone.transform;

            if (DropZone.GetComponent<CrystalContainer>()) //Odkładanie do EQ
            {
                DragObject.InUI = true;
                DragObject.Parent = DropZone.transform;
                DragObject.LastParent = DragObject.Parent;
                DragObject.transform.parent = Camera.main.transform;
                DropZone.GetComponent<CrystalContainer>().ContainObject = DragObject.gameObject;
                DownLight(DropZone);
            }
            else if (DropZone.GetComponent<SlimeLevels>())// Odkładanie na planszę
            {

                DragObject.InUI = false;
                DragObject.Parent = DropZone.transform;
                DragObject.LastParent = DragObject.Parent;
                DownLight(DropZone);
            }
            else { Odstaw(); }

        }
        else // Odkładanie na ostatnią pozycję
        {
            Odstaw();
        }

        DragObject = null;
        DropZone = null;
    }

    void Odstaw()
    {
        DropZone = DragObject.LastParent.gameObject;
        DragObject.transform.parent = DropZone.transform;

        if (DropZone.GetComponent<CrystalContainer>()) //Odkładanie do EQ
        {
            DragObject.InUI = true;
            DragObject.transform.parent = Camera.main.transform;
            DragObject.Parent = DropZone.transform;
            DragObject.LastParent = DragObject.Parent;
            DropZone.GetComponent<CrystalContainer>().ContainObject = DragObject.gameObject;
        }
        if (DropZone.GetComponent<SlimeLevels>()) // Odkładanie na planszę
        {
            DragObject.InUI = false;
            DragObject.Parent = DropZone.transform;
            DragObject.LastParent = DragObject.Parent;
        }
    }

    void HighLight(GameObject k)
    {
        if (k.GetComponent<Renderer>() != null && k.GetComponent<SlimeLevels>()!=null)
        {
            k.GetComponent<Renderer>().material.color *= 1.2f;
            DropZone = k;
        }
        else { DropZone = null; }
    }

    void DownLight(GameObject k)
    {
        if (!k) { return; }
        if (k.GetComponent<Renderer>() != null && k.GetComponent<SlimeLevels>() != null)
        {
            k.GetComponent<Renderer>().material.color *= (1.0f / 1.2f);
        }
    }
}
