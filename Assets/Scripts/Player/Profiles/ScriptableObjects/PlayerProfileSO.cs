using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Scriptable object containing data about the player.
[CreateAssetMenu(fileName = "New Player Runtime Data", menuName = "Player/Data/Runtime Data")]
public class PlayerProfileSO : ScriptableObject
{
    // public string playerName;
    // //Player's personal hiScore.
    // public ulong hiScore;
    public PlayerData playerData;
    public ScoreSO scoreStats;
}
