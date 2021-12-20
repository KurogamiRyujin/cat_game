using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StatusEffect
{
    protected float duration;
    public abstract void ApplyStatus(ThingStats thingStats);
    public virtual bool DecayDuration() {
        duration -= Time.deltaTime;
        // Debug.Log("Duration " + duration);

        if(duration > 0) {
            return true;
        }
        else return false;
    }
}
