using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IventoryV5 : MonoBehaviour
{
    Transform DragObject;
    public int PlayerID;
    public GameObject SlimePrefab;
    public GameObject[] Crystals;
    List<GameObject> MyItems = new List<GameObject>();

    GameObject Highlighted;

    private void Awake()
    {
        foreach(Transform k in transform)
        {
            if (k.name == "ReRoll") { continue; }
            GameObject GO;
            if (k.name == "UnitSlot")
            {
                GO = (GameObject)Instantiate(SlimePrefab, k.position, k.rotation);
                GO.GetComponent<SlimeBehaviour>().PlayerID = PlayerID;
            }
            else { GO = (GameObject)Instantiate(Crystals[Random.Range(0,Crystals.Length)], k.position, k.rotation); }
            MyItems.Add(GO);
            GO.transform.parent = k;
            GO.transform.localScale = Vector3.one;

        }
    }
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit Hit;
        if(Physics.Raycast(ray, out Hit))
        {
            if(DragObject==null && Input.GetMouseButtonDown(0))
            {
                if(MyItems.Contains(Hit.collider.gameObject))
                {
                    DragObject = Hit.collider.transform;
                    DragObject.GetComponent<Collider>().enabled = false;
                }

                if (Hit.collider.transform.childCount>0 && MyItems.Contains(Hit.collider.transform.GetChild(0).gameObject))
                {
                    DragObject = Hit.collider.transform.GetChild(0);
                    DragObject.GetComponent<Collider>().enabled = false;
                }
            }

            if (DragObject != null)
            {
                DragObject.position = Hit.point + Hit.collider.transform.up * 0.5f;

                if (Input.GetMouseButtonUp(0))
                {
                    PutIt(Hit.collider.gameObject);
                }
            }

            if (Hit.collider.gameObject != Highlighted)
            {
                Highlight(Hit.collider.gameObject);
            }
        }
        else
        {
            if (DragObject != null)
            {
                Vector3 Point = Camera.main.transform.position + ray.direction * (-1.0f*((Camera.main.transform.position.y+0.5f) / ray.direction.y));
                DragObject.position = Point + Vector3.up * 0.5f;

                if (Input.GetMouseButtonUp(0))
                {
                    DragObject.GetComponent<Collider>().enabled = true;
                    DragObject = null;
                }
            }

            Highlight(null);
        }
    }

    void PutIt(GameObject DropZone)
    {
        if (DragObject.tag == "Slime")
        {
            if (MyItems.Contains(DropZone) && DropZone.tag!="Crystal")
            {
                Transform tmp = DragObject.parent;
                DragObject.parent = DropZone.transform.parent;
                DropZone.transform.parent = tmp;
            }

            if (DropZone.transform.childCount > 0 && MyItems.Contains(DropZone.transform.GetChild(0).gameObject) && DropZone.transform.GetChild(0).tag!="Crystal")
            {
                DropZone.transform.GetChild(0).parent = DragObject.parent;
                DragObject.parent = DropZone.transform;
            }

            if (DropZone.name == "Node(Clone)" || DropZone.name == "UnitSlot")
            {
                DragObject.parent = DropZone.transform;
            }

            DragObject.GetComponent<Collider>().enabled = true;
            DragObject = null;
        }
        else if (DragObject.tag == "Crystal")
        {
            if (MyItems.Contains(DropZone))
            {
                if (DropZone.tag != "Slime")
                {
                    Transform tmp = DragObject.parent;
                    DragObject.parent = DropZone.transform.parent;
                    DropZone.transform.parent = tmp;
                }
                else
                {
                    DropZone.GetComponent<SlimeLevelsV2>().AddCrystal(DragObject.GetComponent<Crystal>());
                }
            }

            if (DropZone.transform.childCount > 0 && MyItems.Contains(DropZone.transform.GetChild(0).gameObject))
            {
                if (DropZone.transform.GetChild(0).tag == "Crystal")
                {
                    DropZone.transform.GetChild(0).parent = DragObject.parent;
                    DragObject.parent = DropZone.transform;
                }
                else
                {
                    DropZone.transform.GetChild(0).GetComponent<SlimeLevelsV2>().AddCrystal(DragObject.GetComponent<Crystal>());
                }
            }
            DragObject.GetComponent<Collider>().enabled = true;
            DragObject = null;
        }
    }

    void Highlight(GameObject Object)
    {
        if (Highlighted != null && Highlighted.GetComponent<Renderer>())
        { Highlighted.GetComponent<Renderer>().material.color -= Color.white*0.2f; }
        Highlighted = Object;
        if (Highlighted != null && Highlighted.GetComponent<Renderer>())
        { Highlighted.GetComponent<Renderer>().material.color += Color.white * 0.2f; }
    }


    public void FastPut()
    {
        if (DragObject != null)
        { 
            DragObject.position = DragObject.transform.parent.position;
            DragObject.GetComponent<Collider>().enabled = true;
        }
        DragObject = null;
        Highlight(null);
    }

    public void ReRoll()
    {
        foreach (Transform k in transform)
        {
            if (k.name == "UnitSlot" || k.name=="ReRoll") { continue;  }
            else if(k.childCount>0) { Destroy(k.GetChild(0).gameObject); }
            GameObject GO = (GameObject)Instantiate(Crystals[Random.Range(0, Crystals.Length)], k.position, k.rotation); 
            MyItems.Add(GO);
            GO.transform.parent = k;
            GO.transform.localScale = Vector3.one;

        }
    }
}
