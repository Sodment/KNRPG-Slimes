using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragableObject : MonoBehaviour
{
    bool Drag;
    GameObject DropZone;
    GameObject LastObject;
    Vector3 TargetPos;
    Vector3 DefaultPos;
    private Inventory Owner;

    public void Start()
    {
        DefaultPos = transform.position;
        TargetPos = DefaultPos;
    }
    private void OnMouseDown()
    {
        Drag = true;
        gameObject.layer = 2;
        Owner.AddObject(this.gameObject);
    }

    private void OnMouseUp()
    {
        Drag = false;
        gameObject.layer = 0;
        StopDrag();
    }

    private void Update()
    {
        if (Drag)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit HitInfo;
            if (Physics.Raycast(ray, out HitInfo))
            {
                if (HitInfo.transform.parent != null)
                {
                    if (HitInfo.transform.parent.name == "Plansza")
                    {
                        DropZone = HitInfo.collider.gameObject;
                    }
                    else { DropZone = null; }
                }
                else { DropZone = null; }

                if (DropZone != LastObject)
                {
                    if(DropZone!=null)
                    HighLight(DropZone);
                    if (LastObject != null)
                    { DownLight(LastObject); }
                    LastObject = DropZone;
                }
                transform.position = HitInfo.point + Vector3.up * 0.5f;
            }
        }
        else
        {
            if (Vector3.Distance(transform.position, TargetPos) > 0.01f)
            { transform.Translate((TargetPos - transform.position) * Time.deltaTime *5.0f, Space.World); }
            else transform.position = TargetPos;
        }
    }
    void StopDrag()
    {
        if (DropZone != null)
        {
            TargetPos = DropZone.transform.position + Vector3.up;
            DownLight(DropZone);
            Owner.RemoveObject(this.gameObject);
        }
        else
        {
            TargetPos = DefaultPos;
        }
        LastObject = null;
        DropZone = null;
    }

    void HighLight(GameObject GO)
    {
        Renderer Rend;
        if (GO.TryGetComponent<Renderer>(out Rend))
        { Rend.material.color *= 1.2f; }
    }

    void DownLight(GameObject GO)
    {
        Renderer Rend;
        if (GO.TryGetComponent<Renderer>(out Rend))
        { Rend.material.color /= 1.2f; }
    }

    public void SetOwner(Inventory k)
    {
        Owner = k;
    }
}

