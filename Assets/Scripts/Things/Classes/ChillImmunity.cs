using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChillImmunity : StatusEffect
{
    public ChillImmunity(float duration) {
        this.duration = duration;
        isInfinite = false;
    }

    public ChillImmunity() {
        isInfinite = true;
    }

    public override void ApplyStatus(ThingStats thingStats)
    {
    }
}
