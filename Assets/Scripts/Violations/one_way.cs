using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class one_way : MonoBehaviour
{
    [SerializeField]
    bool to_red, to_green;
    int dir;

    private void Awake()
    {
        if(to_red)
        {
            dir = 1;
        }
        else if(to_green)
        {
            dir = -1;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            if (Mathf.Abs(transform.forward.z) == Mathf.Abs(other.transform.forward.z))
            {
                print("active");
                if (other.transform.forward.x + other.transform.forward.z != dir)
                {
                    FindObjectOfType<violation_tracker>().counterf("Counterflow on one way road");
                }
            }
            else
            {
                print("inactive");
            }
        }
    }
}
