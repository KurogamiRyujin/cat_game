using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Listens to any request to exit the game.
public class GameExitListener : MonoBehaviour
{
    [Header("Channel listening to")]
    [SerializeField] private VoidBroadcastChannelSO gameExitChannel;

    private void OnEnable() {
        //Register to Events
        gameExitChannel.onEventRaised += ExitGame;
    }

    private void OnDisable() {
        //Unregister to Events
        gameExitChannel.onEventRaised -= ExitGame;
    }

    private void ExitGame() {
        Application.Quit();
    }
}
