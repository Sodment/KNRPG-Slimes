using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryV4 : MonoBehaviour
{
    public int PlayerIndex; //numer gracza
    public GameObject SlimePrefab;
    public GameObject[] Crystals;
    void Start()
    {
        foreach(ButtonContainer k in GetComponentsInChildren<ButtonContainer>())
        {
            GameObject GO = Instantiate(SlimePrefab);
            k.ContainObject = GO;
            GO.transform.parent = Camera.main.transform.GetChild(PlayerIndex - 1);
            GO.transform.position = Camera.main.ScreenToWorldPoint(k.transform.position)+Camera.main.transform.forward;
            GO.GetComponent<DragSlime>().SetOwner(PlayerIndex);
            GO.GetComponent<DragSlime>().LastParent = k.gameObject;
            GO.GetComponent<Collider>().enabled = false;
            GO.GetComponent<SlimeMovement>().PlayerID = PlayerIndex;
        }

        foreach(CrystalContainer k in GetComponentsInChildren<CrystalContainer>())
        {
            GameObject GO = Instantiate(Crystals[Random.Range(0,Crystals.Length)]);
            k.ContainObject = GO;
            GO.transform.parent = Camera.main.transform.GetChild(PlayerIndex - 1);
            GO.transform.position = Camera.main.ScreenToWorldPoint(k.transform.position) + Camera.main.transform.forward;
            GO.GetComponent<DragCrystal>().LastParent = k.gameObject;
        }
    }

    public void GetSlimeFromButton(ButtonContainer Container)
    {
        if (Container.ContainObject == null) { return; }
        GameObject.FindObjectOfType<DragMenager>().DragObject = Container.ContainObject;
        Container.ContainObject.GetComponent<DragSlime>().SetTMPParent(Container.ContainObject.GetComponent<DragSlime>().LastParent);
        Container.ContainObject.GetComponent<DragSlime>().Drag = true;
        Container.ContainObject = null;
    }

    public void GetButtonForSlime(GameObject Object)
    {
        if (GameObject.FindObjectOfType<DragMenager>().DragObject != null && GameObject.FindObjectOfType<DragMenager>().DragObject.GetComponent<DragSlime>())
        { GameObject.FindObjectOfType<DragMenager>().DragObject.GetComponent<DragSlime>().SetTMPParent(Object); }
    }

    public void ResetButtonForSlime()
    {
        if (GameObject.FindObjectOfType<DragMenager>().DragObject != null && GameObject.FindObjectOfType<DragMenager>().DragObject.GetComponent<DragSlime>())
        {
            GameObject.FindObjectOfType<DragMenager>().DragObject.GetComponent<DragSlime>().SetTMPParent(null);
        }
    }

    public void GetCrystalFromContainer(CrystalContainer Container)
    {
        if (Container.ContainObject == null) { return; }
        GameObject.FindObjectOfType<DragMenager>().DragObject = Container.ContainObject;
        Container.ContainObject.GetComponent<DragCrystal>().SetTMPParent(Container.ContainObject.GetComponent<DragCrystal>().LastParent);
        Container.ContainObject.GetComponent<DragCrystal>().Drag = true;
        Container.ContainObject = null;
    }

    public void GetButtonForCrystal(GameObject Object)
    {
        if (GameObject.FindObjectOfType<DragMenager>().DragObject != null && GameObject.FindObjectOfType<DragMenager>().DragObject.GetComponent<DragCrystal>())
        {
            GameObject.FindObjectOfType<DragMenager>().DragObject.GetComponent<DragCrystal>().SetTMPParent(Object);
        }
    }

    public void ResetButtonForCrystal()
    {
        if (GameObject.FindObjectOfType<DragMenager>().DragObject != null && GameObject.FindObjectOfType<DragMenager>().DragObject.GetComponent<DragCrystal>())
        {
            GameObject.FindObjectOfType<DragMenager>().DragObject.GetComponent<DragCrystal>().SetTMPParent(null);
        }
    }

    public void ReRoll()
    {
        foreach (CrystalContainer k in GetComponentsInChildren<CrystalContainer>())
        {
            Destroy(k.ContainObject);
            GameObject GO = Instantiate(Crystals[Random.Range(0, Crystals.Length)]);
            k.ContainObject = GO;
            GO.transform.parent = Camera.main.transform.GetChild(PlayerIndex - 1);
            GO.transform.position = Camera.main.ScreenToWorldPoint(k.transform.position) + Camera.main.transform.forward;
            GO.GetComponent<DragCrystal>().LastParent = k.gameObject;
        }
    }
}
