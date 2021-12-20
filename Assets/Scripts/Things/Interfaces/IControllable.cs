using UnityEngine;

//For entities that can be controlled
public interface IControllable
{
    void PhysicsControl();
    void Control();
    GameObject GetGameObject();
}