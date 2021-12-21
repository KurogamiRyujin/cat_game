using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceCubeThing : Thing, ISpawnable, IPoolable, IPushable, IWeight, IBurnable, IStatusable
{
    [Header("Melting")]
    [SerializeField] private GameObject iceCubeModelToMelt;
    [SerializeField] private float meltInterval = 3f;
    [SerializeField] private int heatIncrease = 0;

    [Header("Boiler Condition")]
    [SerializeField] private BoilerConditionSO boilerCondition;

    public event Action<IPoolable> OnRelease;
    public event Action<IPoolable> OnDestroyEvent;

    private Rigidbody rb;
    private float meltTimer;

    private void Awake() {
        rb = GetComponent<Rigidbody>();
        meltTimer = 0f;
    }

    private void OnDestroy() {
        if(OnDestroyEvent != null) {
            OnDestroyEvent(this);
        }
    }

    protected override void Start()
    {
        currentThingStats.AddStatus(new Chill(5f));
        InitStats();
    }

    protected override void InitStats()
    {
        heatIncrease = 0;
        iceCubeModelToMelt.transform.localPosition = Vector3.zero;
        iceCubeModelToMelt.transform.localScale = Vector3.one;
        base.InitStats();
    }

    protected override void Update() {
        if(!currentThingStats.CheckStatusFor(typeof(Chill))) {
            Melt();
        }

        base.Update();
    }

    /// <summary>
    /// Increase this Thing's heatValue until it reaches 0.
    /// </summary>
    private void Melt() {
        meltTimer += Time.deltaTime;

        if(meltTimer > meltInterval) {
            meltTimer = 0f;
            heatIncrease++;
        }

        if(currentThingStats.heatValue >= 0) {
            Release();
        }
        else {
            //Melt the IceCube
            currentThingStats.heatModifiers += heatIncrease;

            Vector3 iceCubeScale = iceCubeModelToMelt.transform.localScale;
            iceCubeScale = new Vector3(iceCubeScale.x, Mathf.Abs((float)currentThingStats.heatValue / thingStats.heatValue), iceCubeScale.z);
            iceCubeModelToMelt.transform.localScale = Vector3.Lerp(iceCubeModelToMelt.transform.localScale, iceCubeScale, Time.deltaTime);
            
            Vector3 adjustPos = new Vector3(iceCubeModelToMelt.transform.localPosition.x, (Mathf.Abs((float)currentThingStats.heatValue / thingStats.heatValue) - 1f)/2f, iceCubeModelToMelt.transform.localPosition.z);
            iceCubeModelToMelt.transform.localPosition = Vector3.Lerp(iceCubeModelToMelt.transform.localPosition, adjustPos, Time.deltaTime);
        }
    }

    public void Activate()
    {
        currentThingStats.AddStatus(new Chill(10f));
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
        return SpawnReferenceSO.SpawnType.ICE_CUBE;
    }
}
