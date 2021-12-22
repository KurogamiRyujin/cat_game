using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement;
using UnityEngine.ResourceManagement.AsyncOperations;

/// <summary>
/// Manages particles that are spawned during runtime and/or may be used again and again.
/// </summary>
[CreateAssetMenu(fileName = "SpontaneousParticleManager", menuName = "Particles/Spontaneous Particle Manager")]
public class SpontaneousParticleManagerSO : ScriptableObject
{
    /// <summary>
    /// Dictionary containing loaded particles which can be referenced and instantiated normally by the one requesting.
    /// </summary>
    /// <typeparam name="ParticleReferenceSO">ParticleReference</typeparam>
    /// <typeparam name="GameObject">Prefab</typeparam>
    /// <returns></returns>
    private Dictionary<ParticleReferenceSO, GameObject> particleDictionary = new Dictionary<ParticleReferenceSO, GameObject>();

    public GameObject RequestParticle(ParticleReferenceSO particleReference) {
        if(!particleDictionary.ContainsKey(particleReference)) {
            LoadParticle(particleReference);

            return null;
        }
        else {
            GameObject particle = particleDictionary[particleReference];

            return particle;
        }
    }

    public void LoadParticle(ParticleReferenceSO particleReference) {
        if(!particleDictionary.ContainsKey(particleReference)) {
            AsyncOperationHandle<GameObject> async = Addressables.LoadAssetAsync<GameObject>(particleReference.particleReference);

            async.Completed += (asyncOperationHandle) => {
                if(particleDictionary.ContainsKey(particleReference)) {
                    particleDictionary.Add(particleReference, null);
                }

                particleDictionary[particleReference] = asyncOperationHandle.Result;
            };
        }
    }

    public void UnloadParticle(ParticleReferenceSO particleReference) {
        if(particleDictionary.ContainsKey(particleReference)) {
            Addressables.Release<ParticleReferenceSO>(particleReference);

            particleDictionary[particleReference] = null;
            particleDictionary.Remove(particleReference);
        }
    }
}
