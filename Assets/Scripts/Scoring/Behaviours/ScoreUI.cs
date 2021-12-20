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

    private void Awake() {
        scoreText = GetComponent<Text>();
    }

    // Update is called once per frame
    private void Update()
    {
        switch(scoreType) {
            case ScoreSO.ScoreType.CURRENT:
            scoreText.text = scoreReference.currentScore.ToString();
            break;
            case ScoreSO.ScoreType.HI:
            scoreText.text = scoreReference.HiScore().ToString();
            break;
        }
    }
}
