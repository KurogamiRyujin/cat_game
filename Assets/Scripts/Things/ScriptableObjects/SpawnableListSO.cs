using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Spawnable List", menuName = "Spawning/Spawn List")]
public class SpawnableListSO : ScriptableObject
{
    [Header("Spawnables")]
    public List<SpawnReferenceSO> spawnables = default;
}
