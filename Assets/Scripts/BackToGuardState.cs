using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackToGuardState : PlayerState
{
    public override PlayerState RunCurrentPlayerState()
    {
        PlayerState nextState = this;

        switch(this.playerType)
        {
            case PlayerType.Keeper:
                {
                    nextState = RunKeeperBackToGuard();
                }
                break;
            // Irrelevant!
            //case PlayerType.Beater:
            //    {
            //    }
            //    break;
            //case PlayerType.Chaser:
            //    {
            //    }
            //    break;
            //case PlayerType.Seeker:
            //    {
            //    }
            //    break;
            default:
                {
                    //nextState = this;
                }
                break;
        }

        return nextState;
    }


    private PlayerState RunKeeperBackToGuard()
    {
        PlayerState nextState = this;

        // Reset the player's velocity to stop any ongoing movement
        Rigidbody playerRigidbody = GetComponent<Rigidbody>();
        playerRigidbody.velocity = Vector3.zero;

        // Reset the player's angular velocity to stop any ongoing rotation
        playerRigidbody.angularVelocity = Vector3.zero;

        // Calculate the direction from the current position to the starting position
        Vector3 directionToStartingPosition = startingPosition - transform.position;

        // Check if the player has reached the starting position
        if(directionToStartingPosition.sqrMagnitude > 0.01f) // Adjust the threshold as needed
        {
            // Move towards the starting position
            Vector3 moveDirection = directionToStartingPosition.normalized;
            transform.position += moveDirection * speed * Time.deltaTime;

            // Rotate towards the starting position (back facing the starting position)
            Quaternion targetRotation = Quaternion.LookRotation(-directionToStartingPosition.normalized);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, RotationSpeed * Time.deltaTime);
        }
        else
        {
            // The player has reached the starting position and is facing the correct direction
            // You can decide if you want to transition to a different state or remain in the BackToGuardState

            // For example, transition back to the DefendState after reaching the starting position
            nextState = new IdleState();
        }

        return nextState;
    }

}
