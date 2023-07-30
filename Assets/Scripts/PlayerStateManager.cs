using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateManager : MonoBehaviour
{
    //Initialized with TeamPlayersManager too
    public PlayerTeam team;
    public PlayerType playerType;
    public PlayerState currentState = new IdleState();
    // TODO Check if better to initialize like those above
    private Vector3 startingPosition;
    //Unsure about these..They are here to make flying realistic
    // TODO Check if They do it
    //public float frequency = 2.0f;
    //public float amplitude = 1.0f;
    //public float startingHeight = 1.0f;

    void Start()
    {
        startingPosition = transform.position;
        //currentState = new IdleState();

    }

    void FixedUpdate()
    {
        RunStateMachine();
    }

    private void RunStateMachine()
    {
        PlayerState nextState = currentState?.RunCurrentPlayerState();

        if(nextState)
        {
            switchToNextState(nextState);
        }
    }

    private void switchToNextState(PlayerState nextState)
    {
        nextState.startingPosition = startingPosition;
        nextState.transform.position = currentState.transform.position;
        nextState.target = currentState.target;
        nextState.targetsToDefend = currentState.targetsToDefend;
        nextState.detectionRadius = currentState.detectionRadius;
        nextState.oponents = currentState.oponents;
        nextState.speed = currentState.speed;
        // TODO CHECK IF NEEDED
        //nextState.frequency = frequency;
        //nextState.amplitude = amplitude;
        //nextState.startingHeight = startingHeight;
        nextState.playerLogic.playerTeam = team;
        nextState.playerType = playerType;
        currentState = nextState;
    }
}
