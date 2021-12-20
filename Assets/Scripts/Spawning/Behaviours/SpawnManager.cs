using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

//Broadcasts what Thing should be spawned through the selected spawn/pool request channel.
public class SpawnManager : MonoBehaviour
{
    [Header("Spawn Request Channels")]
    [SerializeField] private List<SpawnRequestBroadcasting> spawnRequestChannels = default;
    [SerializeField] private List<PoolingRequestBroadcastChannel> poolingRequestBroadcastChannels = default;

    [Header("Spawn References")]
    [SerializeField] private SpawnableListSO spawnReferences = default;
    [Header("Spawning Logic")]
    [SerializeField] private SpawnDeciderSO spawnLogic;
    [Min(1f)]
    [SerializeField] private float spawnInterval;
    private float timer;
    
    [Header("Channels listening to")]
    [SerializeField] private VoidBroadcastChannelSO gameOverChannel;

    private bool isSpawning;

    private void Awake() {
        isSpawning = true;
        spawnLogic.IndexReset();
        timer = 0f;
    }

    private void OnEnable() {
        //Register events
        gameOverChannel.onEventRaised += OnGameOver;
    }

    private void OnDisable() {
        //Unregister events
        gameOverChannel.onEventRaised -= OnGameOver;
    }

    private void Update() {
        if(isSpawning) {
            timer += Time.deltaTime;

            if(timer > spawnInterval) {
                RaiseSpawnEvent();
                timer = 0f;
            }
        }
    }

    private void RaiseSpawnEvent() {
        int index = Random.Range(0, spawnRequestChannels.Count);

        SpawnReferenceSO spawnReference = spawnLogic.GiveSuggestion(spawnReferences);
        PoolSO thingsPool = spawnReference.pool;

        //Try if we can request a poolable.
        if(thingsPool.CanRequest()) {
            index = Random.Range(0, poolingRequestBroadcastChannels.Count);

            GameObject poolable = poolingRequestBroadcastChannels[index].SpawnRequestEventRaised(thingsPool.Request());
        }//Otherwise, spawn something new and add it to the pool.
        else if(spawnRequestChannels.Count > 0 && spawnReferences.spawnables.Count > 0) {

            //Get all the results from every spawner that subscribed to the spawnRequestChannel
            IEnumerable<GameObject> spawnables = spawnRequestChannels[index].ManySpawnsRequestEventRaise(spawnReference.prefab.GetComponent<ISpawnable>());
            //Iterate each of the spawnable GameObject
            foreach (GameObject spawnable in spawnables) {
                IPoolable poolable = spawnable.GetComponent<IPoolable>();

                if(poolable != null) {
                    thingsPool.AddPool(poolable);
                }
            }
        }
    }

    private void OnGameOver() {
        isSpawning = false;
    }
}
