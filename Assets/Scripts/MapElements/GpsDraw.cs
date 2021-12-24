using System.Collections.Generic;
using UnityEngine;

public class GpsDraw : MonoBehaviour
{
    [SerializeField]
    LineRenderer linedraw;
    [SerializeField]
    GameObject anchor;
    [SerializeField]
    GameObject target;
    car_scripts car;
    int roadwidth = 15;
    List<GameObject> AnchorArray = new List<GameObject>();
    List<GameObject> route = new List<GameObject>();
    Dictionary<int, float> endpoints = new Dictionary<int, float>();
    Vector3 blockeddirections;
    void Start()
    {
        linedraw = GetComponent<LineRenderer>();
        car = FindObjectOfType<car_scripts>();
        recallibrate();
    }

    public void recallibrate()
    {
        raycasting();
        linedraw.positionCount = route.Count + 1;
        linedraw.SetPosition(linedraw.positionCount-1, car.transform.position - (car.transform.right * roadwidth/4));
        foreach (GameObject obj in route)
        {
            linedraw.SetPosition(route.IndexOf(obj),obj.transform.position + (Vector3.up * 2));
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            recallibrate();
        }

        linedraw.SetPosition(linedraw.positionCount-1, car.transform.position - (car.transform.right * roadwidth / 4));
    }

    void raycasting()
    {
        RaycastHit Hitinf = new RaycastHit();
        Ray cast = new Ray();
        int arrayindex = 0;
        int source_index = 0;
        int target_index = 0;
        Vector3 sourcedir = new Vector3();
        cast.origin = car.transform.position - (car.transform.right * roadwidth/4);
        cast.direction = car.transform.forward;
        int layermask = 1 << 8;
        endpoints.Clear();

        // Raycast Loop
        while(true)
        {
            //First Iteration
            if (arrayindex == 0)
            {
                Physics.Raycast(cast, out Hitinf, Mathf.Infinity, layermask);
                if (AnchorArray.Count >= 1)
                {
                    AnchorArray[0].transform.position = Hitinf.point + (car.transform.forward * roadwidth / 2);
                    AnchorArray[arrayindex].gameObject.SetActive(true);
                }
                else
                {
                    AnchorArray.Add(Instantiate(anchor, Hitinf.point + (car.transform.forward * roadwidth / 2), Quaternion.identity));
                }

                AnchorArray[0].GetComponent<GpsSensor>().value = Hitinf.distance + (Vector3.Distance(AnchorArray[0].transform.position,target.transform.position));
                AnchorArray[0].GetComponent<GpsSensor>().source_index = -1;

                directioncheck(AnchorArray[arrayindex].transform.position);


                source_index = 0;
                sourcedir = cast.direction;
                if (Hitinf.collider.gameObject == target)
                {
                    target_index = arrayindex;
                    break;
                }
                arrayindex++;
            }

            // Subsequent Iterations (Base this on the first iteration with extra ifs)

            if(blockeddirections.x != 1)
            {
                if(sourcedir != -Vector3.right) // Check if source
                {
                    cast.origin = AnchorArray[source_index].transform.position;
                    cast.direction = Vector3.right;

                    Physics.Raycast(cast, out Hitinf, Mathf.Infinity, layermask);
                    if (AnchorArray.Count >= arrayindex + 1)
                    {
                        AnchorArray[arrayindex].transform.position = Hitinf.point + (Vector3.right * roadwidth / 2);
                        AnchorArray[arrayindex].gameObject.SetActive(true);
                    }
                    else
                    {
                        AnchorArray.Add(Instantiate(anchor, Hitinf.point + (Vector3.right * roadwidth / 2), Quaternion.identity));
                    }

                    AnchorArray[arrayindex].GetComponent<GpsSensor>().value = Hitinf.distance + (Vector3.Distance(AnchorArray[arrayindex].transform.position, target.transform.position));
                    AnchorArray[arrayindex].GetComponent<GpsSensor>().source_index = source_index;
                    if (!endpoints.ContainsKey(arrayindex))
                    {
                        endpoints.Add(arrayindex, AnchorArray[arrayindex].GetComponent<GpsSensor>().value);
                    }

                    if (Hitinf.collider.gameObject == target)
                    {
                        target_index = arrayindex;
                        break;
                    }

                    arrayindex++;

                }               
            }

            if (blockeddirections.x != -1)
            {
                if (sourcedir != Vector3.right) // Check if source
                {
                    cast.origin = AnchorArray[source_index].transform.position;
                    cast.direction = Vector3.right * -1;

                    Physics.Raycast(cast, out Hitinf, Mathf.Infinity, layermask);
                    if (AnchorArray.Count >= arrayindex + 1)
                    {
                        AnchorArray[arrayindex].transform.position = Hitinf.point + (Vector3.right * -1 * roadwidth / 2);
                        AnchorArray[arrayindex].gameObject.SetActive(true);
                    }
                    else
                    {
                        AnchorArray.Add(Instantiate(anchor, Hitinf.point + (Vector3.right * -1 * roadwidth / 2), Quaternion.identity));
                    }

                    AnchorArray[arrayindex].GetComponent<GpsSensor>().value = Hitinf.distance + (Vector3.Distance(AnchorArray[arrayindex].transform.position, target.transform.position));
                    AnchorArray[arrayindex].GetComponent<GpsSensor>().source_index = source_index;
                    if (!endpoints.ContainsKey(arrayindex))
                    {
                        endpoints.Add(arrayindex, AnchorArray[arrayindex].GetComponent<GpsSensor>().value);
                    }

                    if (Hitinf.collider.gameObject == target)
                    {
                        target_index = arrayindex;
                        break;
                    }

                    arrayindex++;


                }
            }

            if (blockeddirections.z != 1)
            {
                if (sourcedir != -Vector3.forward) // Check if source
                {
                    cast.origin = AnchorArray[source_index].transform.position;
                    cast.direction = Vector3.forward;

                    Physics.Raycast(cast, out Hitinf, Mathf.Infinity, layermask);
                    if (AnchorArray.Count >= arrayindex + 1)
                    {
                        AnchorArray[arrayindex].transform.position = Hitinf.point + (Vector3.forward * roadwidth / 2);
                        AnchorArray[arrayindex].gameObject.SetActive(true);
                    }
                    else
                    {
                        AnchorArray.Add(Instantiate(anchor, Hitinf.point + (Vector3.forward * roadwidth / 2), Quaternion.identity));
                    }

                    AnchorArray[arrayindex].GetComponent<GpsSensor>().value = Hitinf.distance + (Vector3.Distance(AnchorArray[arrayindex].transform.position, target.transform.position));
                    AnchorArray[arrayindex].GetComponent<GpsSensor>().source_index = source_index;
                    if (!endpoints.ContainsKey(arrayindex))
                    {
                        endpoints.Add(arrayindex, AnchorArray[arrayindex].GetComponent<GpsSensor>().value);
                    }

                    if (Hitinf.collider.gameObject == target)
                    {
                        target_index = arrayindex;
                        break;
                    }

                    arrayindex++;
                }       
            }

            if (blockeddirections.z != -1)
            {
                if (sourcedir != Vector3.forward)
                {
                    cast.origin = AnchorArray[source_index].transform.position;
                    cast.direction = Vector3.forward * -1;

                    Physics.Raycast(cast, out Hitinf, Mathf.Infinity, layermask);
                    if (AnchorArray.Count >= arrayindex + 1)
                    {
                        AnchorArray[arrayindex].transform.position = Hitinf.point + (Vector3.forward * -1 * roadwidth / 2);
                        AnchorArray[arrayindex].gameObject.SetActive(true);
                    }
                    else
                    {
                        AnchorArray.Add(Instantiate(anchor, Hitinf.point + (Vector3.forward * -1 * roadwidth / 2), Quaternion.identity));
                    }

                    AnchorArray[arrayindex].GetComponent<GpsSensor>().value = Hitinf.distance + (Vector3.Distance(AnchorArray[arrayindex].transform.position, target.transform.position));
                    AnchorArray[arrayindex].GetComponent<GpsSensor>().source_index = source_index;
                    if (!endpoints.ContainsKey(arrayindex))
                    {
                        endpoints.Add(arrayindex, AnchorArray[arrayindex].GetComponent<GpsSensor>().value);
                    }

                    if (Hitinf.collider.gameObject == target)
                    {
                        target_index = arrayindex;
                        break;
                    }

                    arrayindex++;
                }                
            }

            endpoints.Remove(source_index);

            // Check lowest value of generated iterations

            source_index = getlowestindex();
            sourcedir = Vector3.Normalize((AnchorArray[source_index].transform.position - AnchorArray[AnchorArray[source_index].GetComponent<GpsSensor>().source_index].transform.position));
            directioncheck(AnchorArray[source_index].transform.position);

            if (arrayindex >= 62) /// Infinite loop breaker (63-ish is total number of turns)
            {
                print("Target not found");
                break;
            }
        }
        if(AnchorArray.Count > arrayindex + 1)
        {
            for (int i = arrayindex + 1; i < AnchorArray.Count;i++)
            {
                AnchorArray[i - 1].gameObject.SetActive(false);
            }
        }

        // Routing Loop
        route.Clear();
        while (target_index != -1)
        {
            route.Add(AnchorArray[target_index]);
            target_index = AnchorArray[target_index].GetComponent<GpsSensor>().source_index;
        }

    }

    // This will output the directions that are blocked in blockeddirections
    void directioncheck(Vector3 origin)
    {
        blockeddirections = Vector3.zero;

        int layer = 1 << 11;
        if(Physics.Raycast(origin,Vector3.forward, 20, layer))/// Z+
        {
            blockeddirections += Vector3.forward;
        }
        if (Physics.Raycast(origin, -Vector3.forward, 20, layer))/// Z-
        {           
            blockeddirections.z = -1;
        }
        if (Physics.Raycast(origin, Vector3.right, 20, layer))/// X+
        {
            blockeddirections += Vector3.right;
        }
        if (Physics.Raycast(origin, -Vector3.right, 20, layer))/// Z-
        {
            blockeddirections.x = -1;
        }
    }

    int getlowestindex()
    {
        int smallest = -1;
        foreach (int index in endpoints.Keys)
        {
            if (smallest == -1)
            {
                smallest = index;
            }
            else if(endpoints[index] < endpoints[smallest])
            {
                smallest = index;
            }
        }
        return smallest;
    }
    
}
