using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Displays text at the center of the Game scene.
/// </summary>
public class GameSceneCenterText : MonoBehaviour
{
    [SerializeField] private Text centerDisplay;

    [Header("Channels listening to")]
    [SerializeField] private VoidBroadcastChannelSO startGameChannel;
    [SerializeField] private IntegerBroadcastChannel sendTimeChannel;
    [SerializeField] private VoidBroadcastChannelSO gameOverChannel;

    private void OnEnable() {
        //Register events
        startGameChannel.onEventRaised += OnStartGame;
        sendTimeChannel.onEventRaised += UpdateTimer;
        gameOverChannel.onEventRaised += OnGameOver;
    }

    private void OnDisable() {
        //Unregister events
        startGameChannel.onEventRaised -= OnStartGame;
        sendTimeChannel.onEventRaised -= UpdateTimer;
        gameOverChannel.onEventRaised -= OnGameOver;
    }

    private void OnStartGame() {
        centerDisplay.text = "START!";
        StartCoroutine(StartDisableCoroutine());
    }

    private IEnumerator StartDisableCoroutine() {
        yield return new WaitForSeconds(1f);
        centerDisplay.enabled = false;
    }

    private void UpdateTimer(int newTime) {
        centerDisplay.text = newTime.ToString();
    }

    private void OnGameOver() {
        centerDisplay.text = "GAME OVER";
        centerDisplay.enabled = true;
    }
}
