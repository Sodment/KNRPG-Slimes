using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class QuickSetupPlayer : MonoBehaviour
{
    int PlayerCount=1;
    void Start()
    {
        CreatePlayer();
    }

    private void CreatePlayer()
    {
        Debug.Log("CreatePlayer"+PlayerCount);
        PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PhotonPlayer"), Vector3.zero, Quaternion.Euler(0,45+180*PlayerCount,0));
    }
}
