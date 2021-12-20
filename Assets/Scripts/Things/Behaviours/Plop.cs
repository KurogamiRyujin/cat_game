using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plop : MonoBehaviour
{
    [SerializeField] private float bloatAmount = 0.5f;
    [SerializeField] private float bloatSpeed = 30f;
    [SerializeField] private float bloatTime = 0.5f;
    private bool hasPlopped;
    private Vector3 targetBloat;
    private Earth earth = null;
    private Thing thing = null;
    
    private void Awake() {
        hasPlopped = false;

        Vector3 bloatModifier = new Vector3(bloatAmount, -bloatAmount, bloatAmount);
        targetBloat = transform.localScale + bloatModifier;
    }

    private void OnCollisionEnter(Collision collision) {
        if(!hasPlopped && (earth == null && thing == null)) {
            Ray ray = new Ray(gameObject.transform.position, Vector3.down);
        
            if(Physics.Raycast(ray, out RaycastHit hit, 1f, ~(1<<3))) {
                earth = hit.collider.gameObject.GetComponent<Earth>();
                thing = hit.collider.gameObject.GetComponent<Thing>();

                if(earth != null || thing != null) {
                    hasPlopped = true;
                    StartCoroutine(PlopCoroutine());
                }
            }
        }
    }

    private void OnCollisionStay(Collision collision) {
        Earth possiblyEarth = collision.gameObject.GetComponent<Earth>();
        Thing possiblyThing = collision.gameObject.GetComponent<Thing>();

        earth = (possiblyEarth != null) ? possiblyEarth : null;
        thing = (possiblyThing != null) ? possiblyThing : null;
    }

    private void OnCollisionExit(Collision collision) {
        Ray ray = new Ray(gameObject.transform.position, Vector3.down);

        if(!Physics.Raycast(ray, out RaycastHit hit, 1f, 1<<3)) {
            earth = null;
            thing = null;
        }
    }

    private IEnumerator PlopCoroutine() {
        float timer = 0f;
        while(timer < bloatTime * 0.3) {
            timer += Time.deltaTime;
            DoPlop();
            yield return null;
        }
        while(timer < bloatTime) {
            timer += Time.deltaTime;
            UnPlop();
            yield return null;
        }
        hasPlopped = false;
    }

    private void DoPlop() {
        transform.localScale = Vector3.Lerp(transform.localScale, targetBloat, bloatSpeed * Time.deltaTime);
        
    }

    private void UnPlop() {
        transform.localScale = Vector3.Lerp(transform.localScale, Vector3.one, bloatSpeed * Time.deltaTime);
    }

    /// <summary>
    /// Checks if the scale is approximately the target scale.
    /// </summary>
    /// <param name="current">Current Scale</param>
    /// <param name="target">Target Scale</param>
    /// <returns></returns>
    private bool ScaleCheck(Vector3 current, Vector3 target) {
        float distance = Vector3.Distance(current, target);
        Debug.Log(distance);
        if(distance <= Mathf.Epsilon) {
            return true;
        }
        else return false;
    }
}
