using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

//Platform for everyThing.
//If this is Burned/Banished, it raises a Game Over event.
public class Earth : MonoBehaviour, IBurnable, IBanishable, IStalkable
{
    [Header("Earth Stats")]
    [SerializeField] private EarthStatsSO earthStats;

    [Header("For Burning")]
    [SerializeField] private SpontaneousParticleManagerSO spontaneousParticleManager;
    [SerializeField] private ParticleReferenceSO fire;
    [SerializeField] private ParticleReferenceSO explosion;
    [SerializeField] private int maxFires = 10;
    private int fireCount;
    [SerializeField] private float explosionsInterval = 0.5f;
    private float explosionTimer;
    [SerializeField] private float minXDestructionPoint = -15f;
    [SerializeField] private float maxXDestructionPoint = 15f;
    [SerializeField] private float minZDestructionPoint = -15f;
    [SerializeField] private float maxZDestructionPoint = 15f;
    [SerializeField] private float minYDestructionPointDisplacement = -1f;
    [SerializeField] private float maxYDestructionPointDisplacement = 1f;

    [Header("Channels Broadcasting to")]
    [SerializeField] private VoidBroadcastChannelSO gameOverChannel;
    
    //Checker so Burn/Banish doesn't get called more than once.
    private bool isBurning, isBanished;

    //Things currently on top of the Earth
    private List<IWeight> weights;

    private void Awake() {
        spontaneousParticleManager.LoadParticle(fire);
        spontaneousParticleManager.LoadParticle(explosion);
    }

    private void OnEnable() {
        isBurning = false;
        weights = new List<IWeight>();
        earthStats.weightCarrying = earthStats.InitialWeightCarrying();
        fireCount = 0;
        explosionTimer = 0f;
    }

    private void Update() {
        if(!isBurning) {
            //Set the weight Earth is carrying to the total weights of all IWeight objects on it.
            float weightCarried = (from weight in weights select weight.GetWeight()).Sum();
            earthStats.weightCarrying = (weightCarried < 0) ? 0 : weightCarried;
        }
        else {
            Ignite();
            Explosions();
        }
    }

    public void Burn() {
        if(!isBurning) {
            isBurning = true;
            RaiseGameOver();
            // Destroy(gameObject);
        }
    }

    private void Ignite() {
        fireCount++;

        if(fireCount < maxFires) {
            float explosionPointX = Random.Range(minXDestructionPoint, maxXDestructionPoint);
            float explosionPointY = Random.Range(minYDestructionPointDisplacement, maxYDestructionPointDisplacement);
            float explosionPointZ = Random.Range(minZDestructionPoint, maxZDestructionPoint);
            Vector3 explosionPoint = new Vector3(explosionPointX, explosionPointY + transform.position.y, explosionPointZ);

            Instantiate(spontaneousParticleManager.RequestParticle(fire), explosionPoint, Quaternion.identity);
        }
    }

    private void Explosions() {
        explosionTimer += Time.deltaTime;

        if(explosionTimer > explosionsInterval) {
            explosionTimer = 0f;
            float explosionPointX = Random.Range(minXDestructionPoint, maxXDestructionPoint);
            float explosionPointY = Random.Range(minYDestructionPointDisplacement, maxYDestructionPointDisplacement);
            float explosionPointZ = Random.Range(minZDestructionPoint, maxZDestructionPoint);
            Vector3 explosionPoint = new Vector3(explosionPointX, explosionPointY + transform.position.y, explosionPointZ);

            Instantiate(spontaneousParticleManager.RequestParticle(explosion), explosionPoint, Quaternion.identity);
        }
    }

    public void Banish() {
        if(!isBanished) {
            isBanished = true;
            RaiseGameOver();
            Destroy(gameObject);
        }
    }

    private void RaiseGameOver() {
        if(gameOverChannel != null) {
            gameOverChannel.onEventRaised();
        }
    }

    private void OnCollisionEnter(Collision collision) {
        IWeight weight = collision.gameObject.GetComponent<IWeight>();

        if(weight != null) {
            if(!weights.Contains(weight)) {
                weights.Add(weight);
                // earthStats.weightCarrying += weight.GetWeight();
            }
        }
    }

    private void OnCollisionExit(Collision collision) {
        IWeight weight = collision.gameObject.GetComponent<IWeight>();

        if(weight != null) {
            if(weights.Contains(weight)) {
                weights.Remove(weight);
                // earthStats.weightCarrying -= weight.GetWeight();
            }
        }
    }

    public Vector3 Stalk()
    {
        return gameObject.transform.position;
    }
}
