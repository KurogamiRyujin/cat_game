using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Button to fire a load scene request.
public class LoadSceneButton : MonoBehaviour
{
    //What scenes to load
    [Header("Scenes to Load")]
    [SerializeField] private List<SceneSO> scenes;

    [Header("Channel to Broadcast to")]
    [SerializeField] private SceneLoadingBroadcasting sceneLoadingBroadcasting;

    public void OnButtonPress() {
        if(sceneLoadingBroadcasting != null) {
            sceneLoadingBroadcasting.SceneLoadingEventRaised(scenes);
        }
    }
}
