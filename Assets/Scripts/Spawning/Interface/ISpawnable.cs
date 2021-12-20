using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISpawnable {
    GameObject Spawn(Vector3 position);

    SpawnReferenceSO.SpawnType SpawnType();
}
