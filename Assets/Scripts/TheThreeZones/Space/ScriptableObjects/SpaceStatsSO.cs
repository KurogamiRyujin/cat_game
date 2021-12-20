using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Space Stats", menuName = "ThreeZones/Space/Stats")]
public class SpaceStatsSO : ScriptableObject
{
    [Header("Space Altitude")]
    public float altitude = 8f;
}
