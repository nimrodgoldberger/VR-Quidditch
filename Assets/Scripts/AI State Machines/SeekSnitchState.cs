using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeekSnitchState : State
{
    public ReturnToStartPositionState ReturnToStartPositionState;
    public override State RunCurrentState()
    {
        Debug.Log("I am seeking the snitch");
        return this;
    }
}
