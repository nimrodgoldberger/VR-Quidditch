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
    public bool holdsQuaffle = true;
    [SerializeField] private float coolDownAfterThrow = 1.0f;
    [SerializeField] private float timerAfterThrow = 0f;
    [SerializeField] private float avoidanceRadius; // Adjust this value based on game's needs
    [SerializeField] private float stoppingDistance; // Adjust this value based on game's needs
    public ChaserAdvanceWithOutQuaffleState chaserAdvanceWithOutQuaffleState;
    public ChaserGetQuaffleState chaserGetQuaffleState;
    public override State RunCurrentState()
    {
        State nextState = this;
        holdsQuaffle = Logic.Quaffle.IsQuaffleHeldByPlayer(Logic);  // TODO Check that works correctly

        if(holdsQuaffle)
        {
            chooseLoopToTarget();// get goal to set the target's position
            Vector3 targetPosition = Logic.target.transform.position; 
            Vector3 chaserPosition = transform.position;

            float distanceToTarget = Vector3.Distance(chaserPosition, targetPosition);

            if(distanceToTarget > stoppingDistance)
            {
                Vector3 desiredDirection = (targetPosition - chaserPosition).normalized;
                Vector3 avoidanceDirection = CalculateAvoidanceDirection(); // Implement avoidance logic

                Vector3 finalDirection = desiredDirection + avoidanceDirection;
                finalDirection.Normalize();

                // Assuming you have a reference to a Rigidbody component
                Rigidbody chaserRigidbody = GetComponent<Rigidbody>();
                chaserRigidbody.velocity = finalDirection * Logic.GetSpeed();
            }
            else // I am close enought to throw  **** TODO add pass probability too 
            {
                Logic.Quaffle.ThrowQuaffle(Logic.target); // Throw the quaffle to the target

                //Logic.ResetTarget(); // Reset the target when you're close enough

                holdsQuaffle = false;
                Logic.isMoving = false; // Stop moving
            }

            /*return this;*/ // Stay in the same state for now
        }
        else
        {
            // If the Keeper doesn't hold the Quaffle anymore, return to the starting position
            if(timerAfterThrow <= coolDownAfterThrow)
            {
                Debug.Log("In cooldown after I threw the quaffle in ChaserAdvanceWithQuaffleState");

                timerAfterThrow += Time.fixedDeltaTime;
            }
            else
            {
                Debug.Log("CoolDown ENDED now returning to ChaserGetQuaffleState");
                timerAfterThrow = 0f;
                Logic.target = null;
                Logic.isMoving = false;
                nextState = chaserGetQuaffleState;
            }
        }

        return nextState;
    }

    private Vector3 CalculateAvoidanceDirection()
    {
        Vector3 avoidanceDirection = Vector3.zero;
        foreach(PlayerLogicManager enemy in Logic.enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if(distanceToEnemy < avoidanceRadius)
            {
                // Calculate a direction to steer away from the enemy
                avoidanceDirection += (transform.position - enemy.transform.position).normalized;
            }
        }

        return avoidanceDirection.normalized;
    }

    private void chooseLoopToTarget()
    {
        Logic.SetTarget(Logic.ChooseTargetGoal());

    }

}

