using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Scriptable Object that gives suggestions on what to spawn for an entity that can request spawnables.
[CreateAssetMenu(fileName = "New Spawn Decider", menuName = "Spawning/Spawn Decider")]
public class SpawnDeciderSO : ScriptableObject
{
    public enum LogicMode {
        RANDOM,
        SEQUENTIAL
    }
    
    //Mode of how to suggest spawnables
    public LogicMode logicMode;
    
    //Index used by the Sequential Logic
    private int sequentialLogicIndex;

    public void IndexReset() {
        sequentialLogicIndex = 0;
    }

    public SpawnReferenceSO GiveSuggestion(SpawnableListSO spawnableList) {
        SpawnReferenceSO spawnReference = null;

        //pick from the list based on spawntype
        switch(logicMode) {
            case LogicMode.RANDOM:
            spawnReference = RandomLogic(spawnableList);
            break;
            case LogicMode.SEQUENTIAL:
            spawnReference = SequentialLogic(spawnableList);
            break;
        }

        return spawnReference;
    }

    //RandomLogic
    private SpawnReferenceSO RandomLogic(SpawnableListSO spawnableList) {
        SpawnReferenceSO suggestion = null;
        int selectedTypeIndex = UnityEngine.Random.Range(0, Enum.GetNames(typeof(SpawnReferenceSO.SpawnType)).Length);

        //Randomly suggests a spawnable
        suggestion = spawnableList.spawnables.Find(x => Array.IndexOf(Enum.GetValues(typeof(SpawnReferenceSO.SpawnType)), x.GetSpawnType()) == selectedTypeIndex);

        return suggestion;
    }

    //SequentialLogic
    private SpawnReferenceSO SequentialLogic(SpawnableListSO spawnableList) {
        SpawnReferenceSO suggestion = null;
        
        //If it goes beyond list count, reset index.
        if(sequentialLogicIndex >= spawnableList.spawnables.Count) {
            IndexReset();
        }

        //Assign suggestion and increment sequentialLogicIndex
        suggestion = spawnableList.spawnables[sequentialLogicIndex++];

        return suggestion;
    }

    //OverheatLogic
}
