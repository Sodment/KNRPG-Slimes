using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class GamePrepare : MonoBehaviour
{
    [SerializeField]
    private MotherSlime MasterMotherSlime;
    [SerializeField]
    private Interact MasterReroll;

    [SerializeField]
    private MotherSlime ClientMotherSlime;
    [SerializeField]
    private Interact ClientReroll;


    private void Awake()
    {
        transform.rotation = Quaternion.Euler(new Vector3(0, 114+90*Mathf.Sign(PhotonNetwork.LocalPlayer.ActorNumber-PhotonNetwork.PlayerListOthers[0].ActorNumber), 0));
        GameObject.FindObjectOfType<IventoryV5>().PlayerID = PhotonNetwork.LocalPlayer.ActorNumber;
        if (PhotonNetwork.LocalPlayer.IsMasterClient)
        {
            Destroy(ClientMotherSlime);
            MasterMotherSlime.GetDMG();
            Destroy(ClientReroll);
        }
        else
        {
            Destroy(MasterMotherSlime);
            ClientMotherSlime.GetDMG();
            Destroy(MasterReroll);
        }
        Destroy(this);
    }
}
