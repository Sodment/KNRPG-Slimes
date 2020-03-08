using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class CreateRoom : MonoBehaviourPunCallbacks {
    [SerializeField]
    private Text _roomName;
    private Text RoomName {
        get { return _roomName; }
    }

    public void OnClickCreateRoom() {
        RoomOptions RoomSet = new RoomOptions() { IsVisible = true, IsOpen = true, MaxPlayers = (byte)16 };
        if(PhotonNetwork.CreateRoom(RoomName.text, RoomSet)) {
            Debug.Log("Create room successfully sent");
        }
    }
    
    public override void OnCreateRoomFailed(short returnCode, string message) {
        Debug.Log("Failed to create room, code: " + returnCode);
    }

    public override void OnCreatedRoom() {
        Debug.Log("Successfully created room ");
    }
}
