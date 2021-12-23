using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//UI behaviour for displaying the score/hi-score.
public class ScoreUI : MonoBehaviour
{
    [Header("Score Reference")]
    [SerializeField] private ScoreSO scoreReference;

    [Header("Score Type")]
    [SerializeField] private ScoreSO.ScoreType scoreType;

    private Text scoreText;
    private ulong currentScore;
    private ulong incrementer;

    private void Awake() {
        currentScore = 0;
        incrementer = 1;
        scoreText = GetComponent<Text>();
    }

    // Update is called once per frame
    private void Update()
    {
        switch(scoreType) {
            case ScoreSO.ScoreType.CURRENT:
            scoreText.text = LerpToScore(scoreReference.currentScore).ToString();
            break;
            case ScoreSO.ScoreType.HI:
            scoreText.text = LerpToScore(scoreReference.HiScore()).ToString();
            break;
        }
    }

    private ulong LerpToScore(ulong target) {
        currentScore = (currentScore < target) ? currentScore+(incrementer++) : target;
        return currentScore;
    }
}
