using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

using UnityEngine.UI;

public class TestConnect : MonoBehaviourPunCallbacks
{
    public Text PlayerName;

    private void Start()
    {
        PlayerName.text="Connecting";
        PhotonNetwork.NickName = "Quest" + Random.Range(0, 10000).ToString();// MasterMenager.GameSettings.NickName;
        PhotonNetwork.GameVersion = "0.0.0";// MasterMenager.GameSettings.GameVersion;
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        PlayerName.text = "Connected";
        Debug.Log(PhotonNetwork.LocalPlayer.NickName);
        PhotonNetwork.JoinLobby();
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        if (PhotonNetwork.CurrentRoom.PlayerCount == PhotonNetwork.CurrentRoom.MaxPlayers)
        {
            PlayerName.text = "Prepare";
        }
    }

    public bool Ready()
    {
        if (PhotonNetwork.CurrentRoom == null) { return false; }
        if (PhotonNetwork.CurrentRoom.PlayerCount == PhotonNetwork.CurrentRoom.MaxPlayers)
            return true;
        else return false;
    }
}
