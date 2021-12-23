using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Makes the object look at an IStalkable.
/// </summary>
public class LookAtTarget : MonoBehaviour
{
    [Header("Target")]
    public GameObject stalkableObject;

    private IStalkable stalkable;

    private void Start() {
        SetStalkable();
    }

    private void Update() {
        LookAtStalkable();
    }

    private void LookAtStalkable() {
        if(stalkableObject != null && stalkable != null) {
            gameObject.transform.LookAt(stalkable.Stalk());
        }
    }

    private void SetStalkable() {
        stalkable = stalkableObject.GetComponent<IStalkable>();
    }
}
