using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Object pool.
//Possessed by behaviours that manage spawning.
[CreateAssetMenu(fileName = "New Pool", menuName = "Pooling/Pool")]
public class PoolSO : ScriptableObject
{
    [SerializeField] private List<IPoolable> standbyObjects = new List<IPoolable>();
    [SerializeField] private List<IPoolable> activeObjects = new List<IPoolable>();

    public bool CanRequest() {
        return standbyObjects.Count > 0;
    }

    public IPoolable Request() {
        IPoolable poolable = null;

        if(CanRequest()) {
            poolable = standbyObjects[0];
            standbyObjects.Remove(poolable);
            activeObjects.Add(poolable);
        }

        return poolable;
    }

    public void Release(IPoolable poolable) {
        if(activeObjects.Contains(poolable)) {
            activeObjects.Remove(poolable);
        }
        if(!standbyObjects.Contains(poolable)) {
            standbyObjects.Add(poolable);
        }
    }

    //Since this pool is made flexible, behaviours can add more objects if a newly spawned object was made.
    public void AddPool(IPoolable poolable) {
        poolable.OnRelease += Release;
        poolable.OnDestroyEvent += Remove;
        if(poolable.GetGameObject().activeInHierarchy) {
            activeObjects.Add(poolable);
        }
        else {
            standbyObjects.Add(poolable);
        }
    }

    //Remove poolable from Pool
    public void Remove(IPoolable poolable) {
        if(activeObjects.Contains(poolable)) {
            activeObjects.Remove(poolable);
        }
        if(standbyObjects.Contains(poolable)) {
            standbyObjects.Remove(poolable);
        }
        poolable.OnDestroyEvent -= Remove;
        poolable.OnRelease -= Release;
    }
}
