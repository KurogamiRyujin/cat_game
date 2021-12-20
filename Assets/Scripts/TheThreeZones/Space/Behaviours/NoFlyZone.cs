using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Like the Incinerator, destroys Things and the Earth.
//Since it will destroy Things before the Earth first due to how they are placed, the Earth can descend.
//However, in the case of an eruption, the force can push the Earth all the way.
public class NoFlyZone : BaseDestructionBehaviour
{
    protected override void OnTriggerEnter(Collider other) {
        IBanishable banishable = other.gameObject.GetComponent<IBanishable>();
        
        if(banishable != null) {
            banishable.Banish();
        }
        base.OnTriggerEnter(other);
    }
}
