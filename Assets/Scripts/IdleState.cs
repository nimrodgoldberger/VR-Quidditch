using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : PlayerState
{
    private Vector3 startingPosition;

    void Start()
    {
        startingPosition = transform.position;
    }


    public override PlayerState RunCurrentPlayerState()
    {
        PlayerState playerState = null;

        switch(this.playerType)
        {
            case PlayerType.Keeper:
                {
                    playerState = RunKeeperIdle();
                }
                break;
            case PlayerType.Beater:
                {
                    playerState = RunBeaterIdle();
                }
                break;
            case PlayerType.Chaser:
                {
                    playerState = RunChaserIdle();
                }
                break;
            case PlayerType.Seeker:
                {
                    playerState = RunSeekerIdle();
                }
                break;
            default:
                {
                    playerState = this;
                }
                break;
        }

        return playerState;
    }

    private PlayerState RunKeeperIdle()
    {
        PlayerState nextState = this;
        //Implementation___________________________________________________________
        bool foundEnemy = searchForCloseOponents();

        if(foundEnemy)
        {
            nextState = new DefendState();
        }
        //_________________________________________________________________________
        return nextState;
    }

    private PlayerState RunBeaterIdle()
    {
        PlayerState nextState = this;
        //Implementation...
        return nextState;
    }

    private PlayerState RunChaserIdle()
    {
        PlayerState nextState = this;
        //Implementation...
        return nextState;
    }

    private PlayerState RunSeekerIdle()
    {
        PlayerState nextState = this;
        //Implementation...
        return nextState;
    }




    //private void initializeMovement()
    //{

    //    //if(!foundEnemy && transform.position != startingPosition)
    //    //{
    //    //    eState = eGuardianState.GoBack;
    //    //}

    //    //if(eState != eGuardianState.Idle)
    //    //{
    //    //    tackleTarget();
    //    //}
    //}

    private bool searchForCloseOponents()
    {
        bool found = false;
        
        foreach(GameObject targetToDefend in targetsToDefend)
        {
            foreach(GameObject oponent in oponents)
            {
                if(IsOponentClose(targetToDefend, oponent))
                {
                    //eState = eGuardianState.Defend;
                    this.target = oponent;
                    found = true;
                    break;
                }
            }

            if(found)
            {
                break;
            }
        }

        return found;
    }

    private bool IsOponentClose(GameObject targetToDefend, GameObject oponent)
    {
        bool isOponentClose = false;
        
        if(Vector3.Distance(oponent.transform.position, targetToDefend.transform.position) < detectionRadius)
        {
            isOponentClose = true;
        }

        return isOponentClose;
    }
}
