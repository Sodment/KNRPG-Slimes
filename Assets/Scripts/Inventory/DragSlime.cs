using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragSlime : MonoBehaviour
{
    int Owner = 0; //1 gracz 1; 2 gracz 2
    GameObject Parent; //Miejsce w którym slime ma zostać odłożony
    public GameObject LastParent; //Miejsce z którego slime został zabrany

    public bool Drag = false;


    private void Update()
    {
        if (Drag)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit HitInfo;
            if(Parent==null)
            {
                if (Physics.Raycast(ray, out HitInfo))
                {
                    if (HitInfo.collider.gameObject.name != "Plansza")
                    {
                        Parent = HitInfo.collider.gameObject;
                    }
                }
                else { Parent = LastParent; }
            }
            else if (!Parent.GetComponent<ButtonContainer>())
            {
                if (Physics.Raycast(ray, out HitInfo))
                {
                    if (HitInfo.collider.gameObject.name != "Plansza")
                    {
                        Parent = HitInfo.collider.gameObject;
                    }
                }
                else { Parent = LastParent; }
            }
            Physics.Raycast(ray, out HitInfo);
            transform.position = HitInfo.point + Vector3.up * 0.5f;
            if (Input.GetMouseButtonUp(0)) 
            {
                Drag = false;
                PutIt();
            }
        }
        else
        {
            if (LastParent.GetComponent<ButtonContainer>() == null)
            {
                if (Vector3.Distance(LastParent.transform.position, transform.position) > 0.1f)
                {
                    transform.Translate((LastParent.transform.position - transform.position) * Time.deltaTime, Space.World);
                }
                else { transform.position = LastParent.transform.position; }
            }
            else
            {
                if (Vector3.Distance(Camera.main.ScreenToWorldPoint(LastParent.transform.position), transform.position) > 0.1f)
                {
                    transform.Translate((Camera.main.ScreenToWorldPoint(LastParent.transform.position)+Camera.main.transform.forward - transform.position) * Time.deltaTime, Space.World);
                }
            }
        }
    }

    void PutIt()
    {
        if (Parent == null)
        {
            Parent = LastParent;
        }
        //Odkładamy slimea na innego slimea (podmianka)
        if (Parent.GetComponent<DragSlime>() != null)
        {
            if (Parent.GetComponent<DragSlime>().Owner != Owner)
            {
                Parent = LastParent; PutIt(); return;
            }
            GameObject tmp = Parent.GetComponent<DragSlime>().LastParent;
            Parent.GetComponent<DragSlime>().LastParent = LastParent;
            LastParent = tmp;

            //Aktualnie przemieszczany slime
            if (LastParent.GetComponent<ButtonContainer>())
            {
                transform.parent = Camera.main.transform.GetChild(Owner - 1);
                LastParent.GetComponent<ButtonContainer>().ContainObject = gameObject;
                LastParent.GetComponent<ButtonContainer>().RefreshData();
                GetComponent<Collider>().enabled = false;
            }
            if (LastParent.GetComponent<Node>())
            {
                transform.parent = LastParent.transform.parent;
                LastParent.GetComponent<Node>().Unit = GetComponent<SlimeMovement>();
                GetComponent<Collider>().enabled = true;
            }

            //Slime ustępujący miejsca
            tmp = Parent.GetComponent<DragSlime>().LastParent;
            if (tmp.GetComponent<ButtonContainer>())
            {
                Parent.transform.parent = Camera.main.transform.GetChild(Owner - 1);
                tmp.GetComponent<ButtonContainer>().ContainObject = Parent;
                tmp.GetComponent<ButtonContainer>().RefreshData();
                Parent.GetComponent<Collider>().enabled = false;
            }
            if (tmp.GetComponent<Node>())
            {
                Parent.transform.parent = tmp.transform.parent;
                tmp.GetComponent<Node>().Unit = Parent.GetComponent<SlimeMovement>();
                Parent.GetComponent<Collider>().enabled = true;
            }
        }

        // Odkładamy Slimea na przycisk (Wolny lub zajęty)
        if (Parent.GetComponent<ButtonContainer>())
        {
            if (Parent.GetComponent<ButtonContainer>().ContainObject == null)
            {
                transform.parent = Camera.main.transform.GetChild(Owner - 1);
                Parent.GetComponent<ButtonContainer>().ContainObject = gameObject;
                Parent.GetComponent<ButtonContainer>().RefreshData();
                GetComponent<Collider>().enabled = false;
            }
            else
            {
                transform.parent = Camera.main.transform.GetChild(Owner - 1);
                GameObject tmp = Parent.GetComponent<ButtonContainer>().ContainObject;
                Parent.GetComponent<ButtonContainer>().ContainObject = gameObject;
                Parent.GetComponent<ButtonContainer>().RefreshData();
                tmp.GetComponent<DragSlime>().LastParent = LastParent;
                GetComponent<Collider>().enabled = false;
                if (LastParent.GetComponent<Node>())
                {
                    tmp.transform.parent = LastParent.transform.parent;
                    LastParent.GetComponent<Node>().Unit = tmp.GetComponent<SlimeMovement>();
                    tmp.GetComponent<Collider>().enabled = true;
                }
                else if(LastParent.GetComponent<ButtonContainer>())
                {
                    tmp.transform.parent = Camera.main.transform.GetChild(Owner - 1);
                    LastParent.GetComponent<ButtonContainer>().ContainObject = tmp;
                    LastParent.GetComponent<ButtonContainer>().RefreshData();
                    tmp.GetComponent<Collider>().enabled = false;
                }
            }
            LastParent = Parent;
        }

        // Odkładamy slimea na pole (wolne lub zajęte)
        if (Parent.GetComponent<Node>())
        {
            if (Parent.GetComponent<Node>().Unit != null)
            {
                if (Parent.GetComponent<Node>().Unit.PlayerID != Owner)
                {
                    Parent = LastParent; PutIt(); return;
                }
                GameObject tmp = Parent.GetComponent<Node>().Unit.gameObject;
                tmp.GetComponent<DragSlime>().LastParent = LastParent;
                if (LastParent.GetComponent<Node>())
                {
                    tmp.transform.parent = LastParent.transform.parent;
                    LastParent.GetComponent<Node>().Unit = tmp.GetComponent<SlimeMovement>();
                    tmp.GetComponent<Collider>().enabled = true;
                }
                else if (LastParent.GetComponent<ButtonContainer>())
                {
                    tmp.transform.parent = Camera.main.transform.GetChild(Owner - 1);
                    LastParent.GetComponent<ButtonContainer>().ContainObject = tmp;
                    LastParent.GetComponent<ButtonContainer>().RefreshData();
                    tmp.GetComponent<Collider>().enabled = false;
                }
                LastParent = Parent;
                transform.parent = LastParent.transform.parent;
                GetComponent<Collider>().enabled = true;
            }
            else
            {
                LastParent = Parent;
                transform.parent = LastParent.transform.parent;
                GetComponent<Collider>().enabled = true;
            }
        }
    }

    private void OnMouseDown()
    {
        if (Owner == GameObject.FindObjectOfType<InventoryV4>().PlayerIndex)
        {
            Drag = true;
            GetComponent<Collider>().enabled = false;
            GameObject.FindObjectOfType<DragMenager>().DragObject = gameObject;
        }
    }

    public void SetOwner(int i)
    {
        Owner = i;
    }

    public int GetOwner()
    {
        return Owner;
    }

    public void SetTMPParent(GameObject _Parent)
    {
        Parent = _Parent;
    }
}
