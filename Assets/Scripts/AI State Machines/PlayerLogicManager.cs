using System.Collections;
using UnityEngine;

public class PlayerLogicManager : Targetable
{
    protected Vector3 startingPosition;
    protected Quaternion startingRotation;
    public DynamicPositionTarget startingPositionTarget;
    public PlayerTeam PlayerTeam;
    public PlayerType PlayerType;

    public PlayerLogicManager[] enemies;
    public PlayerLogicManager[] friends;
    public SnitchLogic Snitch;
    public QuaffleLogicNew Quaffle;
    public float quaffleTakeTime = 0f;
    public BludgerLogic[] Bludgers;
    public Targetable target;
    public bool isMoving = false;
    public bool isRotatingToStartingPos = false;

    [SerializeField] protected float speed;
    [SerializeField] protected float rotationSpeed;

    protected virtual void Start()
    {
        startingPosition = transform.position;
        startingRotation = transform.rotation;
        startingPositionTarget.SetTransform(startingPosition, startingRotation);
    }

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

    public void SetRotationSpeed(float newRotationSpeed)
    {
        rotationSpeed = newRotationSpeed;
    }

    public Targetable GetStartingTransformAsTargetable()
    {
        return startingPositionTarget;
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

    //public void RotateToStartingPosition()
    //{
    //    if(isMoving)
    //        return; // Return if already moving

    //    StartCoroutine(RotateToStartRotationCoroutine());
    //}

    //private IEnumerator RotateToStartRotationCoroutine()
    //{
    //    isMoving = true; // Set the isMoving flag to true before starting the rotation

    //    Quaternion initialRotation = transform.rotation;
    //    Quaternion targetRotation = startingPositionTarget.transform.rotation;
    //    float angleToRotation = Quaternion.Angle(initialRotation, targetRotation);
    //    float rotationDuration = angleToRotation / rotationSpeed;
    //    float elapsedTime = 0f;
    //    while(elapsedTime < rotationDuration)
    //    {
    //        float t = elapsedTime / rotationDuration;
    //        transform.rotation = Quaternion.Slerp(initialRotation, targetRotation, t);

    //        // Yielding inside FixedUpdate to wait for the next FixedUpdate step
    //        yield return new WaitForFixedUpdate();

    //        elapsedTime += Time.fixedDeltaTime;
    //    }

    //    // Ensure the final rotation is set correctly
    //    transform.rotation = targetRotation;
    //    // At the end of the coroutine, reset the target and isMoving flag
    //    ResetTarget();
    //    isMoving = false; // Set the isMoving flag to false after the rotation is complete
    //}

    //public void RotateToStartingPosition()
    //{
    //    if(isMoving)
    //        return; // Return if already moving

    //    StartCoroutine(RotateToStartRotationCoroutine());
    //}

    //private IEnumerator RotateToStartRotationCoroutine()
    //{
    //    isMoving = true; // Set the isMoving flag to true before starting the rotation

    //    Quaternion initialRotation = transform.rotation;
    //    Quaternion targetRotation = startingPositionTarget.transform.rotation;
    //    float angleToRotation = Quaternion.Angle(initialRotation, targetRotation);
    //    float rotationDuration = angleToRotation / rotationSpeed;
    //    float elapsedTime = 0f;
    //    while(elapsedTime < rotationDuration)
    //    {
    //        float t = elapsedTime / rotationDuration;
    //        transform.rotation = Quaternion.Slerp(initialRotation, targetRotation, t);

    //        // Yielding inside FixedUpdate to wait for the next FixedUpdate step
    //        yield return new WaitForFixedUpdate();

    //        elapsedTime += Time.fixedDeltaTime;
    //    }

    //    // Ensure the final rotation is set correctly
    //    transform.rotation = targetRotation;

    //    // At the end of the coroutine, reset the target and isMoving flag
    //    ResetTarget();
    //    isMoving = false; // Set the isMoving flag to false after the rotation is complete

    //    // After the rotation is complete, also reset the isRotatingToStartingPos flag
    //    isRotatingToStartingPos = false;
    //}
    public void RotateToStartingPosition()
    {
        if(isMoving || isRotatingToStartingPos)
            return; // Return if already moving or rotating

        StartCoroutine(RotateToStartRotationCoroutine());
    }

    private IEnumerator RotateToStartRotationCoroutine()
    {
        isMoving = true; // Set the isMoving flag to true before starting the rotation
        isRotatingToStartingPos = true; // Set the isRotatingToStartingPos flag to true before starting the rotation

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
        isMoving = false; // Set the isMoving flag to false after the rotation is complete

        // After the rotation is complete, also reset the isRotatingToStartingPos flag
        isRotatingToStartingPos = false;
    }



    public bool IsSnitchInRange(float range)
    {
        return Vector3.Distance(Snitch.transform.position, transform.position) <= range;
    }

    public bool IsQuaffleInRange(float range)
    {
        //return Quaffle.CanBeTaken(this);
        return Vector3.Distance(Quaffle.transform.position, transform.position) <= range;
    }

    public int IsABludgerInRange(float range)
    {
        if (Vector3.Distance(Bludgers[0].transform.position, transform.position) <= range)
            return 0;
        else if (Vector3.Distance(Bludgers[1].transform.position, transform.position) <= range)
            return 1;
        else
            return 3;//NO BLUDGERS IN RANGE

    }

    public void BudgerWasHit(int bludgerIndex)
    {
        // Create a new instance of Random class
        System.Random random = new System.Random();
        // Generate a random index within the bounds of the array
        int randomIndex = random.Next(0, enemies.Length);
        // New bludger target will be the new enemy
        Bludgers[bludgerIndex].SetTarget(enemies[randomIndex].gameObject);
        Bludgers[bludgerIndex].state = BludgerLogic.State.Chase;
    }

}

