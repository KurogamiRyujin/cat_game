using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Initializes teh current player scores to 0.
/// </summary>
public class CurrentScoreInit : MonoBehaviour
{
    [SerializeField] private ScoreSO runtimePlayerScore;

    private void Start() {
        runtimePlayerScore.currentScore = 0;
        runtimePlayerScore.hiScore = 0;
    }
}
