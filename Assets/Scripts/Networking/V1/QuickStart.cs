using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class QuickStart : MonoBehaviourPunCallbacks
{
    public Text RoomName;
    public override void OnJoinedLobby()
    {
        RoomName.text = "Joined Lobby";
    }

    public void OnClick_OnlinePlay()
    {
        if(!PhotonNetwork.InLobby) { return; }

        Debug.Log("Try join to Random room");
        PhotonNetwork.JoinRandomRoom();
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("Any free room");
        string Name = "Room" + Random.Range(0, 10000).ToString();
        RoomOptions Options = new RoomOptions();
        Options.MaxPlayers = 2;
        Options.IsOpen = true;
        Options.PlayerTtl = 10;
        PhotonNetwork.JoinOrCreateRoom(Name, Options, TypedLobby.Default);
    }

    public override void OnJoinedRoom()
    {
        RoomName.text = PhotonNetwork.NickName + "\n" + PhotonNetwork.CurrentRoom.Name;
        if (PhotonNetwork.CurrentRoom.PlayerCount == PhotonNetwork.CurrentRoom.MaxPlayers)
        {
            RoomName.text = "Prepare";
        }
        if (PhotonNetwork.CurrentRoom.PlayerCount == 1)
        {
            PhotonNetwork.SetMasterClient(PhotonNetwork.LocalPlayer);
        }
    }
}
