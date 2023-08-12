using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DefendChaserState : State
{
    int bludgerCloseIndex;
    float bludgerClosenessRange = 5f;
    public HitBludgerState hitBludgerState;


    public override State RunCurrentState()
    {
        int noBludgerIsClose = -1;//3 means no bludger is close
        State returnState = this; //just for the testing
        //Debug.Log("I am defending the chaser");

        if(Logic.target == null)
            ChooseChaserToDefend();

        bludgerCloseIndex = Logic.IsABludgerInRange(bludgerClosenessRange); //which bludger is close

        if (bludgerCloseIndex != noBludgerIsClose)//one of the bludger is close
        {
            Logic.ResetTarget();
            returnState = hitBludgerState;
        }
        else //-1 means no bludger is close
        {
            //TO DO make beater fly randomly around chaser it defends
            Logic.MoveAndRotateToTarget(); 
            returnState = this;
        }

        return returnState;
    }
    private void ChooseChaserToDefend()
    {
        //TODO use the real code

        //THIS IS THE REAL CODE
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //List<PlayerLogicManager> chaserPlayers = new List<PlayerLogicManager>();
        //foreach (PlayerLogicManager player in Logic.friends)
        //{
        //    if (player.PlayerType == PlayerType.Chaser)
        //        chaserPlayers.Add(player);
        //}
        //// Creates a new instance of Random class
        //System.Random random = new System.Random();

        //// Generates a random index within the bounds of the array
        //int randomIndex = random.Next(0, chaserPlayers.Count - 1);

        //// Set relative position vector randomly 
        //Vector3 relativePosition = CreateRelativePosition();

        //// Get the randomly chosen chaser +relative position as target 
        //Targetable.SetRelativeTarget(relativePosition, chaserPlayers[randomIndex], Logic.relativePositionTarget);

        ////Set target
        //Logic.target = Logic.relativePositionTarget;
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////



        //CODE FOR TESTING BEFORE CHASER IS READY
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        List<PlayerLogicManager> seekerPlayers = new List<PlayerLogicManager>();
        foreach (PlayerLogicManager player in Logic.friends)
        {
            if (player.PlayerType == PlayerType.Seeker)
                seekerPlayers.Add(player);
        }
        // Creates a new instance of Random class
        System.Random random = new System.Random();

        // Generates a random index within the bounds of the array
        int randomIndex = random.Next(0, seekerPlayers.Count - 1);

        Vector3 relativePosition = CreateRelativePosition();

        // Get the randomly chosen chaser +relative position as target 
        Targetable.SetRelativeTarget(relativePosition, seekerPlayers[randomIndex], Logic.relativePositionTarget);

        Logic.target = Logic.relativePositionTarget;
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

        if(value == negative)
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

}
