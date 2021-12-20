using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

//Reference to a spawnable Prefab.
[CreateAssetMenu(fileName = "New Spawnable Reference", menuName = "Spawning/Spawn Reference")]
public class SpawnReferenceSO : ScriptableObject
{
    public enum SpawnType {
        CAT,
        TEST,
        OTHER
    }
    //NOTE: Changed it to normal instantiate instead of Asyncrhronous Instantiate from Addressables.
    //Need to study how to implement it better.
    // public AssetReference prefabReference;
    public GameObject prefab;
    //Instance pool
    public PoolSO pool;

    //Prefab's spawn type
    private SpawnType spawnType;

    public SpawnType GetSpawnType() {
        //Sett the Spawn type of the prefab
        spawnType = (prefab.GetComponent<ISpawnable>() != null) ? prefab.GetComponent<ISpawnable>().SpawnType() : SpawnType.OTHER;
        return spawnType;
    }

    // public GameObject Spawn(Vector3 position) {
    //     AsyncOperationHandle<GameObject> obj = prefabReference.InstantiateAsync(position, Quaternion.identity);
    //     GameObject result = null;

    //     if(obj.Status == AsyncOperationStatus.Succeeded) {
    //         result = obj.Result;
    //     }

    //     return result;
    //     // prefabReference.InstantiateAsync(position, Quaternion.identity);
    // }

    // public GameObject Spawn(Vector3 position) {
    //     return Instantiate<GameObject>(prefab, position, Quaternion.identity);
    // }
}
