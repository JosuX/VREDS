using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GpsSensor : MonoBehaviour
{
    public float value;
    public int source_index;

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            if(Physics.Raycast(transform.position,other.transform.forward,10,1<<11))
            {
                return;
            }

            FindObjectOfType<GpsDraw>().recallibrate();
        }
    }
}
