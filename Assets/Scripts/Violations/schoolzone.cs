﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class schoolzone : MonoBehaviour, Iviolation_script
{
    violation_tracker vt;
    car_scripts car;
    [SerializeField]
    float speedlimit;
    bool monitoring = true;

    public void Add_violation()
    {
        vt.school("You made a mistake at this place: " + car.transform.position.ToString());
        monitoring = false;
    }

    public void Switch()
    {
        this.enabled = !this.enabled;
    }

    void Start()
    {
        car = FindObjectOfType<car_scripts>();
        vt = FindObjectOfType<violation_tracker>();
        car = FindObjectOfType<car_scripts>();
        this.enabled = false;
        this.GetComponent<Collider>().enabled = false;
        this.GetComponent<Collider>().enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!monitoring)
        {
            return;
        }
        /// VIOLATION BEHAVIOR
        if ( car.velocity_norm > speedlimit)
        {
            Add_violation();
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            monitoring = true;
        }
    }
}
