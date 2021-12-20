using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Orbit movement around an axis point.
public class Orbit : MonoBehaviour
{
    public GameObject axisPoint;
    [Min(1f)]
    public float speed = 1f;

    private void Update() {
        if(axisPoint != null) {
            gameObject.transform.RotateAround(axisPoint.transform.position, axisPoint.transform.up, speed * Time.deltaTime);
        }
    }
}
