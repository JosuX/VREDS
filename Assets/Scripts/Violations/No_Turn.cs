using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class No_Turn : MonoBehaviour, Iviolation_script
{
    violation_tracker vt;
    car_scripts car;
    [SerializeField]
    int invalid_turn; // Invalid turn -1 is left, 1 is right
    bool monitoring = true;

    public void Add_violation()
    {
        if (invalid_turn == 1)
        {
            vt.invalid_t("You made an invalid right turn at: " + car.transform.position.ToString());
        }
        else
        {
            vt.invalid_t("You made an invalid left turn at: " + car.transform.position.ToString());
        }
        monitoring = false;
    }

    public void Switch()
    {
        this.enabled = !this.enabled;
    }

    void Start()
    {
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
        if (car.turn_dir == invalid_turn && car.transform.eulerAngles.y == transform.eulerAngles.y)
        {
            Add_violation();
        }

    }
}
