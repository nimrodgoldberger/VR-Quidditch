using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : MonoBehaviour
{
    public State currentState;

    // Update is called once every fixed framerate frame
    void FixedUpdate()
    {
        RunStateMachine();
    }
    private void RunStateMachine()
    {
        State nextState = currentState?.RunCurrentState();

        if(nextState)
        {
            SwitchToNextState(nextState);
        }
    }

    private void SwitchToNextState(State nextState)
    {
        currentState = nextState;
    }
}
