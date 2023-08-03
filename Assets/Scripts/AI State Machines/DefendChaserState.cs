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
        int noBludgerIsClose = 3;//3 means no bludger is close
        State returnState = this; //just for the testing
        Debug.Log("I am defending the chaser");

        if(Logic.target == null)
            ChooseChaserToDefend();

        bludgerCloseIndex = Logic.IsABludgerInRange(bludgerClosenessRange);

        if (bludgerCloseIndex != noBludgerIsClose)//3 means no bludger is close
        {
            Logic.ResetTarget();
            returnState = hitBludgerState;
        }
        else
        {
            //TO DO make beater fly randomly around chaser it defends
            Logic.MoveAndRotateToTarget(); 
            returnState = this;
        }

        return returnState;
    }
    private void ChooseChaserToDefend()
    {
        List<PlayerLogicManager> chaserPlayers = new List<PlayerLogicManager>();
        foreach (PlayerLogicManager player in Logic.friends)
        {
            if (player.PlayerType == PlayerType.Chaser)
                chaserPlayers.Add(player);
        }
        // Creates a new instance of Random class
        System.Random random = new System.Random();

        // Generates a random index within the bounds of the array
        int randomIndex = random.Next(0, chaserPlayers.Count);

        //TO DO - set target on the fly according to transform of the chosen chaser.

        // Get the randomly chosen chaser as target
        //Logic.target = chaserPlayers[randomIndex];

        //TO DO make beater fly randomly around chaser it defends
        //Logic.target.transform.position = chaserPlayers[randomIndex].transform.position + Vector3(0.35f, 0.35f, 0.2f);
    }

}
