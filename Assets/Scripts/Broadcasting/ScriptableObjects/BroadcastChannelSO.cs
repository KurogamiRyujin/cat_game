using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Broadcasting Channel for Game Events with type T parameters.
//Not instantiated, but is derived by other new channels.
public class BroadcastChannelSO<T> : ScriptableObject
{
    //Game Event with T type parameter being sent.
    public delegate void OnEventRaised(T param);
    public OnEventRaised onEventRaised;
}
