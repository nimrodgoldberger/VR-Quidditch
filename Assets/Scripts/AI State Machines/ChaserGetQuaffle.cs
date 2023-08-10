using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaserGetQuaffle : State
{
    public PlayerLogicManager Keeper;
    public PlayerLogicManager Chaser2;
    public PlayerLogicManager Chaser3;
    public State AdvanceWithQuaffle;
    public State AdvanceWithOutQuaffle;
    private float QuaffleVisibilityRange = 250f;


    public override State RunCurrentState()
    {  
        State returnState = this;

        if(Logic.target != Logic.Quaffle)
        {
            Logic.target = Logic.Quaffle;
        }

        // MAYBE UN-NEEDED
        // Check if the Quaffle is held by you
        if(Logic.IsQuaffleHeldByMe())
        {
            // Do something when Quaffle is held by you
            returnState = AdvanceWithQuaffle;
        }
        // Check if the Quaffle is held by our team
        else if(Logic.IsQuaffleHeldByMyTeam())
        {
            // Do something when Quaffle is held by your team
            returnState= AdvanceWithOutQuaffle;
        }
        else
        {
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

                    return AdvanceWithQuaffle;
                }
            }
            else
            {
                Logic.target = null;
                Logic.isMoving = false;

                //return IdleState;  // return this;
            }
        }

        return returnState;
    }

    public bool QuaffleIsOurs()
    {
        return Logic.Quaffle.transform.parent != null && Logic.Quaffle.transform.parent.CompareTag("Logic");
    }


}
