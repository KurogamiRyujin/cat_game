using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// 
/// </summary>
public class PeopleThing : Thing, ISpawnable, IPoolable, IPushable, IWeight, IBurnable, IStatusable
{
    [Header("People Stats")]
    [SerializeField] private PeopleStatsSO peopleStats;
    [SerializeField] private float changeLookInterval = 3f;

    [Header("Spawning")]
    [SerializeField] private SpawnableListSO spawnableList;
    [SerializeField] private SpawnDeciderSO spawningLogic;
    [SerializeField] private Spawner spawner;
    
    [Header("Boiler Condition")]
    [SerializeField] private BoilerConditionSO boilerCondition;

    private Rigidbody rb;
    private float birthTimer;
    private float lookTimer;
    private Vector3 moveDirection;

    public event Action<IPoolable> OnRelease;
    public event Action<IPoolable> OnDestroyEvent;

    private void Awake() {
        rb = GetComponent<Rigidbody>();
        birthTimer = 0f;
        lookTimer = 11f;
        moveDirection = gameObject.transform.position + transform.forward;
    }

    private void OnDestroy() {
        if(OnDestroyEvent != null) {
            OnDestroyEvent(this);
        }
    }

    private void FixedUpdate() {
        Move();
    }

    protected override void Update() {
        base.Update();

        birthTimer += Time.deltaTime;

        if(birthTimer > peopleStats.birthInterval) {
            birthTimer = 0f;

            Birth();
        }
    }

    /// <summary>
    /// Spawn an offspring.
    /// </summary>
    private void Birth() {
        SpawnReferenceSO spawnReference = spawningLogic.GiveSuggestion(spawnableList);
        PoolSO thingsPool = spawnReference.pool;

        //Try if we can request a poolable.
        if(thingsPool.CanRequest()) {
            spawner.Spawn(thingsPool.Request());
        }//Otherwise, spawn something new and add it to the pool.
        else {
            IPoolable poolable = spawner.Spawn(spawnReference.prefab.GetComponent<ISpawnable>()).GetComponent<IPoolable>();

            thingsPool.AddPool(poolable);
        }
    }

    /// <summary>
    /// Movement
    /// </summary>
    private void Move() {
        float horizontal = UnityEngine.Random.Range(-1f, 1f);
        float vertical = UnityEngine.Random.Range(-1f, 1f);

        lookTimer += Time.deltaTime;
        if(lookTimer > changeLookInterval) {
            lookTimer = 0f;

            moveDirection = new Vector3(horizontal, 0f, vertical).normalized;
            transform.forward = moveDirection;
        }

        gameObject.transform.position = Vector3.MoveTowards(transform.position, moveDirection + transform.position, peopleStats.movementSpeed * Time.fixedDeltaTime);
    }

    public void Activate()
    {
        currentThingStats.AddStatus(new ChillImmunity(thingStats.chillImmunityOnSpawnDuration));
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
        return SpawnReferenceSO.SpawnType.PEOPLE;
    }
}
