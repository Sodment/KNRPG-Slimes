using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotherSlime : MonoBehaviour
{
    public PVPGameMenager.Stage Owner;
    public IventoryV5 Player;
    public GameObject SlimePref;
    List<Crystal> Crystals = new List<Crystal>();

    public void AddCrystal(Crystal crystal)
    {
        if (Owner != Player.Player) { Destroy(this); return; }
        Crystals.Add(crystal);
        crystal.transform.parent = transform;
        crystal.transform.position = Vector3.down * 50.0f;
        Destroy(crystal.GetComponent<SmothPass>());
        if (Crystals.Count >= 4)
        {
            Spawn();
        }

    }

    void Spawn()
    {
        GameObject DropZone = null;
        float MinDist = float.MaxValue;
        foreach (Transform k in GameObject.Find("Plansza").transform)
        {
            if (k.childCount > 0) { continue; }
            Vector3 Diff = k.position - transform.position;
            float Dist = Mathf.Abs(Diff.x) + Diff.z * ((Owner == PVPGameMenager.Stage.Player1) ? 100.0f:-100.0f) ;
            if (Dist < MinDist)
            {
                MinDist = Dist;
                DropZone = k.gameObject;
            }
        }
        if (DropZone == null) { return; }
        GameObject GO = (GameObject)Instantiate(SlimePref, transform.position, Quaternion.identity);
        GO.transform.parent = DropZone.transform;
        GO.GetComponent<SlimeBehaviour>().PlayerID = Player.PlayerID;
        GO.GetComponent<SlimeLevelsV2>().Start();
        GO.GetComponent<SlimeLevelsV2>().AddCrystal(Crystals[Random.Range(0, Crystals.Count)]);
        Player.AddItem(GO);
        Crystal[] array = Crystals.ToArray();
        Crystals.Clear();
        foreach(Crystal k in array)
        {
            Destroy(k);
        }

        if (Player.GetComponent<InventoryNetworkingAssistance>() != null)
        { Player.GetComponent<InventoryNetworkingAssistance>().Drop(GO, DropZone.transform.position); }
    }

}
