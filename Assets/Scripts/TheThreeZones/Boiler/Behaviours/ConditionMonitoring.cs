using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BoilerTemperature;

//Behaviour monitoring the Boiler's condition and broadcasting events whenever certain conditions are met.
public class ConditionMonitoring : MonoBehaviour
{
    [Header("Boiler Condition")]
    [SerializeField] private BoilerConditionSO boilerCondition;

    [Header("Channels broadcasting to")]
    [SerializeField] private TemperatureChangeBroadcasting temperatureChangeBroadcasting;
    [SerializeField] private FloatBroadcastingChannel lavaChangeColorChannel;
    
    //Boiler's current status.
    private Status boilerStatus;

    private void Start() {
        //Initiate Boiler's current status.
        boilerCondition.temperature = boilerCondition.Initialtemperature;
        boilerStatus = CheckBoilerCondition(boilerCondition.temperature);
    }

    private void Update() {
        //Check boiler's condition and update its current status.
        //If there was a change in status, raise the corresponding event.
        Status status = CheckBoilerCondition(boilerCondition.temperature);
        CheckTemperatureWarningDifference();
        if(boilerStatus != status) {
            OnStatusChange(status);
            boilerStatus = status;
        }
    }

    //Based on the temperature, check the boiler status.
    private Status CheckBoilerCondition(int temperature) {
        if(temperature > boilerCondition.overheatPoint) {
            // Debug.Log("Boiler Overheat");
            return Status.OVERHEAT;
        }
        else if(temperature < boilerCondition.coldPoint) {
            // Debug.Log("Boiler Cold");
            return Status.COLD;
        }
        else {
            // Debug.Log("Boiler Normal");
            return Status.NORMAL;
        }
    }

    private void CheckTemperatureWarningDifference() {
        float percent = 0f;
        float coldAndOverheatMean = (boilerCondition.coldPoint + boilerCondition.overheatPoint) / 2f;
        float meanAndTailEndDifference = boilerCondition.overheatPoint - coldAndOverheatMean;
        float temperatureAndMeanDifference = boilerCondition.temperature - coldAndOverheatMean;

        percent = temperatureAndMeanDifference / meanAndTailEndDifference;
        lavaChangeColorChannel.FloatEventRaised(percent);
    }

    private void OnStatusChange(Status status) {
        if(temperatureChangeBroadcasting != null) {
            temperatureChangeBroadcasting.TemperatureChangeEventRaised(status);
        }
    }
}
