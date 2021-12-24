using UnityEngine;
using TMPro;

public class car_scripts : MonoBehaviour
{
    input_detection input;
    Animation anim;
    Transform car;
    road_obj current_road;
    violation_tracker vt;
    public Rigidbody rb;
    public int current_axis = 1; /// 1 if in z axis and -1 if in x axis /// SET AT CURRENT STARTING AXIS
    //[HideInInspector]
    public int turn_dir;// 1 if Right, -1 if Left
    float i = 0;
    float target_path;
    float front_offset;
    [SerializeField]
    float snapspeed = 1f;
    float snapmodifier = 1;
    [SerializeField]
    TextMeshProUGUI speed_dis;
    int turn_max_speed = 33; // for detecting speed when turning
    int speed_diff_lim = 15; // this is for detecting sudden brakes
    public float velocity_raw;
    public float velocity_norm; // Velocity in Units/hour
    float speed_update_delay=1;
    float delay_timer;
    public Vector3 lerpstart;
    public int acceleration = 20;
    public turn_zone_obj current_zone_R = null;
    public turn_zone_obj current_zone_L = null;
    public bool not_detecting = false;
    carsounds csound;

    public GameObject blinker;
    [SerializeField] GameObject b_l, b_r;
    [SerializeField] Material b_on, b_off;
    float blinkspeed = 0.3f;
    float b_counter;
    GameObject activeblink;

    public bool cutscene;

    // Start is called before the first frame update
    void Start()
    {
        input = GetComponent<input_detection>();
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animation>();
        car = transform.GetChild(0);
        lerpstart = transform.position; /// Lerp for first run
        front_offset = transform.InverseTransformPoint(this.gameObject.GetComponent<Collider>().bounds.center).z;
        vt = FindObjectOfType<violation_tracker>();
        delay_timer = speed_update_delay;
        csound = FindObjectOfType<carsounds>();

        car.localPosition = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        if (blinker != null)
        {
            on_blink(blinker);
        }

        if (cutscene)
        {
            if (i == 1)
            {
                if(turn_dir == 1)
                {
                    turn(current_zone_R.zone);
                }
                else
                {
                    turn(current_zone_L.zone);
                } 
            }
            return;
        }

        ////////////////////////////////////////////////// CUTSCENE DIVISOR /////////////////////

        // INPUT SECTION START /// Accelerate and Brake are in FixedUpdate()

        if (input.right || input.horizontal == 1) ///// PUT CODES HERE FOR THINGS TO SETUP BEFORE pre_animation()
        {
            if (current_zone_R)
            {
                turn_dir = 1;
                cutscene = true;
                i = 0; // Reset i variable for use in pre_animation()
                lerpstart = transform.position; // Reset lerpstart for pre_animation()
                if (velocity_norm >= turn_max_speed)
                {
                    vt.min("You were going a bit too fast on that turn: " + transform.position);
                }

                if (activeblink != b_r)
                {
                    vt.min("You did not use a right turn blinker here: " + transform.position);
                }

                /// SPEED CHANGE CODE for variable snapspeed for pre_animation()
            }
            else if(current_zone_L)
            {
                print("Right turn lang dyan");
                //Wrong Turn R Animation//
            }

            else
            {
                print("WALANG LIKUAN DYAN!!!");
                //SWERVE ANIMATION R//
            }
        }

        if (input.left || input.horizontal == -1)
        {
            if (current_zone_L)
            {
                turn_dir = -1;
                cutscene = true;
                i = 0; // Reset i variable for use in pre_animation()
                lerpstart = transform.position; // Reset lerpstart for pre_animation()
                if (velocity_norm >= turn_max_speed)
                {
                    vt.min("You were going a bit too fast on that turn: " + transform.position);
                }

                if (activeblink != b_l)
                {
                    vt.min("You did not use a left turn blinker here: " + transform.position);
                }
                /// SPEED CHANGE CODE for variable snapspeed for pre_animation()
            }
            else if (current_zone_R)
            {
                print("left turn lang dyan");
                //// Wrong Turn L Animation///
            }
            else
            {
                print("WALANG LIKUAN DYAN!!!");
                ///Swerve Animation L//
            }
        }
        
        if (Input.GetKeyDown(input.signal_l) || Input.GetKeyDown(input.signal_l_2))
        {
            if(blinker == b_l)
            {
                off_blink();
            }
            else
            {
                blinker = b_l;
                b_counter = blinkspeed;
                csound.blinksound();
            }
        }

        if (Input.GetKeyDown(input.signal_r) || Input.GetKeyDown(input.signal_r_2))
        {
            if (blinker == b_r)
            {
                off_blink();
            }
            else
            {
                blinker = b_r;
                b_counter = blinkspeed;
                csound.blinksound();
            }
        }

    }

    private void FixedUpdate() //// ALL "Physics" based movement is put here /////
    {
        if (cutscene)
        {
            if (!anim.isPlaying)
            {
                if (i != 1)
                {
                    pre_animation(turn_dir);
                }

            }
            return;
        }

        ////////////////////////////////////////////////// CUTSCENE DIVISOR /////////////////////

        reposition();

        if (input.accelerate || input.accelerate_2 == 1)
        {
            rb.AddRelativeForce(new Vector3(0, 0, acceleration), ForceMode.Acceleration);
        }

        if (input.brake || input.brake_2 == 1)
        {
            if (transform.InverseTransformVector( rb.velocity).z > 0)
            {
                rb.AddRelativeForce(new Vector3(0, 0, -acceleration), ForceMode.Acceleration);
            }
            else
            {
                rb.velocity = Vector3.zero;
            }
            
        }

        /// SPEED MONITOR SECTION///
        velocity_raw = rb.velocity.magnitude;

        if (delay_timer >= speed_update_delay)
        {
            float last_vel = velocity_norm;
            velocity_norm = (velocity_raw / Time.deltaTime) / 3600 * 100;
            delay_timer = 0;
            speed_dis.text = Mathf.RoundToInt(velocity_norm).ToString();
            if(last_vel - velocity_norm >= speed_diff_lim )
            {
                vt.min("Why did you suddenly slow down? : " + transform.position);
            }          
        }
        else
        {
            delay_timer += Time.deltaTime;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (cutscene) ////////////////////////////////////////////////// CUTSCENE DIVISOR /////////////////////
        {
            return;
        }

        if (other.CompareTag("road"))
        {
            if (not_detecting)
            {
                return;
            }

            if (current_road)
            {
                if (other.gameObject == current_road.gameObject)
                {
                    return;
                }
            }

            current_road = other.gameObject.GetComponent<road_obj>();
            if (current_axis == 1) // car in Z axis //
            {
                if (transform.forward.z > 0)// Car facing towards positive Z //
                {
                    target_path = current_road.target_path;
                }
                else // Car facing towards negative Z //
                {
                    target_path = current_road.target_opposite;
                }
            }

            else // car in X axis //
            {
                if (transform.forward.x > 0) // Car facing towards positive x //
                {
                    target_path = current_road.target_path;
                }
                else // Car facing towards negative x//
                {
                    target_path = current_road.target_opposite;
                }

             
            }

            not_detecting = true;
            FindObjectOfType<GpsDraw>().recallibrate();

            
        }

        if (other.CompareTag("Destination"))
        {
            FindObjectOfType<Game_Master>().EndRun(true);
        }

        if (other.CompareTag("turn_zone_R"))
        {
            if (other.gameObject.transform.eulerAngles.y == transform.eulerAngles.y)
            {
                current_zone_R = other.gameObject.GetComponent<turn_zone_obj>();
                if(current_zone_R.zone == 1)
                {
                    activeblink = blinker;
                }
            }
        }

        if (other.CompareTag("turn_zone_L"))
        {
            if (other.gameObject.transform.eulerAngles.y == transform.eulerAngles.y)
            {
                current_zone_L = other.gameObject.GetComponent<turn_zone_obj>();
                if (current_zone_L.zone == 1)
                {
                    activeblink = blinker;
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (cutscene)
        {
            return;
        }

        if (other.CompareTag("turn_zone_R") && current_zone_R)
        {
            if(other.GetComponent<turn_zone_obj>().zone == 3)
            {
                current_zone_R = null;
                activeblink = null;
            }
        }

        if (other.CompareTag("turn_zone_L") && current_zone_L)
        {
            if (other.GetComponent<turn_zone_obj>().zone == -3)
            {
                current_zone_L = null;
                activeblink = null;
            }
        }
    }

    private void turn(int dir) /// Function for playing animation
    {
        rb.isKinematic = true;
        if(dir == 1)
        {
            anim.Play("Right1", PlayMode.StopAll);
        }

        if (dir == 2)
        {
            anim.Play("Right2", PlayMode.StopAll);
        }

        if (dir == 3)
        {
            anim.Play("Right3", PlayMode.StopAll);
        }

        if (dir == -1)
        {
            anim.Play("Left1", PlayMode.StopAll);
        }

        if (dir == -2)
        {
            anim.Play("Left2", PlayMode.StopAll);
        }

        if (dir == -3)
        {
            anim.Play("Left3", PlayMode.StopAll);
        }

        //// this part runs BEFORE ANIMATIONS
        current_axis *= -1; /// Change Axis ///
        i = 0; /// Reset Repositioning value //
    }

    private void reposition() // Function for putting player in right place of the road
    {
        if (current_axis == 1)
        {
            if (transform.position.x != target_path)
            {
                transform.position = Vector3.Lerp(new Vector3(lerpstart.x,transform.position.y,transform.position.z), new Vector3(target_path, transform.position.y, transform.position.z), i);
                i += snapspeed * Time.deltaTime;
                i = Mathf.Clamp(i, 0, 1);
            }
        }

        if (current_axis == -1)
        {
            if (transform.position.z != target_path)
            {
                transform.position = Vector3.Lerp(new Vector3(transform.position.x, transform.position.y, lerpstart.z), new Vector3(transform.position.x, transform.position.y, target_path), i);
                i += snapspeed * Time.deltaTime;
                i = Mathf.Clamp(i, 0, 1);
            }
        }
    }

    private void pre_animation(int dir) /// DIR 1 if right, -1 if left    // Function for putting player in correct place before animation
    {
        if(current_axis == 1)
        {
            if(dir == 1)
            {
                transform.position = Vector3.Lerp(new Vector3(transform.position.x, transform.position.y, lerpstart.z), new Vector3(transform.position.x, transform.position.y, current_zone_R.target_spot - (front_offset * transform.forward.z)), i);
                i += snapspeed * Time.deltaTime * snapmodifier;
                i = Mathf.Clamp(i, 0, 1);
            }

            if (dir == -1)
            {
                transform.position = Vector3.Lerp(new Vector3(transform.position.x, transform.position.y, lerpstart.z), new Vector3(transform.position.x, transform.position.y, current_zone_L.target_spot - (front_offset * transform.forward.z)), i);
                i += snapspeed * Time.deltaTime * snapmodifier;
                i = Mathf.Clamp(i, 0, 1);
            }

        }
        
        if(current_axis == -1)
        {
            if(dir == 1)
            {
                transform.position = Vector3.Lerp(new Vector3(lerpstart.x, transform.position.y, transform.position.z), new Vector3(current_zone_R.target_spot - (front_offset * transform.forward.x), transform.position.y, transform.position.z), i);
                i += snapspeed * Time.deltaTime * snapmodifier;
                i = Mathf.Clamp(i, 0, 1);
            }

            if (dir == -1)
            {
                transform.position = Vector3.Lerp(new Vector3(lerpstart.x, transform.position.y, transform.position.z), new Vector3(current_zone_L.target_spot - (front_offset * transform.forward.x), transform.position.y, transform.position.z), i);
                i += snapspeed * Time.deltaTime * snapmodifier;
                i = Mathf.Clamp(i, 0, 1);
            }

        }
    }

    private void on_blink(GameObject arrow)
    {
        if (b_counter >= blinkspeed)
        {
            if(arrow.GetComponent<MeshRenderer>().sharedMaterial == b_on)
            {
                arrow.GetComponent<MeshRenderer>().sharedMaterial = b_off;
            }
            else
            {
                arrow.GetComponent<MeshRenderer>().sharedMaterial = b_on;
            }
            b_counter = 0;
        }
        else
        {
            b_counter += Time.deltaTime;
        }
    }

    public void off_blink()
    {
        blinker = null;
        b_l.GetComponent<MeshRenderer>().sharedMaterial = b_off;
        b_r.GetComponent<MeshRenderer>().sharedMaterial = b_off;
        b_counter = blinkspeed;
        activeblink = null;
        csound.blinkoff();
    }

}
