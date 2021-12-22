using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BoilerTemperature;

//Rises and falls depending on the boiler's status and the total weight of the Things on it.
public class AltitudeControl : MonoBehaviour
{
    [Header("Boiler")]
    [SerializeField] private BoilerStatsSO boilerStats;
    [SerializeField] private BoilerConditionSO boilerCondition;

    //Float value representing what boiler temperature will have the Earth stay stready.
    [Header("Earth Stats")]
    [SerializeField] private EarthStatsSO earthStats;

    [Header("Channels listening to")]
    [SerializeField] private VoidBroadcastChannelSO eruptionChannel;
    [SerializeField] private VoidBroadcastChannelSO freezeChannel;
    [SerializeField] private VoidBroadcastChannelSO normalChannel;

    //Earth's Rigidbody
    private Rigidbody rb;
    //Earth's starting Vector3
    private Vector3 startingPos;

    //Modifier which adjusts the Earth's position in the world by its Y axis.
    private float altitudeModifier;
    
    private bool applyEruptionMod;
    private bool applyFreezeMod;

    //Initialize values
    private void Awake() {
        altitudeModifier = 0f;
        rb = GetComponent<Rigidbody>();
        earthStats.altitude = 0f;
        gameObject.transform.position = new Vector3(gameObject.transform.position.x, earthStats.altitude, gameObject.transform.position.z);
        startingPos = gameObject.transform.position;
    }

    private void OnEnable() {
        //Register events
        eruptionChannel.onEventRaised += OnBoilerEruption;
        freezeChannel.onEventRaised += OnBoilerFreeze;
        normalChannel.onEventRaised += OnBoilerNormal;
    }

    private void OnDisable() {
        //Unregister events
        eruptionChannel.onEventRaised -= OnBoilerEruption;
        freezeChannel.onEventRaised -= OnBoilerFreeze;
        normalChannel.onEventRaised -= OnBoilerNormal;
    }

    private void FixedUpdate() {
        AffectAltitude();
    }

    private void Update() {
        earthStats.altitude = this.gameObject.transform.position.y - startingPos.y;
    }

    private void AffectAltitude() {
        //Apply Temperature Modifiers
        //If boiler temperature is above neutralTemperaturePoint, push Earth up
        //If boiler temperature is below neutralTemperaturePoint, push Earth down
        //Otherwise, no force
        int temperature = boilerCondition.temperature;
        altitudeModifier = (temperature - earthStats.neutralTemperaturePoint) * earthStats.temperatureAltitudeDeltaScalerMod;
        
        //Apply Weight Modifiers
        //Weight pushes Earth down.
        altitudeModifier -= (earthStats.weightCarrying) * earthStats.weightAltitudeDeltaScalerMod;

        if(applyEruptionMod) {
            altitudeModifier += boilerStats.eruptionModifier;
        }

        if(applyFreezeMod) {
            altitudeModifier += boilerStats.freezeModifier;
        }
        
        //Implement Altitude Change
        Vector3 force = new Vector3(0f, altitudeModifier, 0f);
        rb.velocity = Vector3.Lerp(rb.velocity, force, Time.fixedDeltaTime);
    }

    private void OnBoilerEruption() {
        applyEruptionMod = true;
    }

    private void OnBoilerNormal() {
        applyEruptionMod = false;
        applyFreezeMod = false;
    }

    private void OnBoilerFreeze() {
        applyFreezeMod = true;
    }
}
