using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// VolcanoThing behaviour. Adjusts the effect of eruption event.
/// </summary>
public class VolcanoThing : Thing, ISpawnable, IPoolable, IPushable, IWeight, IBurnable, IStatusable
{
    [Header("Volcano Stats")]
    [SerializeField] private VolcanoStatsSO volcanoStats;

    [Header("Boiler Condition")]
    [SerializeField] private BoilerConditionSO boilerCondition;

    [Header("For Eruption")]
    [SerializeField] private BoilerStatsSO boilerStats;
    [SerializeField] private UnityEvent OnEruption;
    [SerializeField] private UnityEvent OnEruptionEnd;

    [Header("Channels listening to")]
    [SerializeField] private VoidBroadcastChannelSO eruptionBroadcastChannel;
    public event Action<IPoolable> OnRelease;
    public event Action<IPoolable> OnDestroyEvent;

    private Rigidbody rb;

    private void Awake() {
        rb = GetComponent<Rigidbody>();
    }

    protected override void Start()
    {
        currentThingStats.AddStatus(new ChillImmunity());
        InitStats();
        boilerStats.eruptionModifierModifier += volcanoStats.eruptionModifierModifier;
    }

    private void OnDestroy() {
        if(OnDestroyEvent != null) {
            OnDestroyEvent(this);
        }
    }

    private void OnEnable() {
        //Register events
        eruptionBroadcastChannel.onEventRaised += Erupt;
    }

    private void OnDisable() {
        //Unregister events
        eruptionBroadcastChannel.onEventRaised -= Erupt;
    }

    private void Erupt() {
        OnEruption.Invoke();
        StartCoroutine(Eruption());
    }

    private IEnumerator Eruption() {
        yield return new WaitForSeconds(volcanoStats.eruptionDuration);
        EruptEnd();
    }

    private void EruptEnd() {
        OnEruptionEnd.Invoke();
        boilerCondition.temperature += volcanoStats.postEruptionCooling;
    }

    public void Activate()
    {
        boilerStats.eruptionModifierModifier += volcanoStats.eruptionModifierModifier;
        currentThingStats.AddStatus(new ChillImmunity());
        InitStats();
        gameObject.SetActive(true);
    }

    public void ApplyStatus(StatusEffect statusEffect)
    {
        currentThingStats.AddStatus(statusEffect);
    }

    public void Burn()
    {
        boilerCondition.temperature += currentThingStats.heatValue;
    }

    public GameObject GetGameObject()
    {
        return gameObject;
    }

    public float GetWeight()
    {
        return currentThingStats.weight;
    }

    public void HoldStill()
    {
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }

    public void Push(float strength, Vector3 direction)
    {
        PhysicsPush(strength, direction);
    }

    private void PhysicsPush(float strength, Vector3 direction) {
        rb.AddForce(direction * strength);
    }

    public void Release()
    {
        boilerStats.eruptionModifierModifier -= volcanoStats.eruptionModifierModifier;
        currentThingStats.CleanseAllStatus();
        gameObject.SetActive(false);
        if(OnRelease != null) {
            OnRelease(this);
        }
    }

    public GameObject Spawn(Vector3 position)
    {
        return Instantiate<GameObject>(gameObject, position, Quaternion.identity);
    }

    public SpawnReferenceSO.SpawnType SpawnType()
    {
        return SpawnReferenceSO.SpawnType.VOLCANO;
    }
}
