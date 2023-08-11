using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldSnitchState : State
{
    [SerializeField] TeamPlayersManager teamManager;
    public override State RunCurrentState()
    {
        teamManager.GoalAnimations(Logic.PlayerTeam);
        Debug.Log("I HAVE CAUGHT THE SNITCH!!!!!!!!!!");
        //Activates winning and loosing animations
        return this; //TODO MAYBE VICTORY ANIMATION? + scene +closing menu
    }

}
