using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DefendChaserState : State
{
    private int bludgerCloseIndex;
    private float bludgerClosenessRange = 15f;
    public HitBludgerState hitBludgerState;
    private PlayerLogicManager currentTarget;//this is in order to check if it is moving and if not just wait and not go crazy on the broom

    public override State RunCurrentState()
    {
        int noBludgerIsClose = -1;//3 means no bludger is close
        State returnState = this; //just for the testing

        if(Logic.target == null)
        {
            ChooseChaserToDefend();
        }

        bludgerCloseIndex = Logic.IsABludgerInRange(bludgerClosenessRange); //which bludger is close

        if (bludgerCloseIndex != noBludgerIsClose)//one of the bludger is close
        {
            returnState = hitBludgerState;
        }
        else //-1 means no bludger is close
        {
            if (currentTarget.isMoving)
            {
                Debug.Log("Protecting chaser");
                Logic.MoveAndRotateToTarget();
            }
                
            else
            {
                //StartCoroutine(Logic.MoveAndRotateToBludger(ChooseRandomBludger(), Logic.CreateRelativePositionToBewareOfBludgers()));
                Logic.isMoving = false;
            }

            returnState = this;
        }

        return returnState;
    }
    private void ChooseChaserToDefend()
    {
        //TODO use the real code

        Vector3 relativePosition;

        //THIS IS THE REAL CODE
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        List<PlayerLogicManager> chaserPlayers = new List<PlayerLogicManager>();
        foreach (PlayerLogicManager player in Logic.friends)
        {
            if (player.PlayerType == PlayerType.Chaser)
                chaserPlayers.Add(player);
        }
        // Creates a new instance of Random class
        System.Random random = new System.Random();

        // Generates a random index within the bounds of the array
        int randomIndex = random.Next(0, chaserPlayers.Count - 1);
        
        //do
        //{
            // Set relative position vector randomly 
        relativePosition = CreateRelativePosition();
            // Get the randomly chosen chaser +relative position as target 
        Targetable.SetRelativeTarget(relativePosition, chaserPlayers[randomIndex], Logic.relativePositionTarget);

        //} while (!TargetsSpawnArea.IsInsidePlayableArea(Logic.relativePositionTarget.transform.position));
        

        
        //Set target
        Logic.target = Logic.relativePositionTarget;
        currentTarget = chaserPlayers[randomIndex];
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////



        //CODE FOR TESTING BEFORE CHASER IS READY
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //List<PlayerLogicManager> seekerPlayers = new List<PlayerLogicManager>();
        //foreach (PlayerLogicManager player in Logic.friends)
        //{
        //    if (player.PlayerType == PlayerType.Seeker)
        //        seekerPlayers.Add(player);
        //}
        //// Creates a new instance of Random class
        //System.Random random = new System.Random();

        //// Generates a random index within the bounds of the array
        //int randomIndex = random.Next(0, seekerPlayers.Count - 1);

        ////// Set relative position vector randomly
        //Vector3 relativePosition = CreateRelativePosition();

        //// Get the randomly chosen chaser +relative position as target 
        //Targetable.SetRelativeTarget(relativePosition, seekerPlayers[randomIndex], Logic.relativePositionTarget);

        //////Set target
        //Logic.target = Logic.relativePositionTarget;
        //currentTarget = seekerPlayers[randomIndex];
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    }

    private Vector3 CreateRelativePosition()
    {
        int negative = 0;
        int positive = 1;

        Vector3 relativePosition = Vector3.one;

        // Creates a new instance of Random class
        System.Random random = new System.Random();

        // Generates a random index within the bounds of the array
        int value = random.Next(0, 1);

        if (value == negative)
        {
            relativePosition.x = random.Next(-5, -3);
            relativePosition.y = random.Next(-5, -3);
            relativePosition.z = random.Next(-5, -3);
        }

        if (value == positive)
        {
            relativePosition.x = random.Next(3, 5);
            relativePosition.y = random.Next(3, 5);
            relativePosition.z = random.Next(3, 5);
        }
        

        return relativePosition;
    }

    

    private int ChooseRandomBludger()
    {
        //Choose random bludger
        // Creates a new instance of Random class
        System.Random random = new System.Random();
        int randomIndex = random.Next(0, 1);
        return randomIndex;
    }
}
