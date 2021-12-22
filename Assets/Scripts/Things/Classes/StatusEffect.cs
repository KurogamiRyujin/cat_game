using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StatusEffect
{
    protected float duration;
    protected bool isInfinite;
    public abstract void ApplyStatus(ThingStats thingStats);
    public virtual bool DecayDuration() {
        if(isInfinite) {
            return true;
        }
        duration -= Time.deltaTime;
        // Debug.Log("Duration " + duration);

        if(duration > 0) {
            return true;
        }
        else return false;
    }
}
