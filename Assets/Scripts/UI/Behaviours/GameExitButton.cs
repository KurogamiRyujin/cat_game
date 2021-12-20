using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Button to request a game exit.
public class GameExitButton : MonoBehaviour
{
    [Header("Channel to broadcast to")]
    [SerializeField] private VoidBroadcastChannelSO gameExitChannel;

    public void OnButtonPress() {
        if(gameExitChannel.onEventRaised != null) {
            gameExitChannel.onEventRaised();
        }
    }
}
