//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

////public class ChaserAdvanceWithQuaffleState : State
////{


////    public override State RunCurrentState()
////    {
////        return this;
////    }
////}

//public class ChaserAdvanceWithQuaffleState : State
//{
//    public bool holdsQuaffle = true;
//    [SerializeField] private float coolDownAfterThrow = 1.0f;
//    [SerializeField] private float timerAfterThrow = 0f;
//    [SerializeField] private float avoidanceRadius; // Adjust this value based on game's needs
//    [SerializeField] private float stoppingDistance; // Adjust this value based on game's needs
//    public ChaserAdvanceWithOutQuaffleState chaserAdvanceWithOutQuaffleState;
//    public ChaserGetQuaffleState chaserGetQuaffleState;
//    public override State RunCurrentState()
//    {
//        State nextState = this;
//        holdsQuaffle = Logic.Quaffle.IsQuaffleHeldByPlayer(Logic);  // TODO Check that works correctly

//        if(holdsQuaffle)
//        {
//            chooseLoopToTarget(); // Choose goal to set the target's position
//            Vector3 targetPosition = Logic.target.transform.position;
//            Vector3 chaserPosition = transform.position;

//            float distanceToTarget = Vector3.Distance(chaserPosition, targetPosition);

//            if(distanceToTarget > stoppingDistance)
//            {
//                Vector3 desiredDirection = (targetPosition - chaserPosition).normalized;
//                Vector3 avoidanceDirection = CalculateAvoidanceDirection(); // Implement avoidance logic

//                Vector3 finalDirection = (desiredDirection + avoidanceDirection).normalized;

//                // Assuming you have a reference to a Rigidbody component
//                Rigidbody chaserRigidbody = GetComponent<Rigidbody>();
//                chaserRigidbody.velocity = finalDirection * Logic.GetSpeed();
//            }
//            else // I am close enough to throw  **** TODO add pass probability too
//            {
//                Logic.Quaffle.ThrowQuaffle(Logic.target); // Throw the quaffle to the target

//                holdsQuaffle = false;
//                Logic.isMoving = false; // Stop moving
//            }
//        }


//        //if(holdsQuaffle)
//        //{
//        //    chooseLoopToTarget();// get goal to set the target's position
//        //    Vector3 targetPosition = Logic.target.transform.position; 
//        //    Vector3 chaserPosition = transform.position;

//        //    float distanceToTarget = Vector3.Distance(chaserPosition, targetPosition);

//        //    if(distanceToTarget > stoppingDistance)
//        //    {
//        //        Vector3 desiredDirection = (targetPosition - chaserPosition).normalized;
//        //        Vector3 avoidanceDirection = CalculateAvoidanceDirection(); // Implement avoidance logic

//        //        Vector3 finalDirection = desiredDirection + avoidanceDirection;
//        //        finalDirection.Normalize();

//        //        // Assuming you have a reference to a Rigidbody component
//        //        Rigidbody chaserRigidbody = GetComponent<Rigidbody>();
//        //        chaserRigidbody.velocity = finalDirection * Logic.GetSpeed();
//        //    }
//        //    else // I am close enought to throw  **** TODO add pass probability too 
//        //    {
//        //        Logic.Quaffle.ThrowQuaffle(Logic.target); // Throw the quaffle to the target

//        //        //Logic.ResetTarget(); // Reset the target when you're close enough

//        //        holdsQuaffle = false;
//        //        Logic.isMoving = false; // Stop moving
//        //    }

//        //    /*return this;*/ // Stay in the same state for now
//        //}
//        //else
//        //{
//        //    // If the Keeper doesn't hold the Quaffle anymore, return to the starting position
//        //    if(timerAfterThrow <= coolDownAfterThrow)
//        //    {
//        //        Debug.Log("In cooldown after I threw the quaffle in ChaserAdvanceWithQuaffleState");

//        //        timerAfterThrow += Time.fixedDeltaTime;
//        //    }
//        //    else
//        //    {
//        //        Debug.Log("CoolDown ENDED now returning to ChaserGetQuaffleState");
//        //        timerAfterThrow = 0f;
//        //        Logic.target = null;
//        //        Logic.isMoving = false;
//        //        nextState = chaserGetQuaffleState;
//        //    }
//        //}

//        return nextState;
//    }

//    private Vector3 CalculateAvoidanceDirection()
//    {
//        Vector3 avoidanceDirection = Vector3.zero;
//        foreach(PlayerLogicManager enemy in Logic.enemies)
//        {
//            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
//            if(distanceToEnemy < avoidanceRadius)
//            {
//                // Calculate a direction to steer away from the enemy
//                avoidanceDirection += (transform.position - enemy.transform.position).normalized;
//            }
//        }

//        return avoidanceDirection.normalized;
//    }

//    private void chooseLoopToTarget()
//    {
//        Logic.SetTarget(Logic.ChooseTargetGoal());

//    }

//}




using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaserAdvanceWithQuaffleState : State
{
    public bool holdsQuaffle = true;
    [SerializeField] private float coolDownAfterThrowOrLostQuaffle = 1.0f;
    [SerializeField] private float timerAfterThrowOrLostQuaffle = 0f;
    [SerializeField] private float avoidanceRadius = 15f;
    [SerializeField] private float desiredDistance = 17f;
    [SerializeField] private float stoppingDistance = 35f;
    public ChaserAdvanceWithOutQuaffleState chaserAdvanceWithOutQuaffleState;
    public ChaserGetQuaffleState chaserGetQuaffleState;
    public IdleState Idle;

    public override State RunCurrentState()
    {
        State nextState = this;
        holdsQuaffle = /*Logic.Quaffle.IsQuaffleHeldByPlayer(Logic) &&*/ Logic.IsQuaffleHeldByMe();

        if(holdsQuaffle)
        {
            chooseLoopToTarget();
            Vector3 targetPosition = Logic.target.transform.position;
            Vector3 chaserPosition = transform.position;

            Vector3 desiredDirection = (targetPosition - chaserPosition).normalized;
            Vector3 avoidanceDirection = CalculateAvoidanceDirection();
            Vector3 finalDirection = desiredDirection + avoidanceDirection;
            finalDirection.Normalize();

            float distanceToTarget = Vector3.Distance(chaserPosition, targetPosition);

            if(!IsTargetALoop()) //
            {
                distanceToTarget = stoppingDistance;
            }

            if(distanceToTarget <= stoppingDistance)
            {
                Logic.StopMoveAndRotateToTarget(); // TODO Check if works

                Logic.Quaffle.ThrowQuaffle(Logic, Logic.target);

                holdsQuaffle = false;
                Logic.isMoving = false;
            }
            else
            {
                // Calculate the position for Logic.relativePositionTarget
                Vector3 relativePosition = targetPosition + (finalDirection * desiredDistance);

                // Update the transform of Logic.relativePositionTarget
                Logic.relativePositionTarget.SetTransform(relativePosition, Quaternion.identity);

                // Set Logic.target to Logic.relativePositionTarget
                Logic.SetTarget(Logic.relativePositionTarget);

                // Move and rotate towards the relative position target
                Logic.MoveAndRotateToTarget();
            }
        }
        else
        {
            if(timerAfterThrowOrLostQuaffle <= coolDownAfterThrowOrLostQuaffle)
            {
                timerAfterThrowOrLostQuaffle += Time.fixedDeltaTime;
            }
            else
            {
                Logic.ResetSpeed();
                timerAfterThrowOrLostQuaffle = 0f;
                Logic.target = null;
                Logic.isMoving = false;
                nextState = chaserGetQuaffleState;
            }
        }


        if(Logic.goalScored)
        {
            nextState = Idle;
            Logic.target = null;
            Logic.isMoving = false;
            Logic.StopMoveAndRotateToTarget();

        }

        return nextState;
    }

    //public override State RunCurrentState()
    //{
    //    State nextState = this;
    //    holdsQuaffle = Logic.Quaffle.IsQuaffleHeldByPlayer(Logic);

    //    if(holdsQuaffle)
    //    {
    //        chooseLoopToTarget();
    //        Vector3 targetPosition = Logic.target.transform.position;
    //        Vector3 chaserPosition = transform.position;

    //        Vector3 desiredDirection = (targetPosition - chaserPosition).normalized;
    //        Vector3 avoidanceDirection = CalculateAvoidanceDirection(); // Calculate avoidance direction

    //        Vector3 finalDirection = desiredDirection + avoidanceDirection;
    //        finalDirection.Normalize();

    //        // Use MoveAndRotateToTarget() with the final direction
    //        Logic.SetTarget(Logic.ChooseTargetGoal());
    //        Logic.MoveAndRotateToTarget(finalDirection);

    //        float distanceToTarget = Vector3.Distance(chaserPosition, targetPosition);

    //        if(distanceToTarget <= stoppingDistance)
    //        {
    //            Logic.Quaffle.ThrowQuaffle(Logic.target); // Throw the quaffle to the target

    //            holdsQuaffle = false;
    //            Logic.isMoving = false;
    //        }
    //    }
    //    else
    //    {
    //        // If the Keeper doesn't hold the Quaffle anymore, return to the starting position
    //        if(timerAfterThrow <= coolDownAfterThrow)
    //        {
    //            timerAfterThrow += Time.fixedDeltaTime;
    //        }
    //        else
    //        {
    //            timerAfterThrow = 0f;
    //            Logic.target = null;
    //            Logic.isMoving = false;
    //            nextState = chaserGetQuaffleState;
    //        }
    //    }

    //    return nextState;
    //}

    /*private Vector3 CalculateAvoidanceDirection()
{
    // Create a histogram of the enemy positions
    int histogramSize = 10;
    Vector3[,] histogram = new Vector3[histogramSize, histogramSize];
    float histogramCellSize = avoidanceRadius * 2 / histogramSize;
    foreach (PlayerLogicManager enemy in Logic.enemies)
    {
        Vector3 enemyPosition = enemy.transform.position;
        int x = Mathf.FloorToInt((enemyPosition.x - transform.position.x + avoidanceRadius) / histogramCellSize);
        int z = Mathf.FloorToInt((enemyPosition.z - transform.position.z + avoidanceRadius) / histogramCellSize);
        if (x >= 0 && x < histogramSize && z >= 0 && z < histogramSize)
        {
            histogram[x, z] += (transform.position - enemyPosition).normalized;
        }
    }

    // Calculate the avoidance direction from the histogram
    Vector3 avoidanceDirection = Vector3.zero;
    for (int x = 0; x < histogramSize; x++)
    {
        for (int z = 0; z < histogramSize; z++)
        {
            Vector3 histogramCellPosition = new Vector3(
                (x + 0.5f) * histogramCellSize - avoidanceRadius,
                0,
                (z + 0.5f) * histogramCellSize - avoidanceRadius
            );
            float distanceToCell = Vector3.Distance(transform.position, histogramCellPosition);
            if (distanceToCell < avoidanceRadius)
            {
                float weight = 1 - distanceToCell / avoidanceRadius;
                avoidanceDirection += histogram[x, z] * weight;
            }
        }
    }

    return avoidanceDirection.normalized;
}*/

    private Vector3 CalculateAvoidanceDirection()
    {
        float avoidanceRadius = 10f;
        int histogramSize = 5;
        float histogramCellSize = avoidanceRadius * 2 / histogramSize;

        // Calculate the histogram of enemy positions
        int[,] histogram = new int[histogramSize, histogramSize];
        List<PlayerLogicManager> enemies = new List<PlayerLogicManager>(Logic.enemies);
        enemies.AddRange(Logic.enemies);
        foreach(PlayerLogicManager enemy in enemies)
        {
            if(enemy.PlayerType == PlayerType.Chaser && enemy != Logic)
            {
                Vector3 enemyPosition = enemy.transform.position;
                Vector3 relativePosition = enemyPosition - transform.position;
                int x = Mathf.FloorToInt((relativePosition.x + avoidanceRadius) / histogramCellSize);
                int z = Mathf.FloorToInt((relativePosition.z + avoidanceRadius) / histogramCellSize);
                if(x >= 0 && x < histogramSize && z >= 0 && z < histogramSize)
                {
                    histogram[x, z]++;
                }
            }
        }

        // Calculate the avoidance direction from the histogram
        Vector3 avoidanceDirection = Vector3.zero;
        for(int x = 0; x < histogramSize; x++)
        {
            for(int z = 0; z < histogramSize; z++)
            {
                Vector3 histogramCellPosition = new Vector3(
                    (x + 0.5f) * histogramCellSize - avoidanceRadius,
                    0,
                    (z + 0.5f) * histogramCellSize - avoidanceRadius
                );
                float distanceToCell = Vector3.Distance(transform.position, histogramCellPosition);
                if(distanceToCell < avoidanceRadius)
                {
                    float weight = 1 - distanceToCell / avoidanceRadius;
                    avoidanceDirection += histogram[x, z] * weight * (transform.position - histogramCellPosition);
                }
            }
        }

        // Check if there are any enemies within the avoidance radius
        bool hasEnemiesWithinRadius = false;
        foreach(PlayerLogicManager enemy in enemies)
        {
            if(enemy.PlayerType == PlayerType.Chaser && enemy != Logic)
            {
                float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
                if(distanceToEnemy < avoidanceRadius)
                {
                    hasEnemiesWithinRadius = true;
                    break;
                }
            }
        }

        // If there are enemies within the avoidance radius, evade them
        if(hasEnemiesWithinRadius)
        {
            return avoidanceDirection.normalized;
        }

        // Define the interceptors variable
        List<PlayerLogicManager> interceptors = new List<PlayerLogicManager>();
        foreach(PlayerLogicManager enemy in Logic.enemies)
        {
            if(enemy.PlayerType == PlayerType.Keeper || enemy.PlayerType == PlayerType.Chaser || enemy.PlayerType == PlayerType.VRPlayer)
            {
                interceptors.Add(enemy);
            }
        }
        // Calculate the time it would take for the quaffle to reach the target position
        float distanceToTarget = Vector3.Distance(transform.position, Logic.target.transform.position);
        float timeToTarget = distanceToTarget / Logic.Quaffle.Speed();

        // Calculate the probability of each interceptor intercepting the quaffle
        float[] probabilities = new float[interceptors.Count];
        for(int i = 0; i < interceptors.Count; i++)
        {
            PlayerLogicManager interceptor = interceptors[i];
            float distanceToInterceptor = Vector3.Distance(transform.position, interceptor.transform.position);
            float timeToInterceptor = distanceToInterceptor / interceptor.GetSpeed();
            probabilities[i] = Mathf.Clamp01(timeToInterceptor / timeToTarget);
        }

        // Calculate the probability of all interceptors failing to intercept the quaffle
        float probabilityToEvade = 1;
        foreach(float probability in probabilities)
        {
            probabilityToEvade *= (1 - probability);
        }

        // If the probability of evading all interceptors is less than 50%, pass the ball to a safe teammate
        if(probabilityToEvade < 0.5f)
        {
            foreach(PlayerLogicManager teammate in Logic.friends)
            {
                if(teammate.PlayerType == PlayerType.Chaser && teammate != Logic)
                {
                    Vector3 passDirection = teammate.transform.position - transform.position;
                    float distanceToTeammate = passDirection.magnitude;
                    passDirection /= distanceToTeammate;
                    float timeToTeammate = distanceToTeammate / Logic.Quaffle.Speed();
                    float timeToIntercept = distanceToTeammate / interceptors[0].GetSpeed();
                    if(timeToTeammate < timeToIntercept)
                    {
                        Logic.target = teammate;
                        return Vector3.zero;
                    }
                }
            }
        }

        // If it's safe to evade the interceptors, evade them
        return avoidanceDirection.normalized;
    }

    /*private Vector3 CalculateAvoidanceDirection()
    {
        // Create a histogram of the enemy positions
        int histogramSize = 10;
        Vector3[,] histogram = new Vector3[histogramSize, histogramSize];
        float histogramCellSize = avoidanceRadius * 2 / histogramSize;
        List<PlayerLogicManager> interceptors = new List<PlayerLogicManager>();
        foreach (PlayerLogicManager enemy in Logic.enemies)
        {
            Vector3 enemyPosition = enemy.transform.position;
            int x = Mathf.FloorToInt((enemyPosition.x - transform.position.x + avoidanceRadius) / histogramCellSize);
            int z = Mathf.FloorToInt((enemyPosition.z - transform.position.z + avoidanceRadius) / histogramCellSize);
            if (x >= 0 && x < histogramSize && z >= 0 && z < histogramSize)
            {
                histogram[x, z] += (transform.position - enemyPosition).normalized;
            }
        }

        // Calculate the avoidance direction from the histogram
        Vector3 avoidanceDirection = Vector3.zero;
        for (int x = 0; x < histogramSize; x++)
        {
            for (int z = 0; z < histogramSize; z++)
            {
                Vector3 histogramCellPosition = new Vector3(
                    (x + 0.5f) * histogramCellSize - avoidanceRadius,
                    0,
                    (z + 0.5f) * histogramCellSize - avoidanceRadius
                );
                float distanceToCell = Vector3.Distance(transform.position, histogramCellPosition);
                if (distanceToCell < avoidanceRadius)
                {
                    float weight = 1 - distanceToCell / avoidanceRadius;
                    avoidanceDirection += histogram[x, z] * weight;
                }
            }
        }

        // If there are multiple interceptors, throw the quaffle to another chaser on your team
        if (interceptors.Count > 1)
        {
            foreach (PlayerLogicManager teammate in Logic.friends)
            {
                if ((teammate.PlayerType == PlayerType.Chaser && teammate != Logic) || teammate.PlayerType == PlayerType.VRPlayer)
                {
                    Logic.target = teammate;
                    return Vector3.zero;
                }
            }
        }

        return avoidanceDirection.normalized;
    }*/
    /*private Vector3 CalculateAvoidanceDirection()
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
    }*/

    private void chooseLoopToTarget()
    {
        Logic.SetTarget(Logic.ChooseTargetGoal());
    }

    private bool IsTargetALoop()
    {
        List<Targetable> loops = Logic.GetMyLoops() as List<Targetable>;
        return Logic.GetMyLoops().Contains(Logic.target);
    }
}


