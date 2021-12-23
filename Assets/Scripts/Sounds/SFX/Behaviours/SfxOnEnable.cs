using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Automatically requests an SFX to play upon this behaviour's OnEnable.
/// </summary>
public class SfxOnEnable : MonoBehaviour
{
    [Header("Sfx to Play on Enable")]
    [SerializeField] private SfxSO.SFXType sfx;

    [Header("Channel Broadcasting to")]
    [SerializeField] private SfxRequestBroadcasting sfxRequestChannel;

    private void OnEnable() {
        if(sfxRequestChannel != null) {
            sfxRequestChannel.SfxRequestEventRaised(sfx);
        }
    }
}
