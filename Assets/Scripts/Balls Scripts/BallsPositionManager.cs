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
}