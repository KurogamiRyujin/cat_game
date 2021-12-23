using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Follow object behaviour where the gameobject with this component will maintain its distance with its target.
public class FollowTarget : MonoBehaviour
{
    [Header("Target")]
    public GameObject stalkableObject;

    [Header("Channels listening to")]
    [SerializeField] private VoidBroadcastChannelSO gameOverChannel;

    private IStalkable stalkable;

    //Distance between this game object and the target at the start.
    private Vector3 initialDistance;
    private bool shouldFollow;

    private void Start() {
        shouldFollow = true;
        SetStalkable();

        if(stalkable != null) {
            initialDistance = gameObject.transform.position - stalkable.Stalk();
        }
    }

    private void OnEnable() {
        //Register events
        gameOverChannel.onEventRaised += OnGameOver;
    }

    private void OnDisable() {
        //Unregister events
        gameOverChannel.onEventRaised -= OnGameOver;
    }

    private void Update() {
        if(shouldFollow) {
            MaintainDistance();
        }
    }

    private void OnGameOver() {
        shouldFollow = false;
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
