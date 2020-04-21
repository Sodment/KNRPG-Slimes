using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectNetworkAssistance : MonoBehaviour
{
    Transform LastParent;
    private void Start()
    {
        LastParent = transform.parent;
    }
    void Update()
    {
        if (LastParent == null) { Start(); }
        if (transform.parent != LastParent)
        {
            if (LastParent.name!="UnitSlot"&&transform.parent.name == "UnitSlot")
            {
                Debug.Log("Powrót do Ekwipunku");
                GameObject.FindObjectOfType<InventoryNetworkingAssistance>().Remove(GetComponent<SlimeBehaviour>().UnitID);
                LastParent = transform.parent;
            }
            else if (LastParent.name == "UnitSlot" && transform.parent.name != "UnitSlot")
            {
                Debug.Log("Wypad na arenę");
                GameObject.FindObjectOfType<InventoryNetworkingAssistance>().Drop(gameObject);
                LastParent = transform.parent;
            }
            else if(LastParent.name != "UnitSlot" && transform.parent.name != "UnitSlot")
            {
                Debug.Log("Zmiana miejsc");
                Vector3 P = transform.parent.position;
                GameObject.FindObjectOfType<InventoryNetworkingAssistance>().UpdateUnitPosition(GetComponent<SlimeBehaviour>().UnitID, P.x, P.y, P.z);
            }
            LastParent = transform.parent;
        }
    }
}
