using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Void Channel", menuName = "Broadcasting/Void Channel")]
public class VoidBroadcastChannelSO : ScriptableObject
{
    public delegate void OnEventRaised();
    public OnEventRaised onEventRaised;
}
