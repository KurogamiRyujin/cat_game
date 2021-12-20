using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Burn : BaseDestructionBehaviour
{
    //Raises a Burn event which increases the Boiler's gushing power.
    [Header("Boiler Condition")]
    [SerializeField] private BoilerConditionSO boilerCondition;

    [Header("Channels broadcasting to")]
    [SerializeField] private SfxRequestBroadcasting burnSFXChannel;

    protected override void OnTriggerEnter(Collider other) {
        //Temperature value increase will depend on the object burned.

        IBurnable burnable = other.GetComponent<IBurnable>();
        if(burnable != null) {
            burnSFXChannel.SfxRequestEventRaised(SfxSO.SFXType.BURN);
            burnable.Burn();
        }
        base.OnTriggerEnter(other);
    }
}
