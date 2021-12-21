using System;
using UnityEngine;

/// <summary>
/// Poolable interface
/// </summary>
public interface IPoolable
{
    /// <summary>
    /// Things to do to activate this IPoolable.
    /// </summary>
    void Activate();

    /// <summary>
    /// Things to do when this IPoolable is released.
    /// </summary>
    void Release();
    GameObject GetGameObject();

    /// <summary>
    /// Events for external classes to subscribe to when this IPoolable is released.
    /// </summary>
    event Action<IPoolable> OnRelease;
    /// <summary>
    /// Events for external classes to subscribe to when this IPoolable is destroyed.
    /// </summary>
    event Action<IPoolable> OnDestroyEvent;
}
