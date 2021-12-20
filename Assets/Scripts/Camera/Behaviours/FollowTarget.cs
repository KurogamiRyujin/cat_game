using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Follow object behaviour where the gameobject with this component will maintain its distance with its target.
public class FollowTarget : MonoBehaviour
{
    [Header("Target")]
    public GameObject stalkableObject;
    private IStalkable stalkable;

    //Distance between this game object and the target at the start.
    private Vector3 initialDistance;

    private void Start() {
        SetStalkable();

        if(stalkable != null) {
            initialDistance = gameObject.transform.position - stalkable.Stalk();
        }
    }

    private void Update() {
        MaintainDistance();
    }

    //Adjust this gameObject's position so it keeps its initial distance with the target.
    private void MaintainDistance() {
        if(stalkable == null) {
            SetStalkable();
        }

        if(stalkableObject != null && stalkable != null) {
            gameObject.transform.position = initialDistance + stalkable.Stalk();
        }
    }

    private void SetStalkable() {
        stalkable = stalkableObject.GetComponent<IStalkable>();
    }
}
