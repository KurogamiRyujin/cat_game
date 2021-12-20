using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Space gameobject. Spelling is like that because Unity already has Space class.
public class Spayce : MonoBehaviour
{
    [SerializeField] private SpaceStatsSO spaceStats;

    private void Awake() {
        gameObject.transform.position = new Vector3(gameObject.transform.position.x, spaceStats.altitude, gameObject.transform.position.z);
    }
}
