using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldSnitchState : State
{
    public override State RunCurrentState()
    {
        Debug.Log("I HAVE CAUGHT THE SNITCH!!!!!!!!!!");
        return this; //MAYBE VICTORY ANIMATION?
    }

}
