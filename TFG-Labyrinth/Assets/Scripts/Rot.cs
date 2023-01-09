using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rot : MonoBehaviour
{
    public float rotVel = 30f;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, rotVel * Time.deltaTime, 0);
    }
}
