using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeeperHoldsQuaffleState : State
{
    public ReturnToStartPositionState ReturnToStartPositionState;
    public bool holdsQuaffle = true;
    public Targetable playerToPassTo;
    [SerializeField] private float coolDownAfterPass = 1.0f;
    [SerializeField] private float timerAfterPass = 0f;

    public override State RunCurrentState()
    {
        State nextState = this;

        // TODO check if works 
        //holdsQuaffle = Logic.Quaffle.IsQuaffleHeldByPlayer(Logic);
        //holdsQuaffle = Logic.IsQuaffleHeldByMe();

        if(holdsQuaffle)
        {

            //if(Logic.target == null)
            //{
            //    Logic.SetTarget(playerToPassTo);
            //}

            // TODO complete the method FindFriendToPassTo()
            // for testing i set it hard coded
            Debug.Log("I Hold the quaffle in KeeperHoldsQuaffleState");

            holdsQuaffle = false;
            PassTheQuaffle();

            // TODO Check Why? row below
            //Logic.isMoving = false;


        }
        else
        {
            // If the Keeper doesn't hold the Quaffle anymore, return to the starting position
            if(timerAfterPass <= coolDownAfterPass)
            {
                //Debug.Log("In cooldown after I threw the quaffle in KeeperHoldsQuaffleState");

                timerAfterPass += Time.fixedDeltaTime;
            }
            else
            {
                Debug.Log("CoolDown ENDED now returning to Starting position in KeeperHoldsQuaffleState");
                timerAfterPass = 0f;
                Logic.target = null;
                Logic.isMoving = false;
                nextState = ReturnToStartPositionState;
            }
        }

        // If the Keeper still holds the Quaffle, stay in this state
        // else wait 1 second and then return to startingPositionState
        return nextState;
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
        Logic.Quaffle.ThrowQuaffle(Logic, playerToPassTo);

    }

    public void PassTheQuaffle()
    {
        //Logic.Quaffle.transform.parent = null;
        Logic.Quaffle.ThrowQuaffle(Logic, playerToPassTo);
    }

    private Targetable GetPlayerToPassTo()
    {
        return playerToPassTo;
    }


    private void ChooseChaserToPassTo()
    {
        List<PlayerLogicManager> chaserPlayers = new List<PlayerLogicManager>();
        foreach(PlayerLogicManager player in Logic.friends)
        {
            if(player.PlayerType == PlayerType.Chaser)
                chaserPlayers.Add(player);
        }
        // Creates a new instance of Random class
        System.Random random = new System.Random();

        // Generates a random index within the bounds of the array
        int randomIndex = random.Next(0, chaserPlayers.Count - 1);

        // TODO make it random for various chasers
        //Set target 
        //Logic.target = chaserPlayers[randomIndex];
        playerToPassTo = chaserPlayers[0];
    }
}
