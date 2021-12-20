using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Stops pushables from moving further and knocks them down.
public class PushableStopperWall : MonoBehaviour
{
    private void OnTriggerExit(Collider collider) {
        IPushable pushable = collider.gameObject.GetComponent<IPushable>();

        if(pushable != null) {
            pushable.HoldStill();
        }
    }
}
