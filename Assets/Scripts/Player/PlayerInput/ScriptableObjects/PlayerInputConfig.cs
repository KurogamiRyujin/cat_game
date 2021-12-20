using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "Player Input Config", menuName = "Player/Input/New Config")]
public class PlayerInputConfig : ScriptableObject
{
    [Header("Possible Inputs")]
    public KeyCode playerRight = KeyCode.D;
    public KeyCode playerLeft = KeyCode.A;
    public KeyCode playerUp = KeyCode.W;
    public KeyCode playerDown = KeyCode.S;
    public KeyCode playerLeftMouseButton = KeyCode.Mouse0;
    public KeyCode playerRightMouseButton = KeyCode.Mouse1;
}
