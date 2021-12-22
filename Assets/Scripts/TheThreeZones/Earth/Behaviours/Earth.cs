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

    [Header("Channels Broadcasting to")]
    [SerializeField] private VoidBroadcastChannelSO gameOverChannel;
    
    //Checker so Burn/Banish doesn't get called more than once.
    private bool isBurning, isBanished;

    //Things currently on top of the Earth
    private List<IWeight> weights;

    private void Awake() {
        isBurning = false;
        weights = new List<IWeight>();
        earthStats.weightCarrying = earthStats.InitialWeightCarrying();
    }

    private void Update() {
        //Set the weight Earth is carrying to the total weights of all IWeight objects on it.
        float weightCarried = (from weight in weights select weight.GetWeight()).Sum();
        earthStats.weightCarrying = (weightCarried < 0) ? 0 : weightCarried;
    }

    public void Burn() {
        if(!isBurning) {
            isBurning = true;
            RaiseGameOver();
            Destroy(gameObject);
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
