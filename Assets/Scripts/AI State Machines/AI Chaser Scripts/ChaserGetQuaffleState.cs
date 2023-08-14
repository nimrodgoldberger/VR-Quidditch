using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaserGetQuaffleState : State
{
    public PlayerLogicManager Keeper;
    public PlayerLogicManager Chaser2;
    public PlayerLogicManager Chaser3;
    public ChaserAdvanceWithQuaffleState AdvanceWithQuaffle;
    public State AdvanceWithOutQuaffle;
    public IdleState Idle;

    private float QuaffleVisibilityRange = 250f;


    public override State RunCurrentState()
    {
        State returnState = this;

        if(Logic.target != Logic.Quaffle)
        {
            //Debug.Log("Quaffle set as target");
            Logic.target = Logic.Quaffle;
        }

        // MAYBE UN-NEEDED
        // Check if the Quaffle is held by you
        if(Logic.IsQuaffleHeldByMe())
        {
            //Debug.Log("I hold the quaffle, Starting Advancing with it");

            // Do something when Quaffle is held by you
            returnState = AdvanceWithQuaffle;
        }
        // Check if the Quaffle is held by our team
        else if(Logic.IsQuaffleHeldByMyTeam())
        {
            //Debug.Log("TeamMate holds the quaffle, Starting Advancing withOUT it");

            // Do something when Quaffle is held by your team
            returnState = AdvanceWithOutQuaffle;
        }
        else // No one in the team holds the Quaffle
        {
            if(!Logic.TryCatchQuaffle()) // I didn't catch the Quaffle
            {
                Logic.MoveAndRotateToTarget();
                return this;
            }
            else // I Caught the Quaffle
            {
                Logic.target = null;
                Logic.isMoving = false;
                //Debug.Log("222222222222222222 I hold the quaffle, Starting Advancing with it");

                return AdvanceWithQuaffle;
            }
        }

        //if (Logic.collisionOccured)
        //{
        //    returnState = Idle;
        //    //Logic.collisionOccured = false;
        //}

        if(Logic.goalScored)
        {
            returnState = Idle;
            Logic.target = null;
            Logic.isMoving = false;
            Logic.StopMoveAndRotateToTarget();

        }

        return returnState;
    }

    public bool QuaffleIsOurs()
    {
        return Logic.Quaffle.transform.parent != null && Logic.Quaffle.transform.parent.CompareTag("Logic");
    }
}
