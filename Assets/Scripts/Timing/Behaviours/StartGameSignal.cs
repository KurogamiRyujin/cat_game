using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Signals when the game starts.
/// </summary>
public class StartGameSignal : MonoBehaviour
{
    [Header("Channels broadcasting to")]
    [SerializeField] private VoidBroadcastChannelSO startGameChannel;
    [SerializeField] private IntegerBroadcastChannel timerSendChannel;

    public UnityEvent OnStartGame;

    [SerializeField] private int countDownTime = 3;
    
    private float timer;

    private void Awake() {
        timer = 0f;
    }

    private void Start() {
        BroadcastNewTime();
    }

    private void Update() {
        timer += Time.deltaTime;

        if(timer >= 1f) {
            timer = 0f;
            countDownTime--;
        }

        if(countDownTime <= 0) {
            StartGame();
        }
        else {
            BroadcastNewTime();
        }
    }

    private void StartGame() {
        if(startGameChannel.onEventRaised != null) {
            startGameChannel.onEventRaised();
        }

        OnStartGame.Invoke();
        gameObject.SetActive(false);
    }

    private void BroadcastNewTime() {
        timerSendChannel.IntegerEventRaised(countDownTime);
    }
}
