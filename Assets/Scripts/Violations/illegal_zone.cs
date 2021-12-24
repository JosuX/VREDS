using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class illegal_zone : MonoBehaviour
{
    [SerializeField] GameObject barriers;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            FindObjectOfType<violation_tracker>().illegal_z("Entered illegal space");
            barriers.SetActive(false);
        }
    }
}
