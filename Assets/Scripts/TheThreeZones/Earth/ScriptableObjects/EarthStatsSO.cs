using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Earth Stats", menuName = "ThreeZones/Earth/Stats")]
public class EarthStatsSO : ScriptableObject
{
    [Header("Inherent Stats")]
    //Earth's current altitude based on the Y axis.
    public float altitude;
    //Boiler temperature considered to have no effect on the Earth's altitude.
    public float neutralTemperaturePoint = 50f;

    [Header("External Factors")]
    [Min(0f)]
    //Total weight Earth is carrying currently
    public float weightCarrying;
    
    //Modifiers that scales how much the boiler temperature and total weight of Things on the Earth affect the Earth's altitude change.
    [Header("Altitude Delta Modifiers")]
    [Range(0.01f, 1f)]
    public float temperatureAltitudeDeltaScalerMod = 1f;
    [Range(0.01f, 1f)]
    public float weightAltitudeDeltaScalerMod = 1f;
}
