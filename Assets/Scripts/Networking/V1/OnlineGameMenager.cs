using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
public class OnlineGameMenager : MonoBehaviour
{
    [SerializeField]
    private Text Timer;
    [SerializeField]
    private int PrepareDruation = 4;
    PhotonView view;


    private int[] PlayerHP = new int[2];
    private void Start()
    {
        PlayerHP[0] = 8;
        PlayerHP[1] = 8;
        if (!PhotonNetwork.LocalPlayer.IsMasterClient) { Destroy(this); }
        view = GetComponent<PhotonView>();
        StartCoroutine(Prepare());
    }

    IEnumerator Prepare()
    {
        for(int i=PrepareDruation; i>=0; i--)
        {
            if (i > 0)
            { view.RPC("ChangeTime", RpcTarget.Others, i); }
            else { view.RPC("ChangeTime", RpcTarget.All, i); }
            Timer.text = i.ToString();
            if (i == 0) break;
            yield return new WaitForSeconds(1.0f);
        }

        transform.GetChild(0).gameObject.SetActive(false);
        StartCoroutine(FightStage());
    }

    IEnumerator FightStage()
    {
        bool GameOver = false;
        for (int i = 500; i >= 0; i--)
        {
            if (i == 0) break;


            int Player1Units = 0;
            int Player2Units = 0;
            foreach(SlimeBehaviour k in GameObject.FindObjectsOfType<SlimeBehaviour>())
            {
                if(k.PlayerID== PhotonNetwork.LocalPlayer.ActorNumber)
                {
                    Player1Units++;
                }
                else
                {
                    Player2Units++;
                }
            }
            if(Player1Units==0 || Player2Units == 0)
            {
                break;
            }

            yield return new WaitForSeconds(1.0f);
        }
        int WinnerID = -1;
        if (GameObject.FindObjectOfType<SlimeBehaviour>() != null)
        {
            WinnerID = GameObject.FindObjectOfType<SlimeBehaviour>().PlayerID;
            if (WinnerID == PhotonNetwork.MasterClient.ActorNumber)
            {
                PlayerHP[1]--;
                if (PlayerHP[1] <= 0) { GameOver = true; }
            }
            else
            {
                PlayerHP[0]--;
                if (PlayerHP[0] <= 0) { GameOver = true; }
            }
        }

        if (!GameOver)
        {
            view.RPC("SubmitBattle", RpcTarget.All, WinnerID);
            yield return new WaitForSeconds(5.0f);
            view.RPC("Prepare", RpcTarget.All);
            StartCoroutine(Prepare());
        }
        else
        {
            view.RPC("SubmitWar", RpcTarget.All, WinnerID);
        }
    }

}
