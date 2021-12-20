using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SfxRequest Channel", menuName = "Broadcasting/SfxRequest Channel")]
public class SfxRequestBroadcasting : BroadcastChannelSO<SfxSO.SFXType>
{
    public void SfxRequestEventRaised(SfxSO.SFXType sfxType) {
        if(onEventRaised != null) {
            onEventRaised(sfxType);
        }
    }
}
