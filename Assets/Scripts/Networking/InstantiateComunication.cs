using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class InstantiateComunication : MonoBehaviour
{
    [PunRPC]
    public void RPC_InstantiateObject(string objName, Vector3 Position, Vector3 Rotation)
    {
        string Name = objName.Remove(objName.Length - 7, 7);
        GameObject GO = (GameObject)Instantiate(Resources.Load(Name), Position, Quaternion.Euler(Rotation));
        Debug.Log("Player" +GetComponent<PhotonView>().ViewID+" Set Object: "+Name+" on position: "+Position);
    }
}
