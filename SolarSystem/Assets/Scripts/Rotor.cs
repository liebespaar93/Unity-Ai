using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotor : MonoBehaviour
{
    public float AngleSpeed = 1.0f;
    // Update is called once per frame
    void Update()
    {
        var Angle = Time.deltaTime * AngleSpeed;
        transform.Rotate(0.0f, Angle, 0.0f);
    }

}
