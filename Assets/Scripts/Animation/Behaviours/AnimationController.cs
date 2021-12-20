using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AnimationController : MonoBehaviour
{
    [Header("Animator")]
    [SerializeField] private Animator animator;

    public void OnStateUpdate(AnimationEventParameter param) {
        animator.SetBool(param.animationEventName, param.boolValue);
    }

    public void OnStateTrigger(AnimationEventParameter param) {
        animator.ResetTrigger(param.animationEventName);
        animator.SetTrigger(param.animationEventName);
    }
}
