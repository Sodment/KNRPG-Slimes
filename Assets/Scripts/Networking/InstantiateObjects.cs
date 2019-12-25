using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.IO;

public class InstantiateObjects : MonoBehaviour
{
    PhotonView MyView;

    public void InstantiateObject(GameObject obj)
    {
        MyView.RPC("RPC_InstantiateObject", RpcTarget.All, obj.name, obj.transform.position, obj.transform.rotation.eulerAngles);
        Destroy(this.gameObject, 0.1f);
    }

    public void SetOwner(PhotonView Owner)
    {
        MyView = Owner;
    }
}
