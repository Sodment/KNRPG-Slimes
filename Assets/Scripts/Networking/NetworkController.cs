using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkController : MonoBehaviourPunCallbacks
{
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Yaaay! Connected to: " + PhotonNetwork.CloudRegion);
    }
    /*
        TypedLobby MyLobby = new TypedLobby("Lobby", LobbyType.Default);
        PhotonNetwork.JoinLobby(MyLobby);
    }

    public override void OnJoinedLobby()
    {
        Debug.Log("Yaaaay!x2 Joined to Lobby " + PhotonNetwork.CurrentLobby.Name);
    }
    */
}
