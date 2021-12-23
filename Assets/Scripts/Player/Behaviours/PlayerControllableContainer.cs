using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Instantiates the player character and connects it to the player controller.
public class PlayerControllableContainer : MonoBehaviour
{
    //Character prefabs that can be requested as the Player Character
    [Header("Spawnable List")]
    [SerializeField] private SpawnableListSO spawnList;
    
    [Header("Channel broadcasting to")]
    [SerializeField] private SpawnRequestBroadcasting playerSpawnRequest;
    [SerializeField] private PoolingRequestBroadcastChannel playerPoolingRequestBroadcastChannel;
    [Header("Channels listening to")]
    // [SerializeField] private PoolingReleaseBroadcastChannel playerPoolingReleaseBroadcastChannel;
    [SerializeField] private VoidBroadcastChannelSO gameOverChannel;

    [Header("Player Controller")]
    [SerializeField] private PlayerController playerController;
    [SerializeField] private float playerRespawnTime = 3f;
    private float respawnTimer;
    // [Header("Pooling")]
    // [SerializeField] private PoolSO playerPool;

    //Currently controlled controllable
    private IControllable currentControllable;
    private SpawnReferenceSO selectedSpawn;
    private bool isSpawning;
    private bool isDead;

    private void Awake() {
        isSpawning = true;
        respawnTimer = 0f;
        isDead = false;
    }

    //Test
    private void Start() {
        selectedSpawn = spawnList.spawnables[0];
        RequestControllable();
    }

    private void OnEnable() {
        //Register events
        // playerPoolingReleaseBroadcastChannel.onEventRaised += OnControllableRelease;
        gameOverChannel.onEventRaised += OnGameOver;
    }

    private void OnDisable() {
        //Unregister events
        // playerPoolingReleaseBroadcastChannel.onEventRaised -= OnControllableRelease;
        gameOverChannel.onEventRaised -= OnGameOver;
    }

    private void Update() {
        if(isDead) {
            respawnTimer += Time.deltaTime;

            if(respawnTimer >= playerRespawnTime) {
                respawnTimer = 0f;
                isDead = false;
                RequestControllable();
            }
        }
    }

    //Request a controllable entity.
    //If a pool is available, use that.
    //Otherwise, spawn a new one.
    private void RequestControllable() {
        if(!isSpawning) {
            return;
        }

        GameObject requestedControllable = null;
        PoolSO playerPool = selectedSpawn.pool;
        
        if(playerPool != null) {
            if(playerPool.CanRequest()) {
                requestedControllable = playerPoolingRequestBroadcastChannel.SpawnRequestEventRaised(playerPool.Request());
            }
            else {
                if(playerSpawnRequest != null) {
                    requestedControllable = playerSpawnRequest.SpawnRequestEventRaised(selectedSpawn.prefab.GetComponent<ISpawnable>());
                    playerPool.AddPool(requestedControllable.GetComponent<IPoolable>());
                    requestedControllable.GetComponent<IPoolable>().OnRelease += OnControllableRelease;
                }
            }
        }

        currentControllable = requestedControllable.GetComponent<IControllable>();

        if(currentControllable != null && currentControllable != playerController.puppet) {
            playerController.puppet = currentControllable;
        }
    }

    private void OnControllableRelease(IPoolable poolable) {
        // selectedSpawn.pool.Release(poolable);
        isDead = true;
        // RequestControllable();
    }

    private void OnGameOver() {
        isSpawning = false;
    }
}
