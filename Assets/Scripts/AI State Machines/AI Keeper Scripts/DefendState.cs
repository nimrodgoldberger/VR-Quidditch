using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefendState : State
{
    public ReturnToStartPositionState ReturnToStartPositionState;
    public KeeperHoldsQuaffleState KeeperHoldsQuaffleState;
    float QuaffleVisibilityRange = 120f; // TODO Adjust I think 70 should be okay


    public override State RunCurrentState()
    {
        if(Logic.target != Logic.Quaffle)
        {
            Debug.Log($"Now Keeper of team {Logic.PlayerTeam.ToString()} Entered Defend State");
            Logic.target = Logic.Quaffle;
        }

        if(Logic.IsQuaffleCloseToMyTeamGoals(QuaffleVisibilityRange))
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
