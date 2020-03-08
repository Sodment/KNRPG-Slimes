using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkController : MonoBehaviourPunCallbacks {
    void Start() {
        PhotonNetwork.ConnectUsingSettings();
        PhotonNetwork.AutomaticallySyncScene = true;

        RoomOptions RoomSet = new RoomOptions() { IsVisible = true, IsOpen = true, MaxPlayers = (byte)16 };
        PhotonNetwork.CreateRoom("Room", RoomSet);

        PhotonNetwork.JoinRoom("Room");
    }

    public override void OnConnectedToMaster() {
        Debug.Log("Connected to: " + PhotonNetwork.CloudRegion);
    }

    public override void OnCreateRoomFailed(short returnCode, string message) {
        Debug.Log("Failed to create room, code: " + returnCode);
    }

    public override void OnJoinRoomFailed(short returnCode, string message) {
        Debug.Log("Failed to join room, code: " + returnCode);
    }
}
