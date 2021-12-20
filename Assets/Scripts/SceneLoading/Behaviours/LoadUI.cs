using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Calls the scene loader behaviour to request the UI
public class LoadUI : MonoBehaviour
{
    [SerializeField] private SceneLoadBehaviour sceneLoadBehaviour;
    
    void Start()
    {
        sceneLoadBehaviour.RequestScenes();
    }
}
