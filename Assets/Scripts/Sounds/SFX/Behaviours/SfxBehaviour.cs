using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Listens to events and triggers the SFX audio source to play.
/// </summary>
public class SfxBehaviour : MonoBehaviour
{
    [Header("Sfx Handler")]
    [SerializeField] private SfxHandler sfxHandler = null;
    [Header("Audio Source Component")]
    [SerializeField] private AudioSource sfxSource = null;

    [Header("Channels listening to")]
    [SerializeField] private SfxRequestBroadcasting sfxRequestBroadcasting = null;

    private void OnEnable() {
        //Register events
        sfxRequestBroadcasting.onEventRaised += OnSoundTriggerEvent;
    }

    private void OnDisable() {
        //Unregister events
        sfxRequestBroadcasting.onEventRaised -= OnSoundTriggerEvent;
    }

    private void OnSoundTriggerEvent() {
        if(!sfxSource.isPlaying) {
            sfxSource.Play();
        }
        else {
            sfxSource.PlayOneShot(sfxSource.clip);
        }
    }

    private void OnSoundTriggerEvent(SfxSO.SFXType sfxType) {
        SfxSO sfx = sfxHandler.RequestSFX(sfxType);
        
        sfxSource.clip = sfx.audioClip;
        OnSoundTriggerEvent();
    }
}
