using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

//Broadcast channel for spawn requests of ISpawnables.
[CreateAssetMenu(fileName = "Ner Spawn Request Channel", menuName = "Broadcasting/Spawn Request Channel")]
public class SpawnRequestBroadcasting : ScriptableObject//BroadcastChannelSO<ISpawnable>
{
    public delegate GameObject OnEventRaised(ISpawnable spawnable);
    public OnEventRaised onEventRaised;

    //For requests of a single GameObject.
    //Will give the last delegate to be invoked.
    public GameObject SpawnRequestEventRaised(ISpawnable spawnable) {
        if(onEventRaised == null) {
            return null;
        }
        return onEventRaised(spawnable);
    }

    //For requests of multiple GameObjects
    public IEnumerable<GameObject> ManySpawnsRequestEventRaise(ISpawnable spawnable) {
        if(onEventRaised == null) {
            return null;
        }
        return onEventRaised.GetInvocationList().Select(x => (GameObject)x.DynamicInvoke(spawnable));
    }
}
