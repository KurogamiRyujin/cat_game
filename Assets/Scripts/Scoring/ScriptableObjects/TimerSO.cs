using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Keeps track of the time span which is used to display a time format.
/// </summary>
[CreateAssetMenu(fileName = "Timer", menuName = "Scoring/Timer")]
public class TimerSO : ScriptableObject
{
    public TimeSpan currentTime;
}
