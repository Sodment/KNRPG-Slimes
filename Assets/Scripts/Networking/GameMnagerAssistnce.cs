using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class GameMnagerAssistnce : MonoBehaviour
{
    public Text text;

    private List<GameObject> Slimes = new List<GameObject>();

    [SerializeField]
    private GameObject WinnerInfoCanvas;
    [SerializeField]
    private Text WinnerIbfoText;

    [PunRPC]
    void ChangeTime(int newTime)
    {
        text.text = newTime.ToString();
        if (newTime == 0) { transform.GetChild(0).gameObject.SetActive(false); StartBattle(); }
    }

    public void StartBattle()
    {
        foreach(SlimeBehaviour k in GameObject.FindObjectsOfType<SlimeBehaviour>())
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
}
