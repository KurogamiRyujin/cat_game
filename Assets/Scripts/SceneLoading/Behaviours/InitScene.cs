using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//Behaviour to initialize the game.
//Additively loads a scene on top of the SceneLoader scene.
//Destroys this object afterwards.
public class InitScene : MonoBehaviour
{
    [Header("Scenes to initialize")]
    [SerializeField] private List<SceneSO> scenes;

    [Header("Channel to broadcast to")]
    [SerializeField] private SceneLoadingBroadcasting sceneLoadingBroadcasting;

    private void Start() {
        if(sceneLoadingBroadcasting != null && scenes != null) {
            sceneLoadingBroadcasting.SceneLoadingEventRaised(scenes);
        }
        Destroy(this.gameObject);
    }
}
