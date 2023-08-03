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

    //public override State RunCurrentState()
    //{
    //    if(!startingPos)
    //    {
    //        startingPos = Logic.GetStartingTransformAsTargetable();
    //        Logic.SetTarget(startingPos);
    //    }

    //    // Check if the Quaffle is in range
    //    if(Logic.IsQuaffleInRange(QuaffleVisibilityRange))
    //    {
    //        Debug.Log("Quaffle in range. Defend!");
    //        return defendState;
    //    }

    //    // Calculate the distance to the startingPos position and rotation
    //    float distanceToPosition = Vector3.Distance(transform.position, startingPos.transform.position);
    //    float angleToRotation = Quaternion.Angle(transform.rotation, startingPos.transform.rotation);

    //    // Check if character has reached both the startingPos position and rotation
    //    if(distanceToPosition <= minDistance && angleToRotation <= 0)
    //    {
    //        Debug.Log("I have reached my starting position and rotation");
    //        return idleState;
    //    }
    //    else
    //    {
    //        // Move and rotate towards the startingPos
    //        Logic.MoveAndRotateToTarget();
    //        return this;
    //    }
    //}

    public override State RunCurrentState()
    {
        if(!startingPos)
        {
            //startingPos = Logic.startingPositionTarget;
            startingPos = Logic.GetStartingTransformAsTargetable();
            Logic.SetTarget(startingPos);
        }

        // Calculate the distance to the startingPos position and rotation
        float distanceToPosition = Vector3.Distance(transform.position, startingPos.transform.position);
        float angleToRotation = Quaternion.Angle(transform.rotation, startingPos.transform.rotation);

        // Check if character has reached both the startingPos position and rotation
        if(distanceToPosition <= minDistance)
        {

            if(angleToRotation == 0)
            {
                Debug.Log("I have reached my starting position and rotation");
                Logic.target = null;
                Logic.isMoving = false;

                return idleState;
            }
            else
            {
                Logic.RotateToStartingPosition();
                return this;
            }


        }
        else
        {
            // Move and rotate towards the startingPos
            Logic.MoveAndRotateToTarget();
            return this;
        }
    }


    //bool wasSnitchCaught;
    //public override State RunCurrentState()
    //{
    //    if(!startingPos)
    //    {
    //        startingPos = Logic.GetStartingTransformAsTargetable();
    //        Logic.SetTarget(startingPos);
    //    }

    //    transform.position = startingPos.transform.position;
    //    transform.rotation = startingPos.transform.rotation;

    //    Debug.Log("I will return to my starting position");
    //    return idleState;

    //    //if(Vector3.Distance(transform.position, startingPos.transform.position) <= minDistance)
    //    //{
    //    //    transform.position = startingPos.transform.position;
    //    //    transform.rotation = startingPos.transform.rotation;

    //    //    Debug.Log("I will return to my starting position");
    //    //    return idleState;
    //    //}
    //    //else
    //    //{
    //    //    Logic.MoveAndRotateToTarget();

    //    //    return this;
    //    //}
    //}
}
