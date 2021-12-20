using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BoilerTemperature;

public class Boiler : MonoBehaviour
{
    [SerializeField] private BoilerStatsSO boilerStats = null;

    [Header("Channels listening to")]
    [SerializeField] private TemperatureChangeBroadcasting temperatureChangeBroadcasting;

    [Header("Channels broadcasting to")]
    [SerializeField] private VoidBroadcastChannelSO eruptionChannel;
    [SerializeField] private VoidBroadcastChannelSO freezeChannel;
    [SerializeField] private VoidBroadcastChannelSO normalChannel;
    private void Awake() {
        gameObject.transform.position = new Vector3(gameObject.transform.position.x, boilerStats.altitude, gameObject.transform.position.z);
    }

    private void OnEnable() {
        //Register events
        temperatureChangeBroadcasting.onEventRaised += OnStatusChange;
    }

    private void OnStatusChange(Status status) {
        switch(status) {
            case Status.OVERHEAT:
            Eruption();
            break;
            case Status.COLD:
            Freeze();
            break;
            case Status.NORMAL:
            Normal();
            break;
        }
    }

    private void Normal() {
        if(normalChannel.onEventRaised != null) {
            normalChannel.onEventRaised();
        }
    }

    private void Eruption() {
        if(eruptionChannel.onEventRaised != null) {
            eruptionChannel.onEventRaised();
        }
    }

    private void Freeze() {
        if(freezeChannel.onEventRaised != null) {
            freezeChannel.onEventRaised();
        }
    }
}