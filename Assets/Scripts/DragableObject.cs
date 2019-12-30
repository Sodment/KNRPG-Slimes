using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragableObject : MonoBehaviour
{
    [HideInInspector]
    public Transform Parent;
    [HideInInspector]
    public Transform LastParent;
    [HideInInspector]
    public bool InUI;

    private void Update()
    {
        if (InUI)
        {
            if (Vector3.Distance(transform.position, Camera.main.ScreenToWorldPoint(Parent.transform.position) + Camera.main.transform.forward) > 0.01f)
            { transform.Translate((Camera.main.ScreenToWorldPoint(Parent.transform.position)+Camera.main.transform.forward - transform.position ) * Time.deltaTime * 5.0f, Space.World); }
            else
            { transform.position = Camera.main.ScreenToWorldPoint(Parent.transform.position) + Camera.main.transform.forward; this.enabled = false; }
        }
        else
        {
            if (Vector3.Distance(transform.position + Vector3.up, Parent.position) > 0.01f)
            { transform.Translate((Parent.position + Vector3.up - transform.position) * Time.deltaTime * 5.0f, Space.World); }
            else
            { transform.position = Parent.transform.position; this.enabled = false; }
        }
    }
}