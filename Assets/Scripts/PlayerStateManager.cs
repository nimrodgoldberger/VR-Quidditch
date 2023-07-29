using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateManager : MonoBehaviour
{
    public PlayerState currentState;
    private Vector3 startingPosition;

    void Start()
    {
        startingPosition = transform.position;
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
        nextState.speed= currentState.speed;
        currentState = nextState;
    }
}
