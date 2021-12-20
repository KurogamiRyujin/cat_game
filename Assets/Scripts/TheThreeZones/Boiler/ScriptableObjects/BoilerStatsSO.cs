using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Boiler Stats", menuName = "ThreeZones/Boiler/Stats")]
public class BoilerStatsSO : ScriptableObject
{
    //Actually really just the Y value of their position
    [Header("Boiler Altitude")]
    public float altitude = -8f;

    [Header("How strong an eruption adds to the upward force for Earth")]
    public float eruptionModifier = 50f;
    [Header("How strong a freeze adds to the downward force for Earth")]
    public float freezeModifier = -50f;
}
