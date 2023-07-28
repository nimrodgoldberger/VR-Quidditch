using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefendState : PlayerState
{
    public override PlayerState RunCurrentPlayerState()
    {
        PlayerState playerState = null;

        switch(this.playerType)
        {
            case PlayerType.Keeper:
                {
                    playerState = RunKeeperDefend();
                }
                break;
            case PlayerType.Beater:
                {
                    playerState = RunBeaterDefend();
                }
                break;
            case PlayerType.Chaser:
                {
                    playerState = RunChaserDefend();
                }
                break;
            // Irrelevant!
            //case PlayerType.Seeker:
            //    {  
            //    }
            //    break;
            default:
                {
                    playerState = this;
                }
                break;
        }

        return playerState;
    }

    private PlayerState RunKeeperDefend()
    {
        PlayerState nextState = this;
        bool isOponentClose = false;
        //Implementation_________________________________________________________________________
        foreach(GameObject targetToDefend in targetsToDefend)
        {
            if(IsOponentClose(target, targetToDefend))
            {
                isOponentClose = true;
                break;
            }
        }

        if(StateCoroutine != null)
        {
            StopCoroutine(StateCoroutine);
        }
        
        if(isOponentClose)
        {
            StateCoroutine = StartCoroutine(MoveAndRotateToTarget());
        }
        else
        {
            nextState= new BackToGuardState();
        }

        //_______________________________________________________________________________________
        return nextState;
    }

    private PlayerState RunBeaterDefend()
    {
        PlayerState nextState = new IdleState();
        //Implementation...
        return nextState;
    }

    private PlayerState RunChaserDefend()
    {
        PlayerState nextState = new IdleState();
        //Implementation...
        return nextState;
    }


    private IEnumerator MoveAndRotateToTarget()
    {
        float elapsedTime = 0f;
        Vector3 initialPosition = transform.position;
        Vector3 targetPosition = target.transform.position;
        Quaternion initialRotation = transform.rotation;
        Quaternion lookRotation = Quaternion.LookRotation(target.transform.position - transform.position);

        while(elapsedTime < 1f)
        {
            // Calculate the new position and rotation using Mathf.Lerp
            transform.position = Vector3.Lerp(initialPosition, targetPosition, elapsedTime);
            transform.rotation = Quaternion.Slerp(initialRotation, lookRotation, elapsedTime);

            // Update the elapsed time based on the speed and rotationSpeed average increase
            // Maybe needs adjustments
            elapsedTime += ((Time.deltaTime * speed) + (Time.deltaTime * RotationSpeed)) / 2;

            yield return null; // Wait for the next frame
        }
    }

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

