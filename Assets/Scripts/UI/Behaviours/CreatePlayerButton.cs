using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Button behaviour that creates a player on click. Upon creation, sets the created player as the current player.
public class CreatePlayerButton : ActivatableButton
{
    [SerializeField] private PlayerProfileSO runtimePlayerProfile = null;
    [SerializeField] private PlayerListSO playerList = null;
    [SerializeField] private InputField playerNameField = null;

    private void Start() {
        OnInputEdit();
    }

    public void CreatePlayer() {
        if(playerNameField != null) {
            string playerName = playerNameField.text;

            //Check if the playerName already exists.
            if(!PlayerDataManager.CheckUsernameExists(playerName)) {
                //If not, create new player.
                PlayerDataManager.CreatePlayerData(playerName);
                PlayerData playerData = new PlayerData();
                playerData.playerName = playerName;
                playerData.hiScore = 0;

                //Then, set them as current player.
                runtimePlayerProfile.playerData = playerData;
                runtimePlayerProfile.scoreStats.hiScore = playerData.hiScore;

                //Then, add a new player into the playerList
                playerList.playerList.Add(playerData);
            }

            //Empty the input field.
            playerNameField.text = "";
        }
    }

    public void OnInputEdit() {
        if(playerNameField != null) {
            if(PlayerDataManager.CheckUsernameExists(playerNameField.text) || playerNameField.text == "") {
                Deactivate();
            }
            else {
                Activate();
            }
        }
    }
}
