using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Push Stats", menuName = "Pushing/Push Stats")]
public class PushSO : ScriptableObject
{
    public enum PushType {
        NORMAL,
        HARD
    }

    [Header("Push Stats")]
    public float normalPushStrength = 500f;
    public float hardPushStrength = 2500f;

    [Header("Push Particles")]
    public ParticleReferenceSO normalPushParticle;
    public ParticleReferenceSO hardPushParticle;

    [Header("Push Sfx")]
    public SfxSO.SFXType sfxType;

    /// <summary>
    /// Returns the push strength based on the type of push.
    /// </summary>
    /// <param name="pushType">Push Type</param>
    /// <returns>Push Strength</returns>
    public float PushStrength(PushType pushType) {
        float strength = 0f;
        switch(pushType) {
            case PushType.NORMAL:
            strength = normalPushStrength;
            break;
            case PushType.HARD:
            strength = hardPushStrength;
            break;
        }

        return strength;
    }
}
