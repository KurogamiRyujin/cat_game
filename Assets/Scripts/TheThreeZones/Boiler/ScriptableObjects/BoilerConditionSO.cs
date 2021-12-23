using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Boiler Condition", menuName = "ThreeZones/Boiler/Condition")]
public class BoilerConditionSO : ScriptableObject
{
    [Header("Boiler Temperature")]
    [SerializeField] private int initialTemperature = 50;
    public int temperature = 0;
    public int Initialtemperature {
        get {
            return initialTemperature;
        }
    }

    [Header("Overheat Point")]
    public int overheatPoint = 0;
    [Header("Cold Point")]
    public int coldPoint = 0;
}
