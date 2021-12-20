using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Handles the current player status.
public class CurrentPlayer : MonoBehaviour
{
    [Header("Runtime Player Profile")]
    [SerializeField] private PlayerProfileSO runtimePlayerProfile = null;

    [Header("Channels listening to")]
    [SerializeField] private VoidBroadcastChannelSO gameOverChannel = null;

    private void Start() {
        //Reset runtime player profile at the start of the game.
        if(runtimePlayerProfile != null) {
            runtimePlayerProfile.playerData = null;
        }
    }

    private void OnEnable() {
        //Register events
        gameOverChannel.onEventRaised += SaveHiScore;
    }

    private void OnDisable() {
        //Unregister events
        gameOverChannel.onEventRaised -= SaveHiScore;
    }

    //Save HiScore to Player Data
    private void SaveHiScore() {
        runtimePlayerProfile.playerData.hiScore = runtimePlayerProfile.scoreStats.HiScore();
        PlayerDataManager.SavePlayerData(runtimePlayerProfile.playerData);
    }
}
