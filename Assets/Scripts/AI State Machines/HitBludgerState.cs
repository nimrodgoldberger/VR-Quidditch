using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBludgerState : State
{

    public IdleState idle;
    int bludgerCloseIndex;
    float bludgerTryHittingRange = 2f;

    public override State RunCurrentState()
    {
        int noBludgerIsClose = 3;
        State returnState = this; //just for the testing
        Debug.Log("Bludger is close! might hit it!");

        bludgerCloseIndex = Logic.IsABludgerInRange(bludgerTryHittingRange);

        if (bludgerCloseIndex != noBludgerIsClose)
        {
            //TO DO Animation activation
            Logic.BudgerWasHit(bludgerCloseIndex);
            Debug.Log("hit bludger!!");
            Logic.ResetTarget();//needed in order to change the chaser that the beater defends
            returnState = idle;
        }
        else
        {
            Logic.MoveAndRotateToTarget();
            //TO DO make beater fly randomly around chaser it defends
            returnState = this;
        }

        return returnState;
    }

}