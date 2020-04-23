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
    List<GameObject> MyNodes = new List<GameObject>();

    GameObject Highlighted;

    private void Start()
    {
        int i = 0;
        string PlayerName = "Player"+PlayerID.ToString()+"_Unit";

        foreach(Transform k in transform)
        {
            GameObject GO;
            if (k.name == "UnitSlot")
            {
                GO = (GameObject)Instantiate(SlimePrefab, k.position, k.rotation);
                // GO.GetComponent<SlimeBehaviour>().PlayerID = PlayerID;
                // GO.GetComponent<SlimeBehaviour>().UnitID = PlayerName + i.ToString();
                GO.GetComponent<SlimeBahaviourV2>().PlayerID = PlayerID;
                i++;
            }
            else { GO = (GameObject)Instantiate(Crystals[Random.Range(0,Crystals.Length)], k.position, k.rotation); }
            MyItems.Add(GO);
            GO.transform.parent = k;
            GO.transform.localScale = Vector3.one;
        }

        foreach(Transform k in GameObject.Find("Plansza").transform)
        {
            if((PlayerID==1 && k.position.z<-2.0f)||(PlayerID!=1 && k.position.z > 2.0f))
            {
                MyNodes.Add(k.gameObject);
            }
        }

        OnEnable();
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


            if (DropZone.transform.childCount > 0 && MyItems.Contains(DropZone.transform.GetChild(0).gameObject) && DropZone.transform.GetChild(0).tag != "Crystal")
            {
                DropZone.transform.GetChild(0).parent = DragObject.parent;
                DragObject.parent = DropZone.transform;
            }

            if (MyNodes.Contains(DropZone))
            {
                DragObject.parent = DropZone.transform;
            }

            if (DropZone.name == "UnitSlot")
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
                    if (GetComponentInParent<InventoryNetworkingAssistance>() != null && DropZone.transform.parent.name!="UnitSlot")
                    {
                        GetComponentInParent<InventoryNetworkingAssistance>().PutCrystal(DropZone.GetComponent<SlimeBehaviour>().UnitID, DragObject.GetComponent<Crystal>().Type);
                    }
                    Destroy(DragObject.gameObject);
                }
            }

            if (DropZone.transform.childCount > 0 && MyItems.Contains(DropZone.transform.GetChild(0).gameObject) && DropZone.name != "King")
            {
                if (DropZone.transform.GetChild(0).tag == "Crystal")
                {
                    DropZone.transform.GetChild(0).parent = DragObject.parent;
                    DragObject.parent = DropZone.transform;
                }
                else
                {
                    DropZone.transform.GetChild(0).GetComponent<SlimeLevelsV2>().AddCrystal(DragObject.GetComponent<Crystal>());
                    if (GetComponentInParent<InventoryNetworkingAssistance>() != null && DropZone.name!="UnitSlot")
                    {
                        GetComponentInParent<InventoryNetworkingAssistance>().PutCrystal(DropZone.transform.GetChild(0).GetComponent<SlimeBehaviour>().UnitID, DragObject.GetComponent<Crystal>().Type);
                    }
                    Destroy(DragObject.gameObject);
                }
            }

            if (DropZone.name == "King" && DropZone.GetComponent<MotherSlime>().Player==this)
            {
                DropZone.GetComponent<MotherSlime>().AddCrystal(DragObject.GetComponent<Crystal>());
            }

            if (DragObject != null)
            {
                DragObject.GetComponent<Collider>().enabled = true;
                DragObject = null;
            }
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
        if (this.enabled == false) { return; }
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

    private void OnEnable()
    {
        foreach(GameObject k in MyNodes)
        {
            int color = Mathf.RoundToInt(Mathf.Abs(k.transform.position.x + k.transform.position.z)) % 2;
            k.GetComponent<Renderer>().material.color += Color.white * 0.15f * ((color == 1) ? 1.0f : -1.0f);
        }
    }
    private void OnDisable()
    {
        foreach (GameObject k in MyNodes)
        {
            int color = Mathf.RoundToInt(Mathf.Abs(k.transform.position.x + k.transform.position.z)) % 2;
            k.GetComponent<Renderer>().material.color -= Color.white * 0.15f *((color==1)?1.0f:-1.0f);
        }
    }


    public void AddItem(GameObject Item)
    {
        Item.GetComponent<SlimeBehaviour>().UnitID = "Player" + PlayerID.ToString() + "_Unit" + MyItems.Count.ToString();
        MyItems.Add(Item);
    }
}
