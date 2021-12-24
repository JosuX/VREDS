using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class input_detection : MonoBehaviour
{
    public bool left, right, accelerate, brake;
    public float horizontal, vertical, rotate, accelerate_2, brake_2;
    public KeyCode signal_l, signal_r, signal_l_2, signal_r_2;

    // Update is called once per frame
    void Update()
    {
        left = Input.GetKey(KeyCode.A);
        right = Input.GetKey(KeyCode.D);
        accelerate = Input.GetKey(KeyCode.W);
        brake = Input.GetKey(KeyCode.S);
        signal_l = KeyCode.Q;
        signal_r = KeyCode.E;

        accelerate_2 = Input.GetAxis("Accelerate");
        brake_2 = Input.GetAxis("Brake");
        signal_l_2 = KeyCode.JoystickButton4;
        signal_r_2 = KeyCode.JoystickButton5;
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        if (horizontal != 0.0f || vertical != 0.0f)
        {
            rotate = Mathf.Atan2(vertical, horizontal) * Mathf.Rad2Deg;
        }
    }
}
