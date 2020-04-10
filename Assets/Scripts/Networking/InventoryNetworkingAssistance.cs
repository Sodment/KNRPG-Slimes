using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class InventoryNetworkingAssistance : MonoBehaviour
{
    PhotonView PV;

    private void Start()
    {
        PV = GetComponent<PhotonView>();
    }

    public void Drop(GameObject Object)
    {
        if (PV == null) { Debug.Log("PhotonView is null"); Start(); }
        else
        {
            Debug.Log("Wysłano");
            PV.RPC("PutSlime", RpcTarget.Others, Object.transform.position.x, Object.transform.position.y, Object.transform.position.z, PV.ViewID, Object.GetComponent<SlimeBehaviour>().UnitID, Object.GetComponent<SlimeLevelsV2>().Crystals());
        }
    }

    public void Drop(GameObject Object, Vector3 Parent)
    {
        if (PV == null) { Debug.Log("PhotonView is null"); Start(); }
        else
        {
            Debug.Log("Wysłano");
            PV.RPC("PutSlime", RpcTarget.Others, Parent.x, Parent.y, Parent.z, PV.ViewID, Object.GetComponent<SlimeBehaviour>().UnitID, Object.GetComponent<SlimeLevelsV2>().Crystals());
        }
    }

    public void Remove(string UnitID)
    {
        if (PV == null) { Debug.Log("PhotonView is null"); Start(); }
        else
        {
            Debug.Log("Wysłano");
            PV.RPC("RemoveSlime", RpcTarget.Others, UnitID);
        }
    }

    public void PutCrystal(string UnitID, int CrystalType)
    {
        if (PV == null) { Debug.Log("PhotonView is null"); Start(); }
        else
        {
            Debug.Log("Wysłano");
            PV.RPC("PutCrystal", RpcTarget.Others, UnitID, CrystalType);
        }
    }


    public void UpdateUnitPosition(string UnitID, float x, float y, float z)
    {
        if (PV == null) { Debug.Log("PhotonView is null"); Start(); }
        else
        {
            Debug.Log("Wysłano");
            PV.RPC("UpdateUnitPosition", RpcTarget.Others, UnitID, x,y,z);
        }
    }

}
