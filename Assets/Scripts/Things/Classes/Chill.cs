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
            thingStats.heatModifiers -= (thingStats.heatValue > 0) ? thingStats.heatValue - Mathf.Abs(thingStats.heatValue / 2) : 0;
            thingStats.weightModifiers += thingStats.weight;
        }
        else {
            duration = 0f;
        }
    }
}
