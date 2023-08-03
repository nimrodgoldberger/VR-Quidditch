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
            // If the Keeper still holds the Quaffle, stay in this state

            //return this;
            Logic.target = null;
            Logic.isMoving = false;
            return ReturnToStartPositionState;

        }
        else
        {
            // If the Keeper doesn't hold the Quaffle anymore, return to the starting position
            Logic.target = null;
            Logic.isMoving = false;
            return ReturnToStartPositionState;
        }
    }


    public void FindFriendToPassTo()
    {
        // TODO Here I need to go through the Logic.friends array and find the ones that are close and easier to pass to
        // (with less enemies between us and the friend)  

        // Find the friend to pass the Quaffle to.
        // Iterate through the Logic.friends array and select the one that is closest and has the least number of enemies between us and the friend.

        PlayerLogicManager closestFriend = null;
        int minEnemiesBetween = int.MaxValue;

        foreach(PlayerLogicManager friend in Logic.friends)
        {
            int enemiesBetween = CountEnemiesBetween(Logic, friend);

            // If the friend has fewer enemies between us and them, update the closest friend
            if(enemiesBetween < minEnemiesBetween)
            {
                minEnemiesBetween = enemiesBetween;
                closestFriend = friend;
            }
        }

        playerToPassTo = closestFriend;
    }

    private int CountEnemiesBetween(PlayerLogicManager source, PlayerLogicManager target)
    {
        // TODO Implement a method to count the number of enemies between the source and target.
        // You can use a raycast or other collision detection method to check for obstacles between the two players.

        // For now, let's assume there are no obstacles and return a random value for demonstration purposes.
        return Random.Range(0, 5);
    }

    public void MoveToPassBallInStraightLine()
    {
        // TODO Here I need to move to pass the ball in a straight line without interferrences

        // TODO Implement a method to move towards the target in a straight line without interference.
        // You can use a navigation system or custom movement logic to achieve this.

        // For now, let's assume we directly pass the ball to the target's position for demonstration purposes.
        Logic.Quaffle.FlyToTarget(playerToPassTo);

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
