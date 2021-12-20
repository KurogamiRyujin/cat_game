using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

//Scriptable Object Holding the reference to a Scene asset.
//Used to reference the Scene to access some of a Scene's functionalities.
[CreateAssetMenu(fileName = "Scene", menuName = "SceneManagement/Scene")]
public class SceneSO : ScriptableObject
{
    public AssetReference sceneReference;
}
