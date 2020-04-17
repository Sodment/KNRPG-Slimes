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
            GameObject.FindObjectOfType<PlayerDataContainer>().SetData(MasterMotherSlime.gameObject);
            Destroy(ClientReroll);
            transform.GetChild(0).GetComponent<GameMnagerAssistnce>().EnemyMotherSlimePos = ClientMotherSlime.transform.position;
            Destroy(ClientMotherSlime.gameObject);
        }
        else
        {
            GameObject.FindObjectOfType<PlayerDataContainer>().SetData(ClientMotherSlime.gameObject);
            Destroy(MasterReroll);
            transform.GetChild(0).GetComponent<GameMnagerAssistnce>().EnemyMotherSlimePos = MasterMotherSlime.transform.position;
            Destroy(MasterMotherSlime.gameObject);
        }
        Destroy(this);
    }
}
