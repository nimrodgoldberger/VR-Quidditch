using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLogicManager : Targetable
{
    //protected Targetable startingTransform;// TODO make return to starting transform.
    protected Vector3 startingPosition;
    protected Quaternion startingRotation;
    public DynamicPositionTarget startingPositionTarget;



    public PlayerTeam PlayerTeam;
    public PlayerType PlayerType;
    public PlayerLogicManager[] enemies;
    public PlayerLogicManager[] friends;
    public SnitchLogic Snitch;
    public QuaffleLogicNew Quaffle;
    public BludgerLogic[] Bludgers;
    public Targetable target;
    public bool isMoving = false;
    [SerializeField] protected float speed;
    [SerializeField] protected float rotationSpeed;

    

    protected virtual void Start()
    {
        startingPosition = transform.position;
        startingRotation = transform.rotation;
        startingPositionTarget.SetTransform(startingPosition, startingRotation);
        //startingPositionTarget = new StartingPositionTarget(startingPosition, startingRotation);
        //startingPositionTarget.position = startingPosition;
        //startingPositionTarget.rotation = startingRotation;
    }

    //private void FixedUpdate()
    //{

    //}

    //private void RunPlayerLogicByType()
    //{
    //    switch(PlayerType)
    //    {
    //        case PlayerType.Keeper:
    //            break;
    //        case PlayerType.Beater:
    //            break;
    //        case PlayerType.Chaser:
    //            break;
    //        case PlayerType.Seeker:
    //            break;
    //        case PlayerType.VRPlayer:
    //            break;
    //        default:
    //            break;
    //    }
    //}

    public bool TryCatchQuaffle()
    {   //TODO Add the timer to catch it
        return Quaffle.TryTakeQuaffle(this);
    }

    // Seeker + VRPlayer
    public bool TryCatchSnitch()
    {
        return Snitch.TryCatchSnitch(this);
    }

    public void SetTarget(Targetable newTarget)
    {
        target = newTarget;
    }

    public void ResetTarget()
    {
        target = null;
    }

    public Targetable GetTarget()
    {
        return target;
    }

    //public Transform GetStartingTransform()
    //{
    //    return startingTransform.transform;
    //}

    public Targetable GetStartingTransformAsTargetable()
    {
        //// Create a new GameObject and set its position and rotation to the starting values
        //GameObject startingObj = new GameObject("StartingTransform");
        //startingObj.transform.position = startingPosition;
        //startingObj.transform.rotation = startingRotation;

        //// Add a Targetable component to the new GameObject and return it
        //Targetable targetable = startingObj.AddComponent<Targetable>();
        //return targetable;
        return startingPositionTarget;
        //return new StartingPositionTarget(startingPosition, startingRotation);
    }

    public void MoveAndRotateToTarget()
    {
        if(isMoving)
            return; // Return if already moving
        StartCoroutine(MoveAndRotateCoroutine());
    }

    private IEnumerator MoveAndRotateCoroutine()
    {
        isMoving = true;

        float totalDistance = Vector3.Distance(transform.position, target.transform.position);
        float travelDuration = totalDistance / speed;

        Vector3 initialPosition = transform.position;
        Vector3 targetPosition = target.transform.position;
        Quaternion initialRotation = transform.rotation;
        Quaternion lookRotation = Quaternion.LookRotation(targetPosition - initialPosition);

        float elapsedTime = 0f;
        while(elapsedTime < travelDuration)
        {
            // Calculate the new position and rotation using Mathf.Lerp
            float t = elapsedTime / travelDuration;
            transform.position = Vector3.Lerp(initialPosition, targetPosition, t);
            transform.rotation = Quaternion.Slerp(initialRotation, lookRotation, t);

            // Yielding inside FixedUpdate to wait for the next FixedUpdate step
            yield return new WaitForFixedUpdate();

            elapsedTime += Time.fixedDeltaTime;
        }

        // Ensure the final position and rotation are set correctly
        transform.position = targetPosition;
        transform.rotation = lookRotation;

        // At the end of the coroutine, reset the target and isMoving flag
        ResetTarget();
        isMoving = false;
    }

    public void RotateToStartingPosition()
    {
        if(isMoving)
            return; // Return if already moving

        StartCoroutine(RotateToStartRotationCoroutine());
    }

    //private IEnumerator RotateToStartPositionCoroutine()
    //{
    //    isMoving = true;

    //    Vector3 directionToStartingPos = startingPositionTarget.transform.position - transform.position;
    //    Quaternion desiredRotation = Quaternion.LookRotation(directionToStartingPos, Vector3.up);

    //    float angleToRotation = Quaternion.Angle(transform.rotation, desiredRotation);
    //    float rotationDuration = angleToRotation / rotationSpeed;

    //    float elapsedTime = 0f;
    //    while(elapsedTime < rotationDuration)
    //    {
    //        float t = elapsedTime / rotationDuration;
    //        transform.rotation = Quaternion.Slerp(transform.rotation, desiredRotation, t);

    //        // Yielding inside FixedUpdate to wait for the next FixedUpdate step
    //        yield return new WaitForFixedUpdate();

    //        elapsedTime += Time.fixedDeltaTime;
    //    }

    //    // Ensure the final rotation is set correctly
    //    transform.rotation = desiredRotation;

    //    // At the end of the coroutine, reset the target and isMoving flag
    //    ResetTarget();
    //    isMoving = false;
    //}
    private IEnumerator RotateToStartRotationCoroutine()
    {
        isMoving = true;

        Quaternion initialRotation = transform.rotation;
        Quaternion targetRotation = startingPositionTarget.transform.rotation;

        float angleToRotation = Quaternion.Angle(initialRotation, targetRotation);
        float rotationDuration = angleToRotation / rotationSpeed;

        float elapsedTime = 0f;
        while(elapsedTime < rotationDuration)
        {
            float t = elapsedTime / rotationDuration;
            transform.rotation = Quaternion.Slerp(initialRotation, targetRotation, t);

            // Yielding inside FixedUpdate to wait for the next FixedUpdate step
            yield return new WaitForFixedUpdate();

            elapsedTime += Time.fixedDeltaTime;
        }

        // Ensure the final rotation is set correctly
        transform.rotation = targetRotation;

        // At the end of the coroutine, reset the target and isMoving flag
        ResetTarget();
        isMoving = false;
    }
    //private IEnumerator RotateToStartRotationCoroutine()
    //{
    //    isMoving = true;

    //    Vector3 directionToStartingPos = startingPositionTarget.transform.position - transform.position;
    //    Quaternion desiredRotation = Quaternion.LookRotation(directionToStartingPos, Vector3.up);

    //    float angleToRotation = Quaternion.Angle(transform.rotation, desiredRotation);

    //    float rotationDuration = angleToRotation / rotationSpeed;

    //    float elapsedTime = 0f;
    //    while(elapsedTime < rotationDuration)
    //    {
    //        float t = elapsedTime / rotationDuration;
    //        transform.rotation = Quaternion.Slerp(transform.rotation, startingPositionTarget.transform.rotation, t);

    //        // Yielding inside FixedUpdate to wait for the next FixedUpdate step
    //        yield return new WaitForFixedUpdate();

    //        elapsedTime += Time.fixedDeltaTime;
    //    }

    //    // Ensure the final rotation is set correctly
    //    transform.rotation = startingPositionTarget.transform.rotation;

    //    // At the end of the coroutine, reset the target and isMoving flag
    //    ResetTarget();
    //    isMoving = false;
    //}


    public bool IsSnitchInRange(float range)
    {
        return Vector3.Distance(Snitch.transform.position, transform.position) <= range;
    }

    public bool IsQuaffleInRange(float range)
    {
        //return Quaffle.CanBeTaken(this);
        return Vector3.Distance(Quaffle.transform.position, transform.position) <= range;
    }
}

