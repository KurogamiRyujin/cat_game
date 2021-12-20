using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Player controller. Controls an IControllable puppet.
public class PlayerController : MonoBehaviour
{
    public IControllable puppet;

    // Update is called once per frame
    private void FixedUpdate()
    {
        if(puppet != null && puppet.GetGameObject() != null) {
            puppet.PhysicsControl();
        }
    }

    private void Update() {
        if(puppet != null && puppet.GetGameObject() != null) {
            puppet.Control();
        }
    }
}
