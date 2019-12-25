using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerV2 : MonoBehaviour
{
    private void Awake()
    {
        GameObject.FindGameObjectWithTag("GameController").GetComponent<InventoryV2>().SetView(GetComponent<PhotonView>());
    }
}
