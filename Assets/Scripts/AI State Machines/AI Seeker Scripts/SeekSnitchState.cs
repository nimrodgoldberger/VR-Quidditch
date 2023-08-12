using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeekSnitchState : State
{

    bool canSeeSnitch = false;
    [SerializeField] private float SnitchCatchingRange = 5f;
    [SerializeField] private float SnitchVisibilityRange = 50f;

    public IdleState idleState;
    public ReturnToStartPositionState returnToStartPositionState;
    public HoldSnitchState holdSnitchState;
    public override State RunCurrentState()
    {

        State returnState = this; //just for the testing
       

        if(Logic.target != Logic.Snitch)
        {
            Logic.target = Logic.Snitch;
            Logic.SetRotationSpeed(100.0f);
        }

        canSeeSnitch = Logic.IsSnitchInRange(SnitchVisibilityRange);

        if(canSeeSnitch)
        {
            if(!Logic.TryCatchSnitch())
            { 
                //Debug.Log("I am seeking the snitch");
                Logic.MoveAndRotateToTarget();
            }
            else // Caught Snitch
            {
                Logic.target = null;
                Logic.isMoving = false; //PUT THE COMMENT FOR THE ANIMATIONS TO WORK
                returnState = holdSnitchState;
            }
        }
        else
        {
            // TODO might add idle state if snitch far
            //Debug.Log("Returning to idleState because Snitch is too far away!?!?!?!");
            returnState = idleState;
        }

        return returnState;
    }
}
