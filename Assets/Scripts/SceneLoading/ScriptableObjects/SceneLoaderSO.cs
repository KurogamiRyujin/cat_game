using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "SceneLoader", menuName = "SceneManagement/Scene Loader")]
public class SceneLoaderSO : ScriptableObject
{
    //Reference to currently loaded scenes
    private List<SceneInstance> loadedScenes = new List<SceneInstance>();
    //Checker if the SceneLoader is loading scenes
    private bool nowLoading = false;
    //Checker if the SceneLoader is unloading scenes
    private bool nowUnloading = false;
    public Action OnScenesLoaded;
    public Action OnScenesUnloaded;

    public IEnumerator LoadScenesCoroutine(List<SceneSO> scenes) {

        //Start Loading each of the SceneSOs
        for(int i = 0; i < scenes.Count; i++) {
            nowLoading = true;
            //Load the scenes
            AsyncOperationHandle<SceneInstance> async = scenes[i].sceneReference.LoadSceneAsync(LoadSceneMode.Additive, true);
            yield return async;

            //When async loading is done...
            if(async.IsDone) {
                
                //then add SceneInstance to loadedScenes.
                loadedScenes.Add(async.Result);

                if(i == scenes.Count-1) {
                    nowLoading = false;
                    OnScenesLoaded();
                }
            }
        }
    }

    public IEnumerator LoadSceneCoroutine(SceneSO scene) {
        nowLoading = true;
        AsyncOperationHandle<SceneInstance> async = scene.sceneReference.LoadSceneAsync();
        yield return async;

        if(async.IsDone) {
            loadedScenes.Add(async.Result);

            nowLoading = false;
            OnScenesLoaded();
        }
    }

    //Unload all loaded Scenes
    public IEnumerator UnloadScenesCoroutine() {
        for(int i = loadedScenes.Count-1; i >= 0; i--) {
            nowUnloading = true;
            //Unload loaded scenes
            AsyncOperation async = SceneManager.UnloadSceneAsync(loadedScenes[i].Scene);
            yield return async;

            //When unloading is done, remove that scene from loadedScenes
            if(async.isDone) {
                loadedScenes.Remove(loadedScenes[i]);

                if(i == 0) {
                    nowUnloading = false;
                    OnScenesUnloaded();
                }
            }
        }
    }

    //Unload selected scenes
    public IEnumerator UnloadScenesCoroutine(List<SceneInstance> scenes) {
        for(int i = scenes.Count-1; i >= 0; i--) {
            nowUnloading = true;
            //Unload loaded scenes
            AsyncOperation async = SceneManager.UnloadSceneAsync(scenes[i].Scene);
            yield return async;

            //When unloading is done, remove that scene from loadedScenes
            if(async.isDone) {
                if(loadedScenes.Contains(scenes[i])) {
                    loadedScenes.Remove(scenes[i]);
                }

                if(i == 0) {
                    nowUnloading = false;
                    OnScenesUnloaded();
                }
            }
        }
    }

    public IEnumerator UnloadScenesCoroutine(List<SceneSO> scenes) {
        for(int i = scenes.Count-1; i >= 0; i--) {
            nowUnloading = true;
            //Unload loaded scenes
            AsyncOperationHandle<SceneInstance> async = scenes[i].sceneReference.UnLoadScene();
            yield return async;

            //When unloading is done, remove that scene from loadedScenes
            if(async.IsDone) {
                if(loadedScenes.Contains(async.Result)) {
                    loadedScenes.Remove(async.Result);
                }

                if(i == 0) {
                    nowUnloading = false;
                    OnScenesUnloaded();
                }
            }
        }
    }

    public bool IsLoading() {
        return nowLoading;
    }

    public bool IsUnloading() {
        return nowUnloading;
    }
}
