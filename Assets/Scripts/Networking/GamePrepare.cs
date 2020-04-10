using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class GamePrepare : MonoBehaviour
{
    public Text PlayerData;
    private void Awake()
    {
        transform.rotation = Quaternion.Euler(new Vector3(0, 114+90*Mathf.Sign(PhotonNetwork.LocalPlayer.ActorNumber-PhotonNetwork.PlayerListOthers[0].ActorNumber), 0));
        
        if(PhotonNetwork.LocalPlayer.ActorNumber - PhotonNetwork.PlayerListOthers[0].ActorNumber > 0)
        {
            GameObject.FindObjectOfType<IventoryV5>().Player = PVPGameMenager.Stage.Player2;
            PlayerData.text = "Player2";
        }
        else
        {
            GameObject.FindObjectOfType<IventoryV5>().Player = PVPGameMenager.Stage.Player1;
            PlayerData.text = "Player1";
        }
        
        Destroy(this);
    }
}
