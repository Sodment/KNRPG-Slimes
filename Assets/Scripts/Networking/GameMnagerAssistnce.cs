using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class GameMnagerAssistnce : MonoBehaviourPunCallbacks
{
    public Text text;

    private List<GameObject> Slimes = new List<GameObject>();

    [SerializeField]
    private GameObject WinnerInfoCanvas=null;
    [SerializeField]
    private Text WinnerIbfoText=null;

    private GameObject EnemyKingSlime;

    public Vector3 EnemyMotherSlimePos;


    [PunRPC]
    void ChangeTime(int newTime)
    {
        text.text = newTime.ToString();
        if (newTime == 0) { transform.GetChild(0).gameObject.SetActive(false); StartBattle(); }
    }

    public void StartBattle()
    {
        foreach (SlimeBehaviour k in GameObject.FindObjectsOfType<SlimeBehaviour>())
        {
            k.ChangeState(SlimeBehaviour.State.Fight);
            Slimes.Add(k.gameObject);
        }
    }

    [PunRPC]
    void SubmitBattle(int WinnerID)
    {
        if (PhotonNetwork.LocalPlayer.ActorNumber != WinnerID)
        {
            GameObject.FindObjectOfType<MotherSlime>().GetDMG();
        }
        else
        {
            EnemyKingSlime.GetComponent<MotherSlimeHP>().GetDMG();
        }
    }

    [PunRPC]
    void SubmitWar(int WinnerID)
    {
        if (PhotonNetwork.LocalPlayer.ActorNumber == WinnerID)
        {
            WinnerIbfoText.color = Color.green;
            WinnerIbfoText.text = "Completed Victory";
        }
        else
        {
            WinnerIbfoText.color = Color.red;
            WinnerIbfoText.text = "Completed Lose";
        }
        WinnerInfoCanvas.SetActive(true);
    }

    [PunRPC]
    void Prepare()
    {
        Debug.Log("And once again!");
        WinnerInfoCanvas.SetActive(false);
        transform.GetChild(0).gameObject.SetActive(true);
        foreach (GameObject k in Slimes)
        {
            k.SetActive(true);
            k.GetComponent<SlimeBehaviour>().ChangeState(SlimeBehaviour.State.Prepare);
        }
        Slimes.Clear();
    }

    [PunRPC]
    void SetEnemyData(float r, float g, float b, string name)
    {
        GameObject GO = (GameObject)Instantiate( Resources.Load("Customize/" + name, typeof(GameObject)) as GameObject, EnemyMotherSlimePos, Quaternion.identity);
        GO.GetComponent<Renderer>().material.color = new Color(r, g, b, 0.5f);
        GO.AddComponent<MotherSlimeHP>();
        GameObject HPwsk = (GameObject)Instantiate(Resources.Load("Tools/CrystalHPwsk", typeof(GameObject)) as GameObject);
        for (int i = HPwsk.transform.childCount - 1; i >= 0; i--)
        {
            HPwsk.transform.GetChild(i).parent = GO.transform;
        }
        GO.GetComponent<MotherSlimeHP>().GetDMG();
        EnemyKingSlime = GO;
        Destroy(HPwsk);
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        WinnerIbfoText.text = "Your opponent surrender \n You win";
        if (GameObject.FindObjectOfType<OnlineGameMenager>() != null)
        { Destroy(GameObject.FindObjectOfType<OnlineGameMenager>().GetComponent<OnlineGameMenager>()); }
        WinnerInfoCanvas.SetActive(true);
    }

    public void LeftRoom()
    {
        PhotonNetwork.LeaveRoom();
    }
}
