using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;

//Behaviour that displays a list of players.
public class PlayerListPanelBehaviour : MonoBehaviour
{
    [SerializeField] private PlayerListSO playerList = null;
    [SerializeField] private UIPrefabReference playerEntryButtonPrefab = null;
    [SerializeField] private GameObject contentPanel = null;
    [SerializeField] private GameObject playerListPanel = null;

    private List<PlayerEntryButton> playerEntryButtons;

    private void Awake() {
        playerEntryButtons = new List<PlayerEntryButton>();
    }

    public void ListPlayers() {
        ClearList();
        for(int i = 0; i < playerList.playerList.Count; i++) {
            StartCoroutine(SpawnPlayerEntryButtonCoroutine(playerList.playerList[i]));
        }
    }

    private IEnumerator SpawnPlayerEntryButtonCoroutine(PlayerData playerData) {
        AsyncOperationHandle<GameObject> async = playerEntryButtonPrefab.uiPrefabReference.InstantiateAsync(contentPanel.transform);
        yield return async;
        
        if(async.IsDone) {
            GameObject uiPrefab = async.Result;
            //Get the button component and add a listener onClick that deactivates the playerListPanel
            Button button = uiPrefab.GetComponent<Button>();
            button.onClick.AddListener(DeactivateParentPanel);

            //Get the PlayerEntryButton component and set its playerData
            PlayerEntryButton playerEntryButton = uiPrefab.GetComponent<PlayerEntryButton>();
            playerEntryButton.SetPlayerData(playerData);

            //Add the playerEntryButton to the playerEntryButton list
            playerEntryButtons.Add(playerEntryButton);
        }
    }

    private void ClearList() {
        for(int i = playerEntryButtons.Count-1; i >= 0; i--) {
            playerEntryButtons[i].Purge();
        }

        playerEntryButtons.Clear();
    }

    private void DeactivateParentPanel() {
        playerListPanel.SetActive(false);
    }
}
