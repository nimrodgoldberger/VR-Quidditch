using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaserAdvanceWithOutQuaffleState : State
{
    public IdleState Idle;

    public override State RunCurrentState()
    {
        State returnState = this;


        if (Logic.goalScored)
        {
            returnState = Idle;
            
        }
        

        return returnState;
    }
}
