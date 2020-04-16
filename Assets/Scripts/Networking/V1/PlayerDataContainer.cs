using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerDataContainer : MonoBehaviour
{
    private Color PlayerColor;
    private string CustomPrefabName;

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void Prepare(Color color, string name)
    {
        PlayerColor = color;
        CustomPrefabName = name;
    }

    public Color GetColor()
    {
        return PlayerColor;
    }

    public string Name()
    {
        return CustomPrefabName;
    }

}
