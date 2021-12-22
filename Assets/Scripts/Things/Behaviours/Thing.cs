using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//The Things that the Earth carries.
public abstract class Thing : MonoBehaviour
{
    [Header("General Thing Stats")]
    [SerializeField] protected ThingStatsSO thingStats;
    [Header("Current Thing Stats")]
    [SerializeField] protected ThingStats currentThingStats;

    [SerializeField] protected Transform pileCheckStartingPoint;

    protected IWeight thingOnTop;

    protected virtual void Start() {
        currentThingStats.AddStatus(new ChillImmunity(thingStats.chillImmunityOnSpawnDuration));
        InitStats();
    }

    protected virtual void InitStats() {
        currentThingStats.weightModifiers = 0;
        currentThingStats.heatModifiers = 0;
        currentThingStats.weight = thingStats.weight;
        currentThingStats.heatValue = thingStats.heatValue;
    }

    protected virtual void Update() {
        //Refresh current stats
        currentThingStats.weight = thingStats.weight;
        currentThingStats.heatValue = thingStats.heatValue;
        
        currentThingStats.ApplyStatuses();
        PileCheck();
        UpdateWeight();
        UpdateHeatValue();

        //Reset modifiers
        currentThingStats.weightModifiers = 0;
        currentThingStats.heatModifiers = 0;
    }

    //Cast a ray above this object to see if there is an IWeight on top of it.
    private void PileCheck() {
        Ray ray = new Ray(pileCheckStartingPoint.position, Vector3.up);
        //Cast ray to check if there's an IWeight on top
        if(Physics.Raycast(ray, out RaycastHit hit)) {
            IWeight weightable = hit.transform.gameObject.GetComponent<IWeight>();
            
            thingOnTop = weightable;//assign IWeight as thingOnTop
        }
        else {//Otherwise, set thingOnTop to null
            thingOnTop = null;
        }

        //If there is a thingOnTop, adjust weight modifier
        if(thingOnTop != null) {
            currentThingStats.weightModifiers += thingOnTop.GetWeight();
        }
    }

    /// <summary>
    /// Update this Thing's weight based on the modifiers.
    /// </summary>
    private void UpdateWeight() {
        currentThingStats.weight = thingStats.weight + currentThingStats.weightModifiers;
    }

    /// <summary>
    /// Update this Thing's heatValue based on modifiers.
    /// </summary>
    private void UpdateHeatValue() {
        currentThingStats.heatValue = thingStats.heatValue + currentThingStats.heatModifiers;
    }
}
