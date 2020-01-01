using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class InventoryV3 : MonoBehaviour
{
    DragableObject DragObject;
    GameObject DropZone;
    ButtonContainer Container;

    Transform Pointer;
    bool DropZoneIsUI;

    private void Start()
    {
        Pointer = new GameObject().transform; 
        foreach(ButtonContainer k in transform.GetComponentsInChildren<ButtonContainer>())
        {
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
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit HitInfo;
            if (Physics.Raycast(ray, out HitInfo))
            {
                Pointer.position = HitInfo.point - Vector3.up*0.5f;
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

    public void StartDrag(ButtonContainer BC)
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
       // if (DZ.GetComponent<ButtonContainer>().ContainObject!=null) { return; }
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
            DownLight(DropZone);

            if (DropZone.GetComponent<ButtonContainer>())
            {
                if (!DropZone.GetComponent<ButtonContainer>().ContainObject)
                    PutInEq();
                else
                    SwapToEq(DropZone.GetComponent<ButtonContainer>().ContainObject);
            }
            else if (DropZone.GetComponent<Node>())
            {
                PutInArena();
            }
            else if (DropZone.GetComponent<SlimeLevels>())
            {
                SwapToArena(DropZone);
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

    void PutInEq()
    {
        DragObject.InUI = true;
        DragObject.Parent = DropZone.transform;
        DragObject.LastParent = DragObject.Parent;
        DragObject.transform.parent = Camera.main.transform;
        DropZone.GetComponent<ButtonContainer>().ContainObject = DragObject.gameObject;
    }

    void PutInArena()
    {
        DragObject.InUI = false;
        DragObject.Parent = DropZone.transform;
        DragObject.LastParent = DragObject.Parent;
    }

    void SwapToEq(GameObject SwapObject)
    {
        DragObject.InUI = true;
        Transform tmp = DragObject.LastParent;
        DragObject.Parent = DropZone.transform;
        DragObject.LastParent = DragObject.Parent;
        DragObject.transform.parent = Camera.main.transform;
        DropZone.GetComponent<ButtonContainer>().ContainObject = DragObject.gameObject;

        DragableObject SwapedObject = SwapObject.GetComponent<DragableObject>();
        SwapedObject.enabled = true;
        if (tmp.GetComponent<ButtonContainer>())
        {
            SwapedObject.InUI = true;
            SwapedObject.transform.parent = Camera.main.transform;
            tmp.GetComponent<ButtonContainer>().ContainObject = SwapObject;
        }
        else
        {
            SwapedObject.InUI = false;
            SwapedObject.transform.parent = tmp;
        }
        SwapedObject.Parent = tmp;
        SwapedObject.LastParent = tmp;
    }

    void SwapToArena(GameObject SwapObject)
    {
        DragObject.InUI = false;
        Transform tmp = DragObject.LastParent;
        DragObject.Parent = DropZone.GetComponent<DragableObject>().LastParent;
        DragObject.LastParent = DragObject.Parent;
        DragObject.transform.parent = DragObject.Parent;

        DragableObject SwapedObject = SwapObject.GetComponent<DragableObject>();
        SwapedObject.enabled = true;
        if (tmp.GetComponent<ButtonContainer>())
        {
            SwapedObject.InUI = true;
            SwapedObject.transform.parent = Camera.main.transform;
            tmp.GetComponent<ButtonContainer>().ContainObject = SwapObject;
        }
        else
        {
            SwapedObject.InUI = false;
            SwapedObject.transform.parent = tmp;
        }
        SwapedObject.Parent = tmp;
        SwapedObject.LastParent = tmp;
    }

    void Odstaw()
    {
        DropZone = DragObject.LastParent.gameObject;
        DragObject.transform.parent = DropZone.transform;

        if (DropZone.GetComponent<ButtonContainer>()) //Odkładanie do EQ
        {
            DragObject.InUI = true;
            DragObject.transform.parent = Camera.main.transform;
            DragObject.Parent = DropZone.transform;
            DragObject.LastParent = DragObject.Parent;
            DropZone.GetComponent<ButtonContainer>().ContainObject = DragObject.gameObject;
        }
        if (DropZone.GetComponent<Node>()) // Odkładanie na planszę
        {
            DragObject.InUI = false;
            DragObject.Parent = DropZone.transform;
            DragObject.LastParent = DragObject.Parent;
        }
    }

    void HighLight(GameObject k)
    {
        if (k.GetComponent<Renderer>() != null)
        {
            k.GetComponent<Renderer>().material.color *= 1.2f;
            DropZone = k;
        }
        else { DropZone = null; }
    }

    void DownLight(GameObject k)
    {
        if (!k) { return; }
        if (k.GetComponent<Renderer>() != null)
        {
            k.GetComponent<Renderer>().material.color *= (1.0f / 1.2f);
        }
    }
}
