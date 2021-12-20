using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPushable
{
    void Push(float strength, Vector3 direction);
    void HoldStill();
}
