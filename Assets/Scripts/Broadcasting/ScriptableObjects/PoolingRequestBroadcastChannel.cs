using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Pooling Request Channel", menuName = "Broadcasting/PoolingRequest Channel")]
public class PoolingRequestBroadcastChannel : ScriptableObject
{
    public delegate GameObject OnEventRaised(IPoolable poolable);
    public OnEventRaised onEventRaised;

    public GameObject SpawnRequestEventRaised(IPoolable poolable) {
        if(onEventRaised == null) {
            return null;
        }
        return onEventRaised(poolable);
    }
}
