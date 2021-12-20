using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BoilerTemperature {

    //Enum for the Boiler's possible status.
    public enum Status {
        NORMAL,
        OVERHEAT,
        COLD
    }

    //Broadcasting channel for temperature change.
    [CreateAssetMenu(fileName = "Temperature Change Channel", menuName = "Broadcasting/Temperature Change Channel")]
    public class TemperatureChangeBroadcasting : BroadcastChannelSO<Status> {
        public void TemperatureChangeEventRaised(Status status) {
            if(onEventRaised != null) {
                onEventRaised(status);
            }
        }
    }
}