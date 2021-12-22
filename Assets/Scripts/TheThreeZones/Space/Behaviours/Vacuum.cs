using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vacuum : MonoBehaviour
{
    [Header("Strength sending to orbit")]
    [SerializeField] private float whooshStrength = 1500f;
    private void OnTriggerEnter(Collider collider) {
        GameObject thing = collider.gameObject;

        if(thing != null) {
            Rigidbody thingRB = thing.GetComponent<Rigidbody>();

            if(thingRB != null) {
                if(thing.transform.position.y < transform.position.y) {
                    thingRB.AddForce(Vector3.up * whooshStrength);
                }
            }
        }
    }
}
