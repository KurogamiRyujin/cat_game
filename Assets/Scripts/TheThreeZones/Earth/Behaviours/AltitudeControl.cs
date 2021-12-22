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
    [SerializeField] private VoidBroadcastChannelSO gameOverChannel;

    //Earth's Rigidbody
    private Rigidbody rb;
    //Earth's starting Vector3
    private Vector3 startingPos;

    //Modifier which adjusts the Earth's position in the world by its Y axis.
    private float altitudeModifier;
    
    private bool applyEruptionMod;
    private bool applyFreezeMod;
    private bool isGameOver;

    //Initialize values
    private void Awake() {
        altitudeModifier = 0f;
        rb = GetComponent<Rigidbody>();
        earthStats.altitude = 0f;
        gameObject.transform.position = new Vector3(gameObject.transform.position.x, earthStats.altitude, gameObject.transform.position.z);
        startingPos = gameObject.transform.position;
        isGameOver = false;
    }

    private void OnEnable() {
        //Register events
        eruptionChannel.onEventRaised += OnBoilerEruption;
        freezeChannel.onEventRaised += OnBoilerFreeze;
        normalChannel.onEventRaised += OnBoilerNormal;
        gameOverChannel.onEventRaised += OnGameOver;
    }

    private void OnDisable() {
        //Unregister events
        eruptionChannel.onEventRaised -= OnBoilerEruption;
        freezeChannel.onEventRaised -= OnBoilerFreeze;
        normalChannel.onEventRaised -= OnBoilerNormal;
        gameOverChannel.onEventRaised -= OnGameOver;
    }

    private void FixedUpdate() {
        AffectAltitude();
    }

    private void Update() {
        earthStats.altitude = gameObject.transform.position.y - startingPos.y;
        
        // if(isGameOver) {
        //     Vector3 stablePosition = new Vector3(transform.position.x, boilerStats.altitude, transform.position.z);
        //     transform.position = Vector3.Lerp(transform.position, stablePosition, Time.deltaTime);
        // }
    }

    private void AffectAltitude() {
        if(isGameOver) {
            rb.velocity = Vector3.Lerp(rb.velocity, Vector3.zero, Time.fixedDeltaTime);
            return;
        }
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

    private void OnGameOver() {
        isGameOver = true;
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
