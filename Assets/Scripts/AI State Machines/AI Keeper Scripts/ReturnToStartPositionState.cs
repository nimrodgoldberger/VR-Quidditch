using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnToStartPositionState : State
{
    public IdleState idleState;
    public DefendState defendState;

    [SerializeField] private float minDistance = 2f;
    public Targetable startingPos;
    float QuaffleVisibilityRange = 30f;

    public override State RunCurrentState()
    {
        if(!startingPos)
        {
            startingPos = Logic.GetStartingTransformAsTargetable();
            Logic.SetTarget(startingPos);
        }

        float distanceToPosition = Vector3.Distance(transform.position, startingPos.transform.position);

        if(distanceToPosition <= minDistance)
        {
            // Snap the character's position to the startingPos position
            transform.position = startingPos.transform.position;
            // Use RotateToStartingPosition method to face the same direction as startingPos
            if(!Logic.isRotatingToStartingPos)
            {
                Logic.RotateToStartingPosition();
                Logic.isRotatingToStartingPos = true;
            }

            if(Logic.goalScored)
            {
                Logic.target = null;
                Logic.isMoving = false;
                Logic.StopMoveAndRotateToTarget();

                return idleState;
            }

            // Check if the character is still rotating
            if(Logic.isMoving || Logic.isRotatingToStartingPos)
            {
                return this; // Keep the state until the rotation finishes
            }
            else
            {
                Debug.Log("I have reached my starting position and rotation");
                Logic.target = null;
                Logic.isMoving = false;
                Logic.isRotatingToStartingPos = false;

                return idleState;
            }
        }
        else
        {
            // Move and rotate towards the startingPos
            Logic.MoveAndRotateToTarget();
            Logic.isRotatingToStartingPos = false; // Reset the flag if still moving towards the position

            return this;
        }
    }
}