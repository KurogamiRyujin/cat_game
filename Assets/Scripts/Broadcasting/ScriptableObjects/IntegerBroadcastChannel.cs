using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntegerBroadcastChannel : BroadcastChannelSO<int>
{
    public void IntegerEventRaised(int num) {
        if(onEventRaised != null) {
            onEventRaised(num);
        }
    }
}
