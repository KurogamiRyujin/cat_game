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
    [SerializeField] private float initialEruptionModifier = 5f;
    public float eruptionModifier = 5f;
    public float eruptionModifierModifier = 0f;
    [Header("How strong a freeze adds to the downward force for Earth")]
    [SerializeField] private float initialFreezeModifier = -5f;
    public float freezeModifier = -5f;
    public float freezeModifierModifier = 0f;

    public float InitialEruptionModifier() {
        return initialEruptionModifier;
    }

    public float InitialFreezeModifier() {
        return initialFreezeModifier;
    }
}
