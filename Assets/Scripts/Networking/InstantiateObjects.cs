using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.IO;

public class InstantiateObjects : MonoBehaviour
{
    PhotonView MyView;

    private void Start()
    {
        MyView = GetComponent<PhotonView>();
    }

    public void InstantiateObject(GameObject obj)
    {
        int Type = obj.GetComponent<SlimeLevels>().Type;
        int Level = obj.GetComponent<SlimeLevels>().LVL;
        MyView.RPC("RPC_InstantiateObject", RpcTarget.All, obj.transform.position, obj.transform.rotation.eulerAngles, Type, Level, MyView.ViewID);
    }
}
