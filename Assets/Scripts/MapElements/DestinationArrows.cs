using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestinationArrows : MonoBehaviour
{
    [SerializeField] Transform arrow, text;
    float i = 0;
    Vector3 position;

    private void Start()
    {
        position = arrow.position;
    }
    void Update()
    {
        text.Rotate(0, 3, 0);
        arrow.position = position + (Vector3.up * Mathf.Sin(i));
        i += Time.deltaTime;
    }
}
