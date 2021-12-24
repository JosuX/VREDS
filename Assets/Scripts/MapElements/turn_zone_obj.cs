using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class turn_zone_obj : MonoBehaviour
{

    //[HideInInspector]
    public float target_spot;
    public int zone;
    [SerializeField] float modifier;

    private void Start()
    {
        float bounds_z = this.GetComponent<Collider>().bounds.extents.z;
        float bounds_x = this.GetComponent<Collider>().bounds.extents.x;
        if (transform.rotation.eulerAngles.y == 180 || transform.rotation.eulerAngles.y == 0) /// Movement turning from Z to X axis
        {
            target_spot = (transform.position + (transform.forward * (bounds_z - modifier))).z;
        }

        if (transform.rotation.eulerAngles.y == 90 || transform.rotation.eulerAngles.y == 270) /// Movement turning from X to Z axis
        {
            target_spot = (transform.position+(transform.forward * (bounds_x - modifier))).x;
        }

    }

}
