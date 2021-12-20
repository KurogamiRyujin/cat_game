using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseDestructionBehaviour : MonoBehaviour
{
    protected virtual void OnTriggerEnter(Collider other) {
        IPoolable poolable = other.GetComponent<IPoolable>();

        if(poolable != null) {
            poolable.Release();
        }
    }
}
