using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stoplight : MonoBehaviour
{
    public bool go_z, go_x;
    private float timer;
    [SerializeField]
    private float time;
    [SerializeField]
    private float yellow_time;

    public List<MeshRenderer> Red_Z = new List<MeshRenderer>();
    public List<MeshRenderer> Yellow_Z = new List<MeshRenderer>();
    public List<MeshRenderer> Green_Z = new List<MeshRenderer>();

    public List<MeshRenderer> Red_X = new List<MeshRenderer>();
    public List<MeshRenderer> Yellow_X = new List<MeshRenderer>();
    public List<MeshRenderer> Green_X = new List<MeshRenderer>();

    public Material Off, Green, Yellow, Red;

    private void Start()
    {
        go_z = true;
        go_x = false;
        timer = time;
    }

    private void Update()
    {
        timer += Time.deltaTime;

        // Behavior for red and green only
        if (timer >= time)
        {
            go_z = !go_z;
            go_x = !go_x;

            if (go_z)
            {
                go('z');
                stop('x');
            }

            if (go_x)
            {
                go('x');
                stop('z');
            }

            timer = 0;
        }

        // Behavior for yellow light

        if (timer >= time-yellow_time)
        {
            slow('x');
            slow('z');
        }
    }

    public void go(char dir)
    {
        if (dir == 'x')
        {
            List<MeshRenderer> other = new List<MeshRenderer>();
            other.AddRange(Red_X);
            other.AddRange(Yellow_X);

            foreach (MeshRenderer elem in other)
            {
                elem.material = Off;
            }


            foreach (MeshRenderer elem in Green_X)
            {
                elem.material = Green;
            }

        }
        else
        {
            List<MeshRenderer> other = new List<MeshRenderer>();
            other.AddRange(Red_Z);
            other.AddRange(Yellow_Z);

            foreach (MeshRenderer elem in other)
            {
                elem.material = Off;
            }

            foreach (MeshRenderer elem in Green_Z)
            {
                elem.material = Green;
            }
        }

    }

    public void slow(char dir)
    {
        if (dir == 'x')
        {
            List<MeshRenderer> other = new List<MeshRenderer>();
            other.AddRange(Red_X);
            other.AddRange(Green_X);

            foreach (MeshRenderer elem in other)
            {
                elem.material = Off;
            }

            foreach (MeshRenderer elem in Yellow_X)
            {
                elem.material = Yellow;
            }

        }
        else
        {
            List<MeshRenderer> other = new List<MeshRenderer>();
            other.AddRange(Red_Z);
            other.AddRange(Green_Z);

            foreach (MeshRenderer elem in other)
            {
                elem.material = Off;
            }

            foreach (MeshRenderer elem in Yellow_Z)
            {
                elem.material = Yellow;
            }
        }
    }

    public void stop(char dir)
    {
        if (dir == 'x')
        {
            List<MeshRenderer> other = new List<MeshRenderer>();
            other.AddRange(Yellow_X);
            other.AddRange(Green_X);

            foreach (MeshRenderer elem in Red_X)
            {
                elem.material = Red;
            }

            foreach (MeshRenderer elem in other)
            {
                elem.material = Off;
            }

        }
        else
        {
            List<MeshRenderer> other = new List<MeshRenderer>();
            other.AddRange(Yellow_Z);
            other.AddRange(Green_Z);

            foreach (MeshRenderer elem in Red_Z)
            {
                elem.material = Red;
            }

            foreach (MeshRenderer elem in other)
            {
                elem.material = Off;
            }
        }
    }

}
