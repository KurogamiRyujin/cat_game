using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

//Cat Behaviour
//Also the player character.
public class Cat : Thing, ISpawnable, IControllable, IPoolable, IWeight, IBurnable, IStatusable
{
    [Header("Player Input Config")]
    [SerializeField] private PlayerInputConfig playerInputConfig;
    [Header("Cat Stats")]
    [SerializeField] private CatStatsSO catStats;

    [Header("Channel broadcasting to")]
    [SerializeField] private PushRequestChannel pushRequestChannel;

    [Header("Boiler Condition")]
    [SerializeField] private BoilerConditionSO boilerCondition;

    public event Action<IPoolable> OnRelease;
    public event Action<IPoolable> OnDestroyEvent;

    //Animation Events
    public UnityEvent<AnimationEventParameter> OnMove;
    public UnityEvent<AnimationEventParameter> OnPush;

    private void OnDestroy() {
        if(OnDestroyEvent != null) {
            OnDestroyEvent(this);
        }
    }

    //Controls involving physics
    public void PhysicsControl() {
        if(gameObject == null) {
            return;
        }
        bool isMoving = false;
        float horizontal = 0f;
        float vertical = 0f;
        //Movement
        if(Input.GetKey(playerInputConfig.playerUp)) {
            vertical += 1f;
            isMoving = true;
        }
        if(Input.GetKey(playerInputConfig.playerDown)) {
            vertical -= 1f;
            isMoving = true;
        }
        if(Input.GetKey(playerInputConfig.playerLeft)) {
            horizontal -= 1f;
            isMoving = true;
        }
        if(Input.GetKey(playerInputConfig.playerRight)) {
            horizontal += 1f;
            isMoving = true;
        }

        Camera camera = Camera.main;

        Vector3 cameraForward = camera.transform.forward;
        Vector3 cameraRight = camera.transform.right;
        cameraForward.y = 0f;
        cameraForward.Normalize();
        cameraRight.y = 0f;
        cameraRight.Normalize();

        Vector3 direction = (cameraForward * vertical) + (cameraRight * horizontal);

        if(isMoving) {
            
            gameObject.transform.Translate(direction * catStats.movementSpeed * Time.deltaTime, Space.World);
        }

        OnMove.Invoke(new AnimationEventParameter("IsMoving", isMoving));
    }
    
    //General controls
    public void Control() {
        if(gameObject == null) {
            return;
        }
        Camera camera = Camera.main;

        //Look at Mouse
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);
        Vector3 lookAtPoint;
        if(Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, 1<<3)) {
            lookAtPoint = new Vector3(hit.point.x, gameObject.transform.position.y, hit.point.z);
            gameObject.transform.LookAt(lookAtPoint, Vector3.up);
        }

        //Push
        if(Input.GetKeyDown(playerInputConfig.playerLeftMouseButton)) {
            OnPush.Invoke(new AnimationEventParameter("Push", true));
            Push();
        }
        //Hard Push
        if(Input.GetKeyDown(playerInputConfig.playerRightMouseButton)) {
            OnPush.Invoke(new AnimationEventParameter("Push", true));
            HardPush();
        }
    }

    public GameObject Spawn(Vector3 position) {
        return Instantiate<GameObject>(gameObject, position, Quaternion.identity);
    }

    public SpawnReferenceSO.SpawnType SpawnType() {
        return SpawnReferenceSO.SpawnType.CAT;
    }

    public GameObject GetGameObject() {
        return gameObject;
    }

    //Push
    private void Push() {
        pushRequestChannel.PushEventRequestEventRaised(PushSO.PushType.NORMAL);
    }

    //Hard Push
    private void HardPush() {
        pushRequestChannel.PushEventRequestEventRaised(PushSO.PushType.HARD);
    }

    public void Activate() {
        currentThingStats.AddStatus(new ChillImmunity(thingStats.chillImmunityOnSpawnDuration));
        InitStats();
        gameObject.SetActive(true);
    }

    public void Release()
    {
        currentThingStats.CleanseAllStatus();
        gameObject.SetActive(false);
        if(OnRelease != null) {
            OnRelease(this);
        }
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
