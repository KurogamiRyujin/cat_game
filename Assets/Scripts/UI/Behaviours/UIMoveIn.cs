using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Behaviour tha moves towards where it is set coming from the starting position.
/// </summary>
public class UIMoveIn : UIEntrance
{
    [Header("Starting Rect Transform Position")]
    [SerializeField] private Vector2 startingPosition;
    //How much it shoots past the target position before moving to it.
    [SerializeField] private Vector2 offset;

    private Vector2 targetPosition;
    private bool shouldRun;
    private bool shouldReturn;
    private RectTransform rectTrasform;

    private void Awake() {
        shouldRun = false;
        shouldReturn = false;
        rectTrasform = GetComponent<RectTransform>();
    }

    protected override void Enter()
    {
        targetPosition = rectTrasform.anchoredPosition;
        rectTrasform.anchoredPosition = startingPosition;
        shouldRun = true;
    }

    private void Update() {
        if(shouldRun) {
            FlyIn();
        }
        else if(shouldReturn) {
            Settle();
        }
    }

    private void FlyIn() {
        rectTrasform.anchoredPosition = Vector2.Lerp(rectTrasform.anchoredPosition, targetPosition + offset, speed * Time.deltaTime);
        FinishCheck();
    }

    private void Settle() {
        rectTrasform.anchoredPosition = Vector2.Lerp(rectTrasform.anchoredPosition, targetPosition, speed * Time.deltaTime);
    }

    private void FinishCheck() {
        if(Vector2.Distance(rectTrasform.anchoredPosition, targetPosition + offset) <= Mathf.Epsilon) {
            shouldRun = false;
            shouldReturn = true;
        }
    }

    private void SettleCheck() {
        if(Vector2.Distance(rectTrasform.anchoredPosition, targetPosition) <= Mathf.Epsilon) {
            shouldReturn = false;
        }
    }
}
