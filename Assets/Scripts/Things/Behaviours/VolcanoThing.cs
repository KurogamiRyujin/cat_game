using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolcanoThing : Thing, ISpawnable, IPoolable, IPushable, IWeight, IBurnable, IStatusable
{
    public event Action<IPoolable> OnRelease;
    public event Action<IPoolable> OnDestroyEvent;

    public void Activate()
    {
        throw new NotImplementedException();
    }

    public void ApplyStatus(StatusEffect statusEffect)
    {
        throw new NotImplementedException();
    }

    public void Burn()
    {
        throw new NotImplementedException();
    }

    public GameObject GetGameObject()
    {
        throw new NotImplementedException();
    }

    public float GetWeight()
    {
        throw new NotImplementedException();
    }

    public void HoldStill()
    {
        throw new NotImplementedException();
    }

    public void Push(float strength, Vector3 direction)
    {
        throw new NotImplementedException();
    }

    public void Release()
    {
        throw new NotImplementedException();
    }

    public GameObject Spawn(Vector3 position)
    {
        throw new System.NotImplementedException();
    }

    public SpawnReferenceSO.SpawnType SpawnType()
    {
        throw new System.NotImplementedException();
    }
}
