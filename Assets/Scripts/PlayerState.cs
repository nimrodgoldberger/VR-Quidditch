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
    Seeker = 3
}

// Abstract PlayerState from which all states will inherit
public abstract class PlayerState : MonoBehaviour
{
    //For Resetting/Init
    public Vector3 startingPosition;
    //For targeting 
    public GameObject[] targetsToDefend;
    public GameObject[] oponents;
    public float detectionRadius;
    public GameObject target;
    //For movement
    public float speed;
    //For rotation
    [SerializeField] public float RotationSpeed;
    protected Coroutine StateCoroutine;
    public PlayerLogic playerLogic;
    //For wave-like flight
    public float frequency = 2.0f;
    public float amplitude = 1.0f;
    public float startingHeight = 1.0f;// TODO Check if necessary

    //To decide which action to perform in each state
    public PlayerType playerType;

    // Start is called before the first frame update
    public abstract PlayerState RunCurrentPlayerState();


}
