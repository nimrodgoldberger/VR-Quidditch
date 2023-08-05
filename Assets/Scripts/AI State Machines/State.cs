using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Payer Type Changes the behaviour and return value(nextState) of a specific RunCurrentPlayerState()
// depending on the type of the player.
// For example Keeper goes from idle to Defend when the quaffle is close, Beater doesn't care,
// Chaser acts differently if the holder is ours or not, Seeker doesn't care.
public enum PlayerType
{
    Keeper = 0,
    Beater = 1,
    Chaser = 2,
    Seeker = 3,
    VRPlayer = 4
}


public abstract class State : MonoBehaviour
{
    public PlayerLogicManager Logic;
    public abstract State RunCurrentState();
}
