using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//public class ChaserAdvanceWithQuaffleState : State
//{


//    public override State RunCurrentState()
//    {
//        return this;
//    }
//}

public class ChaserAdvanceWithQuaffleState : State
{
    public float stoppingDistance = 3f; // Adjust this value based on your game's needs

    public override State RunCurrentState()
    {
        Vector3 targetPosition = Logic.target.transform.position; // Replace with how you get the target's position
        Vector3 chaserPosition = transform.position;

        float distanceToTarget = Vector3.Distance(chaserPosition, targetPosition);

        if(distanceToTarget > stoppingDistance)
        {
            Vector3 desiredDirection = (targetPosition - chaserPosition).normalized;
            Vector3 avoidanceDirection = CalculateAvoidanceDirection(Logic); // Implement your avoidance logic

            Vector3 finalDirection = desiredDirection + avoidanceDirection;
            finalDirection.Normalize();

            // Assuming you have a reference to a Rigidbody component
            Rigidbody chaserRigidbody = GetComponent<Rigidbody>();
            chaserRigidbody.velocity = finalDirection /** Logic.speed*/;
        }
        else
        {
            // Stop or perform other behavior when close to the target
            Logic.ResetTarget(); // Reset the target when you're close enough
            Logic.isMoving = false; // Stop moving
        }

        return this; // Stay in the same state for now
    }

    private Vector3 CalculateAvoidanceDirection(PlayerLogicManager chaserLogic)
    {
        Vector3 avoidanceDirection = Vector3.zero;
        foreach(PlayerLogicManager enemy in chaserLogic.enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if(distanceToEnemy < 20 /*chaserLogic.avoidanceRadius*/)
            {
                // Calculate a direction to steer away from the enemy
                avoidanceDirection += (transform.position - enemy.transform.position).normalized;
            }
        }

        return avoidanceDirection.normalized;
    }
}

