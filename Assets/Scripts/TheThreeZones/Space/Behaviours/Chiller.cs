using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chiller : MonoBehaviour
{
    [SerializeField] private float chillDuration = 10f;

    private void OnTriggerEnter(Collider collider) {
        IStatusable statusable = collider.gameObject.GetComponent<IStatusable>();

        if(statusable != null) {
            statusable.ApplyStatus(new Chill(chillDuration));
        }
    }
}
