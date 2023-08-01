using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State : MonoBehaviour
{
    public PlayerLogicManager Logic;
    public abstract State RunCurrentState();
}
