using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragMenager : MonoBehaviour
{
    public GameObject DragObject=null;

    public void FastDrop()
    {
        if (DragObject != null)
        {
            if (DragObject.GetComponent<DragSlime>())
            {
                DragObject.GetComponent<DragSlime>().Drag = false;
                GameObject LastParent = DragObject.GetComponent<DragSlime>().LastParent;
                if (LastParent.GetComponent<ButtonContainer>())
                {
                    DragObject.transform.parent = Camera.main.transform.GetChild(DragObject.GetComponent<DragSlime>().GetOwner()-1);
                    DragObject.transform.position = Camera.main.ScreenToWorldPoint(LastParent.transform.position)+Camera.main.transform.forward;
                }
                else if (LastParent.GetComponent<Node>())
                {
                    DragObject.transform.parent = LastParent.transform;
                }
            }
            if (DragObject.GetComponent<DragCrystal>())
            {
                DragObject.GetComponent<DragCrystal>().Drag = false;
                DragObject.transform.position = Camera.main.ScreenToWorldPoint(DragObject.GetComponent<DragCrystal>().transform.position);
            }
        }
    }
}
