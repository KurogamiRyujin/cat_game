using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Stats for VolcanoThing.
/// </summary>
[CreateAssetMenu(fileName = "Volcano Stats", menuName = "Thing/VolcanoThing Stats")]
public class VolcanoStatsSO : ThingStatsSO
{
    public float eruptionModifierModifier;
    public int postEruptionCooling = -7;
    public float eruptionDuration = 5f;
}
