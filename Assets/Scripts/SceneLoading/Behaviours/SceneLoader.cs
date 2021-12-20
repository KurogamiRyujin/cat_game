using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

//Scene loading behaviour implementation.
public class SceneLoader : MonoBehaviour
{
    //
    [Header("Channels Listening To")]
    [SerializeField] private SceneLoadingBroadcasting sceneLoadingBroadcasting;
    [SerializeField] private VoidBroadcastChannelSO voidBroadcastChannel;

    [Header("Other Events to Raise on Loading New Scene")]
    public UnityEvent Response;

    //Reference to currently loaded scenes that is not the SceneLoader scene.
    private List<SceneInstance> loadedScenes;
    //Checker if the SceneLoader is loading scenes;
    private bool nowLoading;

    private void Awake() {
        loadedScenes = new List<SceneInstance>();
        nowLoading = false;
    }

    private void OnEnable() {
        //Register to Events
        if(sceneLoadingBroadcasting != null) {
            sceneLoadingBroadcasting.onEventRaised += LoadScenes;
        }
        if(voidBroadcastChannel != null) {
            voidBroadcastChannel.onEventRaised += OnSceneLoadRequest;
        }
    }

    private void OnDisable() {
        //Unregister to Events
        if(sceneLoadingBroadcasting != null) {
            if(sceneLoadingBroadcasting.onEventRaised != null) {
                sceneLoadingBroadcasting.onEventRaised -= LoadScenes;
            }
        }
        if(voidBroadcastChannel != null) {
            if(voidBroadcastChannel.onEventRaised != null) {
                voidBroadcastChannel.onEventRaised -= OnSceneLoadRequest;
            }
        }
    }

    //Scene Loading Method.
    //Loads scenes additively on top of the SceneLoader scene.
    private void LoadScenes(List<SceneSO> scenes) {
        if(nowLoading) {
            return;
        }
        nowLoading = true;

        UnloadScenes(loadedScenes);
        // for(int i = 0; i < scenes.Count; i++) {
        //     scenes[i].sceneReference.LoadSceneAsync(LoadSceneMode.Additive, true);
        //     loadedScenes.Add(scenes[i]);
        // }
        StartCoroutine(LoadScenesCoroutine(scenes));
    }

    private IEnumerator LoadScenesCoroutine(List<SceneSO> scenes) {
        //Store Async Operation results as a list of SceneInstances
        List<SceneInstance> results = new List<SceneInstance>();

        //Start Loading each of the SceneSOs
        for(int i = 0; i < scenes.Count; i++) {
            //Load the scenes
            AsyncOperationHandle<SceneInstance> async = scenes[i].sceneReference.LoadSceneAsync(LoadSceneMode.Additive, true);
            yield return async;

            //When async loading is done...
            if(async.IsDone) {
                // //Set the active scene to the first scene loaded
                // //This is to ensure all newly instantiated objects are spawned in the active scene
                // //For this game, there is only one scene at a time that instantiates objects, so this is fine
                // if(i == 0) {
                //     SceneManager.SetActiveScene(async.Result.Scene);
                // }
                
                //then add SceneInstance to results and loadedScenes.
                results.Add(async.Result);
                loadedScenes.Add(async.Result);
            }
        }

        nowLoading = false;
    }
    
    //Unload Scene Method.
    //Unloads loadedScenes starting from the last loadedScene.
    private void UnloadScenes(List<SceneInstance> scenes) {
        // for(int i = scenes.Count-1; i >= 0; i--) {
        //     scenes[i].sceneReference.UnLoadScene();

        //     if(loadedScenes.Contains(scenes[i])) {
        //         loadedScenes.Remove(scenes[i]);
        //     }
        // }
        StartCoroutine(UnloadScenesCoroutine(scenes));
    }

    private IEnumerator UnloadScenesCoroutine(List<SceneInstance> scenes) {
        for(int i = scenes.Count-1; i >= 0; i--) {
            // AsyncOperationHandle<SceneInstance> async = scenes[i].sceneReference.UnLoadScene();
            
            //Unload loaded scenes
            AsyncOperation async = SceneManager.UnloadSceneAsync(scenes[i].Scene);
            yield return async;

            //When unloading is done, remove that scene from loadedScenes
            if(async.isDone) {
                if(loadedScenes.Contains(scenes[i])) {
                    loadedScenes.Remove(scenes[i]);
                }
            }
        }
    }

    //Other Responses to invoke upon event happening.
    //Subscribed to Void Broadcast Channel.
    private void OnSceneLoadRequest() {
        Response.Invoke();
    }
}
