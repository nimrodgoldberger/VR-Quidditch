using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeekSnitchState : State
{

    bool canCatchTarget;
    float SnitchCatchingRange = 3f;

    public ReturnToStartPositionState ReturnToStartPositionState;
    public HoldSnitchState holdSnitchState;
    public override State RunCurrentState()
    {

        State returnState = this; //just for the testing
        Debug.Log("I am seeking the snitch");

        if (Logic.target != Logic.Snitch)
        {
            Logic.target = Logic.Snitch;
        }

        canCatchTarget = Logic.IsSnitchInRange(SnitchCatchingRange);

        if (canCatchTarget)
        {
            if (!Logic.TryCatchSnitch())
            {
                Logic.MoveAndRotateToTarget();
                return this;
            }
            else // Caught Snitch
            {
                Logic.target = null;

                return holdSnitchState;
            }
        }
        else
        {
            //might add idle state if snitch far
            returnState = this;
        }

        return returnState;
    }
}
