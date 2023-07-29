using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeekSnitchState : PlayerState
{
    public override PlayerState RunCurrentPlayerState()
    {
        PlayerState playerState = null;

        switch(this.playerType)
        {
            case PlayerType.Seeker:
                {
                    playerState = RunSeekerSeekSnitch();
                }
                break;
            default:
                {
                    playerState = new IdleState();
                }
                break;
        }

        return playerState;
    }

    private PlayerState RunSeekerSeekSnitch()
    {
        // Get the position of the snitch
        Vector3 snitchPosition = target.transform.position;

        // Calculate the direction to the snitch
        Vector3 directionToSnitch = snitchPosition - this.transform.position;

        // Normalize the direction to get a unit vector
        directionToSnitch.Normalize();

        // Calculate the new position of the player
        Vector3 newPosition = this.transform.position + directionToSnitch * Time.deltaTime * this.speed;

        // Move the player to the new position
        this.transform.position = newPosition;

        // Calculate the new height of the player
        float newHeight = Mathf.Sin(Time.time * this.frequency) * this.amplitude + this.startingHeight;

        // Set the new height of the player
        this.transform.position = new Vector3(newPosition.x, newHeight, newPosition.z);

        // Calculate the new rotation of the player
        Quaternion newRotation = Quaternion.LookRotation(directionToSnitch);

        // Set the new rotation of the player
        this.transform.rotation = Quaternion.Slerp(this.transform.rotation, newRotation, Time.deltaTime * this.RotationSpeed);

        // Return the current state
        return this;
    }
}
