using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SFX Handler", menuName = "Sounds/SFX/Handler")]
public class SfxHandler : ScriptableObject
{
    [SerializeField] private SfxList allSfxes = default;

    public SfxSO RequestSFX(SfxSO.SFXType sfxType) {
        SfxSO sfx = allSfxes.allSfXes.Find(x => x.sfxType == sfxType);
        
        return sfx;
    }
}
