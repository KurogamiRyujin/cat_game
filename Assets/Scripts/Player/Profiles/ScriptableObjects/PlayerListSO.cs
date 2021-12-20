using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Scriptable Object holding a reference to the players
[CreateAssetMenu(fileName = "New Player List", menuName = "Player/Data/Player List")]
public class PlayerListSO : ScriptableObject
{
    public List<PlayerData> playerList = new List<PlayerData>();
}
