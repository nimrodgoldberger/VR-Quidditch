using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeeperHoldsQuaffleState : State
{
    public ReturnToStartPositionState ReturnToStartPositionState;
    public bool holdsQuaffle = true;
    public PlayerLogicManager playerToPassTo;
    public override State RunCurrentState()
    {
       

        if( holdsQuaffle )
        {

            return this;
        }
        else
        {
            return ReturnToStartPositionState;
        }
    }


    public void FindFriendToPassTo()
    {
        // TODO Here I need to go through the Logic.friends array and find the ones that are close and easier to pass to
        // (with less enemies between us and the friend)  
    }

    public void MoveToPassBallInStraightLine()
    {
        // TODO Here I need to move to pass the ball in a straight line without interferrences
        
    }

    public void PassTheQuaffle()
    {
        Logic.Quaffle.FlyToTarget(playerToPassTo);
    }

    private PlayerLogicManager GetPlayerToPassTo()
    {
        return playerToPassTo;
    }
}
