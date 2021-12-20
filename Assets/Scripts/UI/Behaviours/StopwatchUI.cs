using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StopwatchUI : MonoBehaviour
{
    [SerializeField] private TimerSO stopwatch;
    private Text stopwatchText;

    private void Awake() {
        stopwatchText = GetComponent<Text>();
    }

    // Update is called once per frame
    private void Update()
    {
        //Display the time in HH:MM:SS
        stopwatchText.text = string.Format("{0:D2}:{1:D2}:{2:D2}", stopwatch.currentTime.Hours, stopwatch.currentTime.Minutes, stopwatch.currentTime.Seconds);
    }
}
