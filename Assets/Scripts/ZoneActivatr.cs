using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneActivatr : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Zone"))
        {
            other.gameObject.GetComponent<Iviolation_script>().Switch();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Zone"))
        {
            other.gameObject.GetComponent<Iviolation_script>().Switch();
        }
    }
}
