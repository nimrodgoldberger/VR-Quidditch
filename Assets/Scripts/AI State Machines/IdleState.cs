using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : State
{
    bool canSeeTarget;
    float timeToStayInIdleState = 0f;
    float SnitchVisibilityRange = 20f;
    float QuaffleVisibilityRange = 30f;
    public SeekSnitchState seekSnitchState;
    public DefendState defendState;

    public override State RunCurrentState()
    {
        State returnState = this; //just for the testing
        //State returnState = null;

        switch(Logic.PlayerType)
        {
            case PlayerType.Keeper:
                {
                    canSeeTarget = Logic.IsQuaffleInRange(QuaffleVisibilityRange);
                    if(canSeeTarget)
                    {
                        Debug.Log("I Have started DefenseState seeking the Quaffle!");
                        returnState = defendState;
                    }
                    else
                    {
                        returnState = this;
                    }
                }
                break;
            case PlayerType.Beater:
                {

                }
                break;
            case PlayerType.Chaser:
                {

                }
                break;
            case PlayerType.Seeker:
                {
                    canSeeTarget = Logic.IsSnitchInRange(SnitchVisibilityRange);
                    if(canSeeTarget)
                    {
                        Debug.Log("I Have started seeking the snitch!");
                        returnState = seekSnitchState;
                    }
                    else
                    {
                        returnState = this;
                    }
                }
                break;
            case PlayerType.VRPlayer:
                {

                }
                break;
            default:
                {
                    
                }
                break;
        }

        return returnState;
    }



}
