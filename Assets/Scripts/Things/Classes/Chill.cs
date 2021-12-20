using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chill : StatusEffect
{
    public Chill(float duration) {
        this.duration = duration;
    }

    public override void ApplyStatus(ThingStats thingStats)
    {
        if(!thingStats.CheckStatusFor(typeof(ChillImmunity))) {
            thingStats.heatValue = thingStats.heatValue - Mathf.Abs(thingStats.heatValue / 2);
            thingStats.weight *= 2;
        }
        else {
            duration = 0f;
        }
    }
}
