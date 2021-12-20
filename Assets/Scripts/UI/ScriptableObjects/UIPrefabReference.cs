using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

//Scriptable Object holding a reference to a prefab asset.
[CreateAssetMenu(fileName = "New UI Prefab Reference", menuName = "UI/UI Prefab Reference")]
public class UIPrefabReference : ScriptableObject
{
    public AssetReference uiPrefabReference;
}
