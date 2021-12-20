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
    // [SerializeField] protected float weight = 1f;
    // [SerializeField] protected int heatValue = 2;

    protected IWeight thingOnTop;

    protected virtual void Awake() {
        currentThingStats = new ThingStats();
        
    }

    protected virtual void Start() {
        currentThingStats.AddStatus(new ChillImmunity(thingStats.chillImmunityOnSpawnDuration));
    }

    protected virtual void InitStats() {
        currentThingStats.weight = thingStats.weight;
        currentThingStats.heatValue = thingStats.heatValue;
    }

    protected virtual void Update() {
        InitStats();
        PileCheck();
        UpdateWeight();
        currentThingStats.ApplyStatuses();
    }

    //Cast a ray above this object to see if there is an IWeight on top of it.
    private void PileCheck() {
        Ray ray = new Ray(gameObject.transform.position, Vector3.up);
        //Cast ray to check if there's an IWeight on top
        if(Physics.Raycast(ray, out RaycastHit hit)) {
            IWeight weightable = hit.transform.gameObject.GetComponent<IWeight>();
            
            thingOnTop = weightable;//assign IWeight as thingOnTop
        }
        else {//Otherwise, set thingOnTop to null
            thingOnTop = null;
        }
    }

    //Update this Thing's weight according to presence of thingOnTop
    private void UpdateWeight() {
        //If there is a thingOnTop, adjust it
        if(thingOnTop != null) {
            currentThingStats.weight = thingOnTop.GetWeight() + thingStats.weight;
        }//Otherwise if there is no thingOnTop, reset it
        else {
            ResetWeight();
        }
    }

    protected virtual void ResetWeight() {
        currentThingStats.weight = thingStats.weight;
    }
}
