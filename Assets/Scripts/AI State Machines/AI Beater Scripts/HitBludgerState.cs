using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBludgerState : State
{

    public IdleState idle;
    public DefendChaserState defendChaser;
    private int bludgerCloseIndex;
    private int bludgerChaseIndex;
    private float bludgerClosenessRange = 15f;
    private float bludgerTryHittingRange = 0.5f;

    public override State RunCurrentState()
    {
        int noBludgerIsClose = -1;
        State returnState = this; //just for the testing
        //Debug.Log("Bludger is close! might hit it!");

        bludgerCloseIndex = Logic.IsABludgerInRange(bludgerTryHittingRange);

        if (bludgerCloseIndex != noBludgerIsClose) 
        {
            //TO DO Animation activation
            Debug.Log("hit bludger!!");
            Logic.BudgerWasHit(bludgerCloseIndex);
            Logic.ResetTarget();//needed in order to change the chaser that the beater defends
            returnState = idle;
        }
        else
        {
             Logic.ResetTarget();//needed in order to change the chaser that the beater defends
             returnState = defendChaser;
        }

        return returnState;
    }

}