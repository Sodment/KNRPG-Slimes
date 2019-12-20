using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Player : MonoBehaviour
{
    int PlayerID;

    public Transform Cam;
    float RotateX;
    float RotateY;

    public float Sensitive = 1;
    void Start()
    {
        RotateX = Cam.rotation.eulerAngles.x;
        RotateY = transform.rotation.eulerAngles.y;
        PlayerID = GetComponentInParent<PhotonView>().ViewID;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(2))
        {
            RotateY += Input.GetAxis("Mouse X") * Sensitive;
            RotateX += Input.GetAxis("Mouse Y") / Sensitive;
            if (RotateX > 55.0f) { RotateX = 55.0f; }
            if (RotateX < 35.0f) { RotateX = 35.0f; }
        }

        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, RotateY, 0), Time.deltaTime*3.0f);
        Cam.localRotation = Quaternion.Slerp(Cam.localRotation, Quaternion.Euler(RotateX, 0, 0), Time.deltaTime);
    }
}
