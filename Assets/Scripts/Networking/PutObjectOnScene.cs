using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PutObjectOnScene : MonoBehaviour
{
    [SerializeField]
    private GameObject SlimePrefab= null;
    [SerializeField]
    private GameObject[] CrystalsData=null;

    [PunRPC]
    void PutSlime(float x, float y, float z, int PlayerID, string UnitID, int[] Crystals)
    {
        GameObject GO = (GameObject)Instantiate(SlimePrefab, new Vector3(x,y,z), Quaternion.identity);
        Ray ray = new Ray(GO.transform.position, Vector3.down);
        RaycastHit HitInfo;
        if(Physics.Raycast(ray, out HitInfo))
        {
            GO.transform.parent = HitInfo.collider.gameObject.transform;
        }

        GO.GetComponent<SlimeLevelsV2>().Start();

        foreach(int k in Crystals)
        {
            GO.GetComponent<SlimeLevelsV2>().AddCrystal(CrystalsData[k].GetComponent<Crystal>());
        }

        GO.GetComponent<SlimeBehaviour>().PlayerID = PlayerID;
        GO.GetComponent<SlimeBehaviour>().UnitID = UnitID;
    }

    [PunRPC]
    void RemoveSlime(string UnitID)
    {
        foreach(SlimeBehaviour k in GameObject.FindObjectsOfType<SlimeBehaviour>())
        {
            if (k.UnitID == UnitID) { Destroy(k.gameObject); }
        }
    }

    [PunRPC]
    void PutCrystal(string UnitID, int CrystalType)
    {
        foreach (SlimeBehaviour k in GameObject.FindObjectsOfType<SlimeBehaviour>())
        {
            if (k.UnitID == UnitID) { k.GetComponent<SlimeLevelsV2>().AddCrystal(CrystalsData[CrystalType].GetComponent<Crystal>()); }
        }
    }

    [PunRPC]
    void UpdateUnitPosition(string UnitID, float x, float y, float z)
    {
        bool Founded = false;
        Ray ray = new Ray(new Vector3(x, y, z), Vector3.down);
        RaycastHit Info;
        Physics.Raycast(ray, out Info);

        foreach (SlimeBehaviour k in GameObject.FindObjectsOfType<SlimeBehaviour>())
        {
            if (k.UnitID == UnitID)
            { 
                if (!Founded)
                { k.transform.parent = Info.collider.transform; Founded = true; }
                else { Destroy(k.gameObject); }
            }
        }
    }
}