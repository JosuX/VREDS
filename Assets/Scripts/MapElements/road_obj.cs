using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class road_obj : MonoBehaviour
{
    [HideInInspector]
    public float target_path, target_opposite;
    // Start is called before the first frame update
    void Start()
    {
        float local_x = this.GetComponent<Collider>().bounds.extents.x/ transform.localScale.x / 2;
        float local_z = this.GetComponent<Collider>().bounds.extents.z / transform.localScale.x / 2;

        if (transform.rotation.eulerAngles.y == 90 || transform.rotation.eulerAngles.y == 270)
        {
            target_opposite = transform.TransformPoint(Vector3.right * -local_z).z;
            target_path = transform.TransformPoint(Vector3.right * local_z).z;
        }
        else if (transform.rotation.eulerAngles.y == 180 || transform.rotation.eulerAngles.y == 0)
        {
            target_opposite = transform.TransformPoint(Vector3.right * -local_x).x;
            target_path = transform.TransformPoint(Vector3.right * local_x).x;
        }
        
        else
        {
            print(gameObject.name + " is unaligned" + Mathf.Abs(transform.rotation.eulerAngles.y));
        }
    }

}
