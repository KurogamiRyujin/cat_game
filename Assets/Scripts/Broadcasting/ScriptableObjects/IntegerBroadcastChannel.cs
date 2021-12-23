using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Integer Channel", menuName = "Broadcasting/Integer Channel")]
public class IntegerBroadcastChannel : BroadcastChannelSO<int>
{
    public void IntegerEventRaised(int num) {
        if(onEventRaised != null) {
            onEventRaised(num);
        }
    }
}
