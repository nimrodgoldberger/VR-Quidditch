using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : State
{
    bool canSeeTarget;
    float timeToStayInIdleState = 0f;
    [SerializeField] private float SnitchVisibilityRange = 50f;
    [SerializeField] private float QuaffleVisibilityRange = 30f;
    public SeekSnitchState seekSnitchState;
    public DefendState defendState;
    public DefendChaserState defendChaserState;
    public CountdownManager isStart;

    public override State RunCurrentState()
    {
        State returnState = this; //just for the testing
        //State returnState = null;

        if (isStart.TimeRemaining == 0)
        {
            switch (Logic.PlayerType)
            {
                case PlayerType.Keeper:
                    {
                        canSeeTarget = Logic.IsQuaffleInRange(QuaffleVisibilityRange);
                        if (canSeeTarget)
                        {
                            Debug.Log("I Have started DefenseState seeking the Quaffle!");
                            Logic.target = null;
                            Logic.isMoving = false;
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
                        returnState = defendChaserState;
                    }
                    break;
                case PlayerType.Chaser:
                    {

                    }
                    break;
                case PlayerType.Seeker:
                    {
                        canSeeTarget = Logic.IsSnitchInRange(SnitchVisibilityRange);
                        if (canSeeTarget)
                        {
                            Debug.Log("I Have started seeking the snitch!");
                            Logic.target = null;
                            Logic.isMoving = false;
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
        }

        return returnState;
    }

    private IEnumerator DelayedAction()
    {
        // Wait for 3 seconds
        yield return new WaitForSeconds(3f);

        // This code will execute after the delay
        Debug.Log("Delayed action executed.");
    }

}
