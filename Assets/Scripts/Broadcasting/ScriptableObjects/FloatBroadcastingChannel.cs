using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Float Broadcasting Channel", menuName = "Broadcasting/Float Broadcasting Channel")]
public class FloatBroadcastingChannel : BroadcastChannelSO<float>
{
    public void FloatEventRaised(float num) {
        if(onEventRaised != null) {
            onEventRaised(num);
        }
    }
}
