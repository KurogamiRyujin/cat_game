using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Behaviour that saves the current player's gameplay progress into the device.
/// </summary>
public class ProgressSaver : MonoBehaviour
{
    [Header("Runtime Player Profile")]
    [SerializeField] private PlayerProfileSO runtimePlayerProfile = null;
    [Header("Channels listening to")]
    [SerializeField] private VoidBroadcastChannelSO gameOverChannel = null;

    private void OnEnable() {
        //Register events
        gameOverChannel.onEventRaised += SaveHiScore;
    }

    private void OnDisable() {
        //Unregister events
        gameOverChannel.onEventRaised -= SaveHiScore;
    }

    //Save HiScore to Player Data
    public void SaveHiScore() {
        if(runtimePlayerProfile.playerData != null && runtimePlayerProfile.playerData.playerName != "") {
            runtimePlayerProfile.playerData.hiScore = runtimePlayerProfile.scoreStats.HiScore();
            PlayerDataManager.SavePlayerData(runtimePlayerProfile.playerData);
        }
    }
}
