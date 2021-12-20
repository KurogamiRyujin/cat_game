using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//General stats all Things have.
[CreateAssetMenu(fileName = "Thing Stats", menuName = "Thing/Thing Stats")]
public class ThingStatsSO : ScriptableObject
{
    [Min(0f)]
    public float weight = 1f;
    [Min(0)]
    public int heatValue = 2;
    [Min(0f)]
    public float chillImmunityOnSpawnDuration = 1f;
}
