using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DisplayGold : MonoBehaviour
{
    Text goldtext;
    int player1gold;
    int player2gold;
    int currentplayer;
    void Start()
    {
        goldtext = GetComponent<Text>();
        player1gold = GameObject.Find("CameraHandler").GetComponent<PVPGameMenager>().goldplayer1;
        player2gold = GameObject.Find("CameraHandler").GetComponent<PVPGameMenager>().goldplayer2;
        currentplayer = (int)GameObject.Find("CameraHandler").GetComponent<PVPGameMenager>().CurrentStage;
    }

    // Update is called once per frame
    void Update()
    {
        player1gold = GameObject.Find("CameraHandler").GetComponent<PVPGameMenager>().goldplayer1;
        player2gold = GameObject.Find("CameraHandler").GetComponent<PVPGameMenager>().goldplayer2;
        currentplayer = (int)GameObject.Find("CameraHandler").GetComponent<PVPGameMenager>().CurrentStage;
        if (currentplayer == 0){ goldtext.enabled = true;  goldtext.text = "Gold: " + player1gold.ToString(); }
        else if(currentplayer == 2){ goldtext.enabled = true; goldtext.text = "Gold: " + player2gold.ToString(); }
        else { goldtext.enabled = false; }
    }
}
