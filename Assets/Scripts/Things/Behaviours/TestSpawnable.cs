using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSpawnable : Thing, ISpawnable, IPoolable, IPushable, IWeight, IBurnable, IStatusable
{
    [SerializeField] private float pushDuration = 3f;
    private Rigidbody rb;
    // private bool isBeingPushed;

    [SerializeField] private BoilerConditionSO boilerCondition;

    public event Action<IPoolable> OnRelease;
    public event Action<IPoolable> OnDestroyEvent;

    protected override void Awake() {
        rb = GetComponent<Rigidbody>();
        // isBeingPushed = false;
    }

    private void OnDestroy() {
        OnDestroyEvent(this);
    }

    public void Push(float strength, Vector3 direction) {
        PhysicsPush(strength, direction);
        // if(isBeingPushed) {
        //     isBeingPushed = false;
        //     StopAllCoroutines();
        // }
        // isBeingPushed = true;
        // StartCoroutine(Pushed(strength, direction));
    }

    public void HoldStill() {
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }

    private void PhysicsPush(float strength, Vector3 direction) {
        rb.AddForce(direction * strength);
    }

    private IEnumerator Pushed(float strength, Vector3 direction) {
        //strength determines distance
        Vector3 newPos = gameObject.transform.position + (direction * strength);
        float originalDistance = Vector3.Distance(gameObject.transform.position, newPos);
        float timer = 0f;

        //Move until object reaches newPos
        while(timer < pushDuration) {
            timer += Time.deltaTime;
            float distance = Vector3.Distance(gameObject.transform.position, newPos);
            newPos.y = gameObject.transform.position.y;
            gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, newPos, strength * Time.deltaTime);
            
            yield return null;
        }
        // isBeingPushed = false;
    }

    public GameObject Spawn(Vector3 position) {
        return Instantiate<GameObject>(gameObject, position, Quaternion.identity);
    }

    public SpawnReferenceSO.SpawnType SpawnType() {
        return SpawnReferenceSO.SpawnType.TEST;
    }

    public void Activate() {
        OnActivate();
        gameObject.SetActive(true);
    }

    public void Release()
    {
        currentThingStats.CleanseAllStatus();
        gameObject.SetActive(false);
        OnRelease(this);
    }

    public void OnActivate()
    {
        currentThingStats.AddStatus(new ChillImmunity(thingStats.chillImmunityOnSpawnDuration));
        InitStats();
    }

    public GameObject GetGameObject()
    {
        return gameObject;
    }

    public float GetWeight()
    {
        return currentThingStats.weight;
    }

    public void Burn()
    {
        boilerCondition.temperature += currentThingStats.heatValue;
    }

    public void ApplyStatus(StatusEffect statusEffect)
    {
        currentThingStats.AddStatus(statusEffect);
    }
}
