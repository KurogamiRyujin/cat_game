using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Handles how the score increases.
public class ScoringLogic : MonoBehaviour
{
    [Header("Stat References")]
    [SerializeField] private ScoreSO scoreReference;
    [SerializeField] private TimerSO stopwatch;
    [SerializeField] private EarthStatsSO earthStats;//Not yet used

    [Header("Channels listening to")]
    [SerializeField] private VoidBroadcastChannelSO gameOverChannel;

    [Header("Score Modifiers")]
    [Min(1)]
    [SerializeField] private ulong scoreIncreasePerTick = 1;
    [Min(0.01f)]
    [SerializeField] private float tickWaitInSeconds = 1f;
    private float timer;
    private bool isRunning;

    private void OnEnable() {
        timer = 0f;
        stopwatch.currentTime = TimeSpan.FromSeconds(0);
        scoreReference.currentScore = 0;
        isRunning = true;

        //Register events
        gameOverChannel.onEventRaised += OnGameOver;
    }

    private void OnDisable() {
        //Unregister events
        gameOverChannel.onEventRaised -= OnGameOver;
    }

    private void Update() {
        if(isRunning) {
            ScoreOverTime();
        }
    }

    private void ScoreOverTime() {
        ulong scoreThisTick = 0;

        timer += Time.deltaTime;
        stopwatch.currentTime += TimeSpan.FromSeconds(Time.deltaTime);

        if(timer > tickWaitInSeconds) {
            timer = 0f;
            scoreThisTick += scoreIncreasePerTick;
        }

        //Aside from the score over time, Earth's stats will also be used to manipulate the score.
        //Possibly other factors as well.

        scoreReference.currentScore += scoreThisTick;
    }

    private void OnGameOver() {
        isRunning = false;
    }
}
