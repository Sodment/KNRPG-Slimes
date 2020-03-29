using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IventoryV5 : MonoBehaviour
{
    Transform DragObject;
    public int PlayerID;
    public GameObject SlimePrefab;
    List<GameObject> MyItems = new List<GameObject>();

    private void Awake()
    {
        foreach(Transform k in transform)
        {
            GameObject GO = (GameObject)Instantiate(SlimePrefab, k.position, k.rotation);
            MyItems.Add(GO);
            GO.transform.parent = k;
            GO.transform.localScale = Vector3.one;
        }
    }
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit[] Hitinfo = Physics.RaycastAll(ray);
        if (DragObject == null && Input.GetMouseButtonDown(0))
        {
            foreach (RaycastHit k in Hitinfo)
            {
                if (MyItems.Contains(k.collider.gameObject))
                {
                    DragObject = k.collider.gameObject.transform;
                    DragObject.GetComponent<Collider>().enabled = false;
                    break;
                }
            }
        }

        if (DragObject != null)
        {
            DragObject.GetComponent<SmothPass>().enabled = false;
            if (Mathf.Abs(DragObject.lossyScale.x - 0.3f) > 0.01f)
            {
                DragObject.localScale += Vector3.one * ((DragObject.lossyScale.x < 0.3f) ? 1 : -1) * Time.deltaTime * 2.0f;
            }

            RaycastHit Hitinfotmp;
            Physics.Raycast(ray,out Hitinfotmp);
            DragObject.position = Hitinfotmp.point + Vector3.up * 0.5f;
            if (Input.GetMouseButtonUp(0))
            {
                DragObject.GetComponent<SmothPass>().enabled = true;
                PutIt(Hitinfo);
            }
        }
    }

    void PutIt(RaycastHit[] Info)
    {
        Sorted(ref Info);
        foreach(RaycastHit k in Info)
        {
            if (k.collider.gameObject.name == "Plansza") { continue; }

            if (MyItems.Contains(k.collider.gameObject))
            {
                Transform tmp = DragObject.transform.parent;
                DragObject.parent = k.collider.transform.parent;
                k.collider.transform.parent = tmp;
                DragObject.GetComponent<Collider>().enabled = true;
                DragObject = null;
                return;
            }

            if (k.collider.transform.childCount > 0)
            {
                if (MyItems.Contains(k.collider.transform.GetChild(0).gameObject))
                {
                    if (k.collider.transform == DragObject.parent) { continue; }
                    Transform tmp = DragObject.transform.parent;
                    DragObject.parent = k.collider.transform;
                    k.collider.transform.GetChild(0).parent = tmp;
                    DragObject.GetComponent<Collider>().enabled = true;
                    DragObject = null;
                    return;
                }
            }

            if(k.collider.gameObject.name== "Node(Clone)" || k.collider.gameObject.name== "UnitSlot")
            {
                if (k.collider.transform.childCount == 0)
                {
                    DragObject.parent = k.collider.transform;
                    DragObject.GetComponent<Collider>().enabled = true;
                    DragObject = null;
                    return;
                }
                else
                {
                    if (MyItems.Contains(k.collider.transform.GetChild(0).gameObject))
                    {
                        Transform tmp = DragObject.parent;
                        DragObject.parent = k.collider.transform;
                        k.collider.transform.GetChild(0).parent = tmp;
                        DragObject.GetComponent<Collider>().enabled = true;
                        DragObject = null;
                        return;
                    }
                }
            }
        }
       // DragObject.localPosition = Vector3.zero;
        DragObject.GetComponent<Collider>().enabled = true;
        DragObject = null;
    }

    void Sorted(ref RaycastHit[] Data)
    {
        List<RaycastHit> SortedData = new List<RaycastHit>();
        List<RaycastHit> UnSortedData = new List<RaycastHit>();
        UnSortedData.AddRange(Data);

        SortedData.AddRange(UnSortedData.FindAll(k => MyItems.Contains(k.collider.gameObject)));
        UnSortedData.RemoveAll(k => MyItems.Contains(k.collider.gameObject));
        SortedData.AddRange(UnSortedData.FindAll(k => k.collider.transform.childCount>0 && MyItems.Contains(k.collider.transform.GetChild(0).gameObject)));
        UnSortedData.RemoveAll(k => k.collider.transform.childCount>0 && MyItems.Contains(k.collider.transform.GetChild(0).gameObject));
        SortedData.AddRange(UnSortedData.FindAll(k => k.collider.gameObject.name == "Node(Clone)" || k.collider.gameObject.name == "UnitSlot"));
        UnSortedData.RemoveAll(k => k.collider.gameObject.name == "Node(Clone)" || k.collider.gameObject.name == "UnitSlot");
        SortedData.AddRange(UnSortedData);

        Data = SortedData.ToArray();
    }
}
