using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement;

[CreateAssetMenu(fileName = "Particle Reference", menuName = "Particles/Reference")]
public class ParticleReferenceSO : ScriptableObject
{
    public AssetReference particleReference;
}
