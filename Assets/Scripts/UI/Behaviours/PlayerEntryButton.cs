using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Player Entry Button behaviour that sets the current player on click.
public class PlayerEntryButton : MonoBehaviour
{
    [SerializeField] private PlayerProfileSO runtimePlayerProfile = null;
    [SerializeField] private Text playerNameText = null;
    private PlayerData playerData = null;

    public void SetPlayerData(PlayerData playerData) {
        this.playerData = playerData;
        
        if(playerNameText != null) {
            playerNameText.text = this.playerData.playerName;
        }
    }

    public void OnClick() {
        if(playerData != null) {
            //Set Current Player
            runtimePlayerProfile.playerData = playerData;
            runtimePlayerProfile.scoreStats.hiScore = playerData.hiScore;
        }
    }

    public void Purge() {
        Destroy(gameObject);
    }
}
