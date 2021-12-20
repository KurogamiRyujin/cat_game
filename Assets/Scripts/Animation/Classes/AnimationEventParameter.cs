using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Parameters for animation control.
/// </summary>
[Serializable]
public class AnimationEventParameter
{
    public string animationEventName;
    public bool boolValue;

    public AnimationEventParameter(string animationEventName, bool boolValue) {
        this.animationEventName = animationEventName;
        this.boolValue = boolValue;
    }
}
