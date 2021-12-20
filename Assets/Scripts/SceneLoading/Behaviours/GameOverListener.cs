using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Listens for any Game Over event.
//If it hears one, GG.
public class GameOverListener : MonoBehaviour
{
    [Header("Scenes to Load")]
    [SerializeField] private List<SceneSO> scenes = new List<SceneSO>();

    [Header("Channels Broadcasting to")]
    [SerializeField] private SceneLoadingBroadcasting sceneLoadingBroadcasting;
    [Header("Scene Load Behaviour")]
    [SerializeField] private SceneLoadBehaviour sceneLoadBehaviour;

    [Header("Channels listening to")]
    [SerializeField] private VoidBroadcastChannelSO gameOverChannel;

    private void OnEnable() {
        //Register events
        gameOverChannel.onEventRaised += OnGameOver;
    }

    private void OnDisable() {
        //Unregister events
        gameOverChannel.onEventRaised -= OnGameOver;
    }

    private void OnGameOver() {
        sceneLoadBehaviour.SwitchScenes();
        // if(sceneLoadingBroadcasting != null) {
        //     sceneLoadingBroadcasting.SceneLoadingEventRaised(scenes);
        // }
    }
}
