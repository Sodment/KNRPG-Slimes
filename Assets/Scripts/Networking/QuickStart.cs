using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuickStart : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private GameObject StartButton;
    [SerializeField]
    private GameObject CancelButton;
    [SerializeField]
    private int RoomSize;
   
    public override void OnConnectedToMaster()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        StartButton.SetActive(true);
    }

    public void QuickStartButton()
    {
        StartButton.SetActive(false);
        CancelButton.SetActive(true);
        PhotonNetwork.JoinRandomRoom();
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("Fail connect");
        CreateRoom();
    }

    void CreateRoom()
    {
        int RandomRoomNumber = Random.Range(0, 1000);
        RoomOptions RoomSet = new RoomOptions() { IsVisible = true, IsOpen = true, MaxPlayers = (byte)RoomSize };
        PhotonNetwork.CreateRoom("Room" + RandomRoomNumber, RoomSet);
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        CreateRoom();
    }

    public void QuickCancelButton()
    {
        StartButton.SetActive(true);
        CancelButton.SetActive(false);
        PhotonNetwork.LeaveRoom();
    }
}