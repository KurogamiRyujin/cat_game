using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vacuum : MonoBehaviour
{
    [Header("Strength sending to orbit")]
    [SerializeField] private float whooshStrength = 1500f;

    [Header("Channels broadcasting to")]
    [SerializeField] private SfxRequestBroadcasting sfxRequestBroadcasting;

    private void OnTriggerEnter(Collider collider) {
        GameObject thing = collider.gameObject;
        IBanishable banishable = thing.GetComponent<IBanishable>();

        if(thing != null) {
            Rigidbody thingRB = thing.GetComponent<Rigidbody>();

            if(thingRB != null) {
                if(thing.transform.position.y < transform.position.y) {
                    thingRB.AddForce(Vector3.up * whooshStrength);
                }
            }

            if(banishable != null) {
                banishable.Banish();
                sfxRequestBroadcasting.SfxRequestEventRaised(SfxSO.SFXType.BANISH);
            }
        }
    }
}
