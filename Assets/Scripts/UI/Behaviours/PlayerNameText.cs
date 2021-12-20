using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerNameText : MonoBehaviour
{
    [SerializeField] private PlayerProfileSO runtimePlayerProfile = null;
    private Text playerNameText;

    private void Awake() {
        playerNameText = gameObject.GetComponent<Text>();
    }

    private void Update() {
        if(runtimePlayerProfile.playerData != null) {
            playerNameText.text = runtimePlayerProfile.playerData.playerName;
        }
        else {
            playerNameText.text = "Who are you?";
        }
    }
}
