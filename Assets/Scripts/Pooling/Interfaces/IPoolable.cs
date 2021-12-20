using System;
using UnityEngine;

//Poolable interface
public interface IPoolable
{
    void Activate();
    void Release();
    void OnActivate();
    GameObject GetGameObject();
    event Action<IPoolable> OnRelease;
    event Action<IPoolable> OnDestroyEvent;
}
