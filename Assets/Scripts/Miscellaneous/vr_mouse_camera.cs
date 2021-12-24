using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class vr_mouse_camera : MonoBehaviour
{
    public float speedH = 2.0f;
    public float speedV = 2.0f;

    private float yaw = 0.0f;
    private float pitch = 0.0f;

    private Vector3 cam_offset;
    private Transform carmodel;

    // Start is called before the first frame update
    void Start()
    {
        carmodel = transform.parent;
    }

    // Update is called once per frame
    void Update()
    {
        yaw += speedH * Input.GetAxis("Mouse X");
        pitch -= speedV * Input.GetAxis("Mouse Y");

        cam_offset = carmodel.eulerAngles;

        transform.eulerAngles = new Vector3(pitch + cam_offset.x, yaw + cam_offset.y, 0.0f);
    }
}
