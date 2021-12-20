using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PushRequestChannel", menuName = "Broadcasting/PushRequest Channel")]
public class PushRequestChannel : BroadcastChannelSO<PushSO.PushType>
{
    public void PushEventRequestEventRaised(PushSO.PushType pushType) {
        if(onEventRaised != null) {
            onEventRaised(pushType);
        }
    }
}
