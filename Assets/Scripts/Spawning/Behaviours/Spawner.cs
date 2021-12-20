using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Spawns an ISpawnable or IPoolable on this spawner's location.
public class Spawner : MonoBehaviour
{
    [Header("Channels listening to")]
    [SerializeField] private SpawnRequestBroadcasting spawnRequestBroadcasting;
    [SerializeField] private PoolingRequestBroadcastChannel poolingRequestBroadcastChannel;

    private void OnEnable() {
        //Register events
        if(spawnRequestBroadcasting != null) {
            spawnRequestBroadcasting.onEventRaised += Spawn;
            poolingRequestBroadcastChannel.onEventRaised += Spawn;
        }
    }

    private void OnDisable() {
        //Unregister events
        if(spawnRequestBroadcasting.onEventRaised != null) {
            spawnRequestBroadcasting.onEventRaised -= Spawn;
            poolingRequestBroadcastChannel.onEventRaised -= Spawn;
        }
    }

    private GameObject Spawn(ISpawnable spawnable) {
        return spawnable.Spawn(this.gameObject.transform.position);
    }

    private GameObject Spawn(IPoolable poolable) {
        poolable.GetGameObject().transform.position = gameObject.transform.position;
        poolable.Activate();
        return poolable.GetGameObject();
    }
}
