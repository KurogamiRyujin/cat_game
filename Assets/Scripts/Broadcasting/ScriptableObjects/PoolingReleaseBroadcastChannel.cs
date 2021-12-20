using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Pooling Release Broadcast Channel", menuName = "Broadcasting/PoolingRelease Channel")]
public class PoolingReleaseBroadcastChannel : BroadcastChannelSO<IPoolable>
{
    public void ReleaseEventRaised(IPoolable poolable) {
        if(onEventRaised != null) {
            onEventRaised(poolable);
        }
    }
}
