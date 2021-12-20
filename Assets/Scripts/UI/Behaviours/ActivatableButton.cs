using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class ActivatableButton : MonoBehaviour
{
    protected Button button;
    protected List<Transform> childElements;

    protected virtual void Awake() {
        button = gameObject.GetComponent<Button>();
        childElements = new List<Transform>(GetComponentsInChildren<Transform>());
        childElements.Remove(transform);
    }
    
    protected virtual void Activate() {
        button.enabled = true;
        foreach(Transform obj in childElements) {
            obj.gameObject.SetActive(true);
        }
    }

    protected virtual void Deactivate() {
        button.enabled = false;
        foreach(Transform obj in childElements) {
            obj.gameObject.SetActive(false);
        }
    }
}
