using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Channel for Scene Loading specific events.
[CreateAssetMenu(fileName = "New SceneLoading Channel", menuName = "Broadcasting/SceneLoading Channel")]
public class SceneLoadingBroadcasting : BroadcastChannelSO<List<SceneSO>>
{
    public void SceneLoadingEventRaised(List<SceneSO> scenes) {
        if(onEventRaised != null) {
            onEventRaised(scenes);
        }
    }
}
