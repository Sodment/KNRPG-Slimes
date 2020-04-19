using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class Customize : MonoBehaviour
{
    [SerializeField]
    private Slider R;
    [SerializeField]
    private Slider G;
    [SerializeField]
    private Slider B;
    [SerializeField]
    private GameObject Model;
    [SerializeField]
    private Material Mat;

    List<GameObject> Models = new List<GameObject>();
    int CurrentMesh = 1;

    void Start()
    {
        Models.AddRange(Resources.LoadAll("Customize").Cast<GameObject>().ToArray());
        Model.GetComponent<MeshFilter>().sharedMesh = Models[0].GetComponent<MeshFilter>().sharedMesh;
        ColorChange();
        MeshChange(false);
    }

    public void ColorChange()
    {
        Mat.color = new Color(R.value, G.value, B.value, 0.5f);
    }

    public void MeshChange(bool Forward)
    {
        CurrentMesh = (CurrentMesh + ((Forward) ? 1 : -1)) % Models.Count;
        Vector3 Pos = Model.transform.position;
        Destroy(Model);
        Model = (GameObject)Instantiate(Models[CurrentMesh], Pos, Quaternion.identity);
        Mat = Model.GetComponent<Renderer>().material;
        ColorChange();
    }

    public void GetData(ref float r, ref float g, ref float b, ref string Name)
    {
        r = R.value;
        g = G.value;
        b = B.value;
        Name = Models[CurrentMesh].name;
    }
}
