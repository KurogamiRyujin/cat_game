using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Boiler Condition", menuName = "ThreeZones/Boiler/Condition")]
public class BoilerConditionSO : ScriptableObject
{
    [Header("Boiler Temperature")]
    public int temperature = 0;
    [Header("Overheat Point")]
    public int overheatPoint = 0;
    [Header("Cold Point")]
    public int coldPoint = 0;
}
