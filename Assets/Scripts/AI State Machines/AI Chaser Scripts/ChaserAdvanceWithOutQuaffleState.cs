using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ChaserAdvanceWithOutQuaffleState : State
{

    public IdleState Idle;

    [SerializeField] private ChaserGetQuaffleState chaserGetQuaffleState;

    private float minGoalDistance = 40f;


    public override State RunCurrentState()
    {
        State returnState = this;



        if(Logic.goalScored)
        {
            returnState = Idle;

        }


        // Check if the quaffle is still in your team
        if(Logic.Quaffle.IsQuaffleHeldByTeam(Logic.PlayerTeam) == false)
        {
            returnState = chaserGetQuaffleState;
            Logic.StopMoveAndRotateToTarget();
            Logic.target = null;
            Logic.isMoving = false;
        }
        else
        {
            Targetable chosenEnemyGoal = Logic.ChooseTargetGoal();

            // Calculate the target position as the enemy goal
            Vector3 targetPosition = chosenEnemyGoal.transform.position;

            // Calculate the direction to the chaser with the quaffle
            Vector3 QuaffleDirection = Logic.Quaffle.transform.position - transform.position;
            QuaffleDirection.y = 0f;
            QuaffleDirection.Normalize();

            // Calculate the position to keep a straight path between you and the chaser with the quaffle
            Vector3 straightPathPosition = Logic.Quaffle.transform.position - QuaffleDirection * minGoalDistance;

            // Calculate the final target position as a weighted average of the enemy goal and the straight path position
            float straightPathWeight = 0.5f;
            Vector3 finalTargetPosition = targetPosition * (1 - straightPathWeight) + straightPathPosition * straightPathWeight;

            // Create a raycast from the chaser to the straight path position
            RaycastHit hit;
            if(Physics.Raycast(transform.position, straightPathPosition - transform.position, out hit, minGoalDistance))
            {
                // Check if the raycast hit an enemy
                PlayerLogicManager enemy = hit.transform.GetComponent<PlayerLogicManager>();
                if(enemy != null && Logic.enemies.Contains(enemy))
                {
                    // If the raycast hit an enemy, adjust the straight path position to avoid the enemy
                    straightPathPosition = hit.point + hit.normal * 2f;
                    finalTargetPosition = targetPosition * (1 - straightPathWeight) + straightPathPosition * straightPathWeight;
                }
            }

            Logic.relativePositionTarget.transform.position = finalTargetPosition;
            // Set the target to the final target position
            Logic.target = Logic.relativePositionTarget;

            Logic.MoveAndRotateToTarget();
            StartCoroutine("MoveAndRotateToTarget");
        }


        return returnState;
    }
}
