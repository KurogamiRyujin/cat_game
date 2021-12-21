using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Base class for UIEntrance behaviours.
/// </summary>
public abstract class UIEntrance : MonoBehaviour
{
    [SerializeField] protected float speed;

    protected virtual void OnEnable() {
        Enter();
    }
    
    protected abstract void Enter();
}
