using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragCrystal : MonoBehaviour
{
    public bool Drag;
    GameObject Parent;
    public GameObject LastParent;
    
    void Update()
    {
        if (Drag)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit HitInfo;
            if (Physics.Raycast(ray, out HitInfo))
            {
                transform.position = HitInfo.point + Vector3.up * 0.5f;
                if (HitInfo.collider.gameObject.GetComponent<SlimeLevels>()) { Parent = HitInfo.collider.gameObject; }
            }
            if (Input.GetMouseButtonUp(0))
            {
                Drag = false;
                GameObject.FindObjectOfType<DragMenager>().DragObject = null;
                PutIt();
            }
        }

        if (Vector3.Distance(Camera.main.ScreenToWorldPoint(LastParent.transform.position), transform.position) > 0.1f)
        {
            transform.Translate((Camera.main.ScreenToWorldPoint(LastParent.transform.position) + Camera.main.transform.forward - transform.position) * Time.deltaTime, Space.World);
        }
    }

    void PutIt()
    {
        if (Parent == null) { Parent = LastParent; }

        if (Parent.GetComponent<SlimeLevels>())
        {
            Parent.GetComponent<SlimeLevels>().AddCrystal(GetComponent<Crystal>().Type);
            Destroy(gameObject);
        }
        if (Parent.GetComponent<ButtonContainer>())
        {
            if (Parent.GetComponent<ButtonContainer>().ContainObject != null)
            {
                Parent.GetComponent<ButtonContainer>().ContainObject.GetComponent<SlimeLevels>().AddCrystal(GetComponent<Crystal>().Type);
                Parent.GetComponent<ButtonContainer>().RefreshData();
                Destroy(gameObject);
            }
        }
        if (Parent.GetComponent<CrystalContainer>())
        {
            if (Parent.GetComponent<CrystalContainer>().ContainObject == null)
            {
                Parent.GetComponent<CrystalContainer>().ContainObject =gameObject;
            }
            else
            {
                LastParent.GetComponent<CrystalContainer>().ContainObject = Parent.GetComponent<CrystalContainer>().ContainObject;
                Parent.GetComponent<CrystalContainer>().ContainObject = gameObject;
                LastParent.GetComponent<CrystalContainer>().ContainObject.GetComponent<DragCrystal>().LastParent = LastParent;
                LastParent = Parent;
            }
        }
        else
        {
            LastParent.GetComponent<CrystalContainer>().ContainObject = gameObject;
        }
    }

    public void SetTMPParent(GameObject _Parent)
    {
        Parent = _Parent;
    }
}
