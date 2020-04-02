using Photon.Pun;
using UnityEngine;
using Photon.Realtime;

public class QuickStartRoomController : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private int SceneIndex=0;

    public override void OnEnable()
    {
        PhotonNetwork.AddCallbackTarget(this);
    }

    public override void OnDisable()
    {
        PhotonNetwork.RemoveCallbackTarget(this);
    }

    public override void OnJoinedRoom()
    {
        StartGame();
    }

    private void StartGame()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.LoadLevel(SceneIndex);
        }
    }

}
