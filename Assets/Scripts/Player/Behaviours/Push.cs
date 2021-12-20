using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Behaviour for the Push mechanic.
//Needs a trigger collider for this to work.
public class Push : MonoBehaviour
{
    [Header("Push Stats Reference")]
    [SerializeField] private PushSO pushStatsReference;
    [Header("Channel listening to")]
    [SerializeField] private PushRequestChannel pushRequestChannel;

    [Header("Channels broadcasting to")]
    [SerializeField] private SfxRequestBroadcasting doPushSFXChannel;
    [SerializeField] private SfxRequestBroadcasting pushHitSFXChannel;
    private List<IPushable> pushables;

    private void Awake() {
        pushables = new List<IPushable>();
    }

    private void OnEnable() {
        //Register events
        pushRequestChannel.onEventRaised += DoPush;
    }

    private void OnDisable() {
        //Unregister events
        pushRequestChannel.onEventRaised -= DoPush;
    }

    //Get a reference of pushable when it enters the push zone.
    private void OnTriggerEnter(Collider other) {
        IPushable pushable = other.GetComponent<IPushable>();

        if(pushable != null && !pushables.Contains(pushable) && other.gameObject != gameObject.transform.parent.gameObject) {
            pushables.Add(pushable);
        }
    }

    //Remove pushable if it is no longer inside push zone.
    private void OnTriggerExit(Collider other) {
        IPushable pushable = other.GetComponent<IPushable>();

        if(pushable != null && pushables.Contains(pushable)) {
            pushables.Remove(pushable);
        }
    }

    //Push all pushables inside the push zone and remove them from pushables.
    private void DoPush(PushSO.PushType pushType) {
        //Check if this Pusher pushed something;
        bool didPush = false;

        if(doPushSFXChannel.onEventRaised != null) {
            doPushSFXChannel.SfxRequestEventRaised(SfxSO.SFXType.PUSH);
        }

        for(int i = pushables.Count-1; i >= 0; i--) {
            pushables[i].Push(pushStatsReference.PushStrength(pushType), gameObject.transform.forward);
            pushables.Remove(pushables[i]);
            didPush = true;
        }

        if(didPush) {
            switch(pushType) {
                case PushSO.PushType.NORMAL:
                pushHitSFXChannel.SfxRequestEventRaised(SfxSO.SFXType.NORMAL_PUSH_HIT);
                break;
                case PushSO.PushType.HARD:
                pushHitSFXChannel.SfxRequestEventRaised(SfxSO.SFXType.HARD_PUSH_HIT);
                break;
            }
        }
    }
}
