using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartButton : ActivatableButton
{
    [SerializeField] private PlayerProfileSO runtimePlayerProfile;

    private void Update() {
        if(runtimePlayerProfile.playerData != null && runtimePlayerProfile.playerData.playerName != "") {
            Activate();
        }
        else {
            Deactivate();
        }
    }
}
