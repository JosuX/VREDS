using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class anim_events : MonoBehaviour
{
    Vector3 new_pos;
    Vector3 new_rotation;
    Transform car_model;
    Animation anim;
    car_scripts cs;
    float turn_speed_reduction = 2;

    private void Start()
    {
        anim = GetComponent<Animation>();
        cs = GetComponent<car_scripts>();
        car_model = transform.GetChild(0);
    }
    public void teleport() 
    {
        new_pos = car_model.position;
        transform.position = new_pos;
        new_rotation = car_model.eulerAngles;
        transform.eulerAngles = new_rotation;
        transform.eulerAngles = Vector3.up * Mathf.Round(transform.eulerAngles.y);
        anim.Play("Idle",PlayMode.StopAll);
        ///// PUT CODES HERE FOR THINGS TO SETUP BEFORE reposition() and AFTER animation play
        cs.current_zone_R = null;
        cs.current_zone_L = null;
        cs.lerpstart = transform.position; /// Lerp For Reposition
        cs.rb.velocity = cs.transform.forward * (cs.velocity_raw - turn_speed_reduction); // Set velocity
        cs.cutscene = false;
        cs.not_detecting = false;
        cs.rb.isKinematic = false;
        cs.off_blink();
        GetComponent<BoxCollider>().enabled = false;
        GetComponent<BoxCollider>().enabled = true;
    }
}
