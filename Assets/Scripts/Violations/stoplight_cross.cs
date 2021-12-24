using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stoplight_cross : MonoBehaviour, Iviolation_script
{

    violation_tracker vt;
    car_scripts car;
    stoplight sl;

    public void Add_violation()
    {
        vt.stop_l("That was a red light bruv: " + transform.position.ToString());
    }

    public void Switch()
    {
        this.enabled = !this.enabled;
    }

    void Start()
    {
        vt = FindObjectOfType<violation_tracker>();
        car = FindObjectOfType<car_scripts>();
        sl = GetComponentInParent<stoplight>();
        this.enabled = false;
        this.GetComponent<Collider>().enabled = false;
        this.GetComponent<Collider>().enabled = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if(!sl.go_z && car.current_axis == 1)
            {
                Add_violation();
            }

            if (!sl.go_x && car.current_axis == -1)
            {
                Add_violation();
            }
        }
    }

}
