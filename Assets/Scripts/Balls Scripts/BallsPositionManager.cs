using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallsPositionManager : MonoBehaviour
{
    [SerializeField] private GameObject quaffleStartingPosition;
    [SerializeField] private GameObject snitchStartingPosition;
    [SerializeField] private GameObject bludger1StartingPosition;
    [SerializeField] private GameObject bludger2StartingPosition;
    [SerializeField] private QuaffleLogic quaffle;
    [SerializeField] private SnitchLogic snitch;
    [SerializeField] private BludgerLogic bludger1;
    [SerializeField] private BludgerLogic bludger2;

    public void StartingGame()
    {
        quaffle.transform.position = quaffleStartingPosition.transform.position;
        snitch.transform.position = snitchStartingPosition.transform.position;
        bludger1.transform.position = bludger1StartingPosition.transform.position;
        bludger2.transform.position = bludger2StartingPosition.transform.position;
    }


    public void GoalWasScored()
    {
        quaffle.transform.position = quaffleStartingPosition.transform.position;
    }
    //public void SetPlayerStartingPos(PlayerLogicManager player, int typeIndex)
    //{
    //    if(player.PlayerTeam == team1)
    //    {
    //        switch(player.PlayerType)
    //        {
    //            case PlayerType.Beater:
    //                player.startingPosition = StartingPositionsBeatersTeam1[typeIndex].transform.position;
    //                break;
    //            case PlayerType.Chaser:
    //                player.startingPosition = StartingPositionsChasersTeam1[typeIndex].transform.position;
    //                break;
    //            case PlayerType.Seeker:
    //                player.startingPosition = StartingPositionsSeekerTeam1[typeIndex].transform.position;
    //                break;
    //            case PlayerType.Keeper:
    //                player.startingPosition = StartingPositionsKeeperTeam1[typeIndex].transform.position;
    //                break;
    //        }

    //        player.transform.position = player.startingPosition;
    //    }
    //    else if(player.PlayerTeam == team2)
    //    {
    //        switch(player.PlayerType)
    //        {
    //            case PlayerType.Beater:
    //                player.startingPosition = StartingPositionsBeatersTeam2[typeIndex].transform.position;
    //                break;
    //            case PlayerType.Chaser:
    //                player.startingPosition = StartingPositionsChasersTeam2[typeIndex].transform.position;
    //                break;
    //            case PlayerType.Seeker:
    //                player.startingPosition = StartingPositionsSeekerTeam2[typeIndex].transform.position;
    //                break;
    //            case PlayerType.Keeper:
    //                player.startingPosition = StartingPositionsKeeperTeam2[typeIndex].transform.position;
    //                break;
    //        }

    //        player.transform.position = player.startingPosition;
    //    }
    //}




}
