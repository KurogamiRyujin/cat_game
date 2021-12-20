using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Holds a score record.
[CreateAssetMenu(fileName = "New Score Record", menuName = "Scoring/Score Record")]
public class ScoreSO : ScriptableObject
{
    public enum ScoreType {
        CURRENT,
        HI
    }
    [Min(0)]
    public ulong currentScore = 0;
    [Min(0)]
    public ulong hiScore = 0;

    //Updates HiScore
    public ulong HiScore() {
        if(currentScore > hiScore) {
            hiScore = currentScore;
        }
        return hiScore;
    }
}
