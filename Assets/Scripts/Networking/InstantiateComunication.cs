using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class InstantiateComunication : MonoBehaviour
{
    [PunRPC]
    public void RPC_InstantiateObject(Vector3 Position, Vector3 Rotation, int Type, int Level, int ID)
    {
        GameObject GO = (GameObject)Instantiate(Resources.Load("BattleSlime"), Position, Quaternion.Euler(Rotation));
        GO.GetComponent<SlimeLevels>().Type = Type;
        GO.GetComponent<SlimeLevels>().LVL = Level;
        GO.GetComponent<SlimeMovement>().PlayerID = ID;
    }
}
