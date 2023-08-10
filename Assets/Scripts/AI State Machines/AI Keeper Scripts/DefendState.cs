using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefendState : State
{
    public ReturnToStartPositionState ReturnToStartPositionState;
    public KeeperHoldsQuaffleState KeeperHoldsQuaffleState;
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
            else // Caught Quaffle
            {
                Logic.target = null;
                Logic.isMoving = false;

                return KeeperHoldsQuaffleState;
            }
        }
        else
        {
            Logic.target = null;
            Logic.isMoving = false;

            return ReturnToStartPositionState;
        }
    }
}
