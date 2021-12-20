using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Stats for the Cat character.
[CreateAssetMenu(fileName = "Cat Stats", menuName = "Thing/Cat Stats")]
public class CatStatsSO : ThingStatsSO
{
    public float movementSpeed = 2f;
    public PushSO pushStats;
}
