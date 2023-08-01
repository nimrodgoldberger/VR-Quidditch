using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefendState : State
{
    public ReturnToStartPositionState ReturnToStartPositionState;
    float QuaffleVisibilityRange = 30f;

    public override State RunCurrentState()
    {
        if(Logic.target != Logic.Quaffle)
        {
            Logic.target = Logic.Quaffle;
        }

        if(Logic.IsQuaffleInRange(QuaffleVisibilityRange))
        {
            if(!Logic.TryCatchQuaffle())
            {
                Logic.MoveAndRotateToTarget();
                return this;
            }
            else
            {
                return ReturnToStartPositionState;
            }
        }
        else
        {
            return ReturnToStartPositionState;
        }
    }
}
