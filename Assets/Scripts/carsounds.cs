using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class carsounds : MonoBehaviour
{
    [SerializeField] AudioSource car, blinker, crash;
    car_scripts cs;
    float pitch;
    float reference = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        cs = FindObjectOfType<car_scripts>();
        car.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (cs.cutscene)
        {
            pitch = Mathf.SmoothDamp(pitch, 0.4f, ref reference, 0.5f);
        }
        else
        {
            pitch = Mathf.SmoothDamp(pitch, (cs.rb.velocity.magnitude / 35) * 1.5f, ref reference, 0.5f);
        }
        car.pitch = pitch + 1;
    }

    public void blinksound()
    {
        blinker.Play();
    }

    public void blinkoff()
    {
        blinker.Stop();
    }

    public void crashplay()
    {
        crash.Play();
    }

}
