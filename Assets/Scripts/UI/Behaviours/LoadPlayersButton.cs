using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Button behaviour that loads the players from the persistent data path directory into a PlayerListSO
public class LoadPlayersButton : MonoBehaviour
{
    [SerializeField] private PlayerListSO playerList;

    private void Start() {
        OnClick();
    }

    public void OnClick() {
        List<PlayerData> playerDataList = new List<PlayerData>();

        playerDataList.AddRange(PlayerDataManager.LoadAllPlayers());

        if(playerDataList.Count > 0) {
            playerList.playerList = playerDataList;
        }
    }
}
