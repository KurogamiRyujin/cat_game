using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

/// <summary>
/// SFX reference to an audio clip.
/// </summary>
[CreateAssetMenu(fileName = "SFX", menuName = "Sounds/SFX/SFX Reference")]
public class SfxSO : ScriptableObject
{
    public enum SFXType {
        PUSH,
        NORMAL_PUSH_HIT,
        HARD_PUSH_HIT,
        BURN
    }
    public AudioClip audioClip;
    public SFXType sfxType;
}
