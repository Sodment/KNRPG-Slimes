using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerDataContainer : MonoBehaviour
{
    private float Red;
    private float Green;
    private float Blue;
    private string CustomPrefabName;

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void Prepare()
    {
        GameObject.FindObjectOfType<Customize>().GetData(ref Red, ref Green, ref Blue, ref CustomPrefabName);
    }

    public void SetData(GameObject MotherSlime)
    {
        Vector3 Pos = MotherSlime.transform.position;
        Destroy(MotherSlime);
        GameObject GO = (GameObject)Instantiate(Resources.Load("Customize/" + CustomPrefabName, typeof(GameObject)) as GameObject, Pos, Quaternion.identity);
        GO.GetComponent<Renderer>().material.color = new Color(Red, Green, Blue, 0.5f);
        GO.AddComponent<MotherSlime>();
        GameObject HPwsk = (GameObject)Instantiate(Resources.Load("Tools/CrystalHPwsk", typeof(GameObject)) as GameObject);
        for(int i= HPwsk.transform.childCount-1; i>=0; i--)
        {
            HPwsk.transform.GetChild(i).parent = GO.transform;
        }
        GO.GetComponent<MotherSlime>().GetDMG();
        GO.GetComponent<MotherSlime>().Player = GameObject.FindObjectOfType<IventoryV5>();
        GO.GetComponent<MotherSlime>().SlimePref = Resources.Load("Slimes/NetworkSlime", typeof(GameObject)) as GameObject;
        GO.name = "King";
        Destroy(HPwsk);
        GameObject.FindObjectOfType<PhotonView>().RPC("SetEnemyData", RpcTarget.Others, Red, Green, Blue, CustomPrefabName);
    }

}
