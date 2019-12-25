using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class ConnectionInfo : MonoBehaviour
{
    public Text Room;

    private void Start()
    {
        Room.text = PhotonNetwork.CurrentRoom.Name;
    }
}
