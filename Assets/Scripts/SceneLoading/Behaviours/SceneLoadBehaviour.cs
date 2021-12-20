using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

public class SceneLoadBehaviour : MonoBehaviour
{
    [Header("Scenes to load")]
    [SerializeField] private List<SceneSO> scenes;
    [SerializeField] private LoadSceneMode loadSceneMode;
    [Header("Scene Loader")]
    [SerializeField] private SceneLoaderSO sceneLoader;
    [Header("On Scene Loaded Events")]
    public UnityEvent OnSceneLoadingFinished;
    [Header("On Scene Unloaded Events")]
    public UnityEvent OnSceneUnloadingFinished;

    private void OnEnable() {
        //Register events
        sceneLoader.OnScenesLoaded += OnSceneLoaded;
        sceneLoader.OnScenesUnloaded += OnSceneUnloaded;
    }

    private void OnDisable() {
        //Unregister events
        sceneLoader.OnScenesLoaded -= OnSceneLoaded;
        sceneLoader.OnScenesUnloaded -= OnSceneUnloaded;
    }

    /// <summary>
    /// RequestUnloadScences() then RequestScenes()
    /// </summary>
    public void SwitchScenes() {
        RequestUnloadScenes();
        RequestScenes();
    }

    public void RequestScenes() {
        if(loadSceneMode == LoadSceneMode.Single) {
            StartCoroutine(sceneLoader.LoadSceneCoroutine(scenes[0]));
        }
        else {
            StartCoroutine(sceneLoader.LoadScenesCoroutine(scenes));
        }
    }

    public void RequestUnloadScenes() {
        if(loadSceneMode == LoadSceneMode.Additive) {
            StartCoroutine(sceneLoader.UnloadScenesCoroutine());
        }
    }

    public void RequestUnloadScenes(List<SceneInstance> selectedScenes) {
        StartCoroutine(sceneLoader.UnloadScenesCoroutine(selectedScenes));
    }

    public void RequestUnloadScenes(List<SceneSO> selectedScenes) {
        StartCoroutine(sceneLoader.UnloadScenesCoroutine(selectedScenes));
    }

    private void OnSceneLoaded() {
        OnSceneLoadingFinished.Invoke();
    }

    private void OnSceneUnloaded() {
        OnSceneUnloadingFinished.Invoke();
    }
}
