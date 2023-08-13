using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLogicManager : Targetable
{
    private Animator animator;

    public Vector3 startingPosition;
    protected Quaternion startingRotation;
    public DynamicPositionTarget startingPositionTarget;
    public PlayerTeam PlayerTeam;
    public PlayerType PlayerType;

    [SerializeField] private List<ScoreArea> myTeamGoals;
    [SerializeField] private List<ScoreArea> enemyTeamGoals;

    public PlayerLogicManager[] enemies;
    public PlayerLogicManager[] friends;
    public SnitchLogic Snitch;
    public QuaffleLogic Quaffle;
    public float quaffleTakeTime = 0f;
    public BludgerLogic[] Bludgers;
    public Targetable target;
    public Targetable relativePositionTarget;
    public bool isMoving = false;
    public bool isRotatingToStartingPos = false;

    protected float startingSpeed;
    [SerializeField] protected float speed;
    [SerializeField] protected float rotationSpeed;

    protected virtual void Start()
    {
        startingSpeed = speed;
        startingPosition = transform.position;
        startingRotation = transform.rotation;
        startingPositionTarget.SetTransform(startingPosition, startingRotation);
        animator = GetComponent<Animator>();
    }

    public virtual bool TryCatchQuaffle()
    {   //TODO Add the timer to catch it
        return Quaffle.TryTakeQuaffle(this);
    }

    // Seeker + VRPlayer
    public virtual bool TryCatchSnitch()
    {
        return Snitch.TryCatchSnitch(this);
    }

    public virtual void SetTarget(Targetable newTarget)
    {
        target = newTarget;
    }

    public virtual void ResetTarget()
    {
        target = null;
    }

    //public virtual Targetable GetTarget()
    //{
    //    return target;
    //}

    //public virtual float GetSpeed()
    //{
    //    return speed;
    //}

    public virtual void SetRotationSpeed(float newRotationSpeed)
    {
        rotationSpeed = newRotationSpeed;
    }

    public virtual Targetable GetStartingTransformAsTargetable()
    {
        return startingPositionTarget;
    }

    public virtual void StopMoveAndRotateToTarget() // TODO Check if works
    {
        StopCoroutine(MoveAndRotateCoroutine());
    }

    public virtual void MoveAndRotateToTarget()
    {
        if (isMoving)
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
        while (elapsedTime < travelDuration)
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


    //public virtual IEnumerator MoveAndRotateToBludger(int bludgerIndex, Vector3 relativePos)
    //{
    //    isMoving = true;
    //    float totalDistance = Vector3.Distance(transform.position, Bludgers[bludgerIndex].transform.position);
    //    float travelDuration = totalDistance / speed;
    //    Vector3 initialPosition = transform.position;
    //    Vector3 bludgerPosition = Bludgers[bludgerIndex].transform.position;
    //    Quaternion initialRotation = transform.rotation;
    //    Quaternion lookRotation = Quaternion.LookRotation(bludgerPosition - initialPosition);

    //    float elapsedTime = 0f;
    //    while (elapsedTime < travelDuration)
    //    {
    //        // Calculate the new position and rotation using Mathf.Lerp
    //        float t = elapsedTime / travelDuration;
    //        transform.position = Vector3.Lerp(initialPosition, bludgerPosition, t);
    //        transform.rotation = Quaternion.Slerp(initialRotation, lookRotation, t);

    //        // Yielding inside FixedUpdate to wait for the next FixedUpdate step
    //        yield return new WaitForFixedUpdate();

    //        elapsedTime += Time.fixedDeltaTime;
    //    }

    //    // Ensure the final position and rotation are set correctly
    //    transform.position = bludgerPosition + relativePos;
    //    transform.rotation = lookRotation;

    //    // At the end of the coroutine, reset the target and isMoving flag
    //    isMoving = false;
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
    public virtual void RotateToStartingPosition()
    {
        if (isMoving || isRotatingToStartingPos)
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
        while (elapsedTime < rotationDuration)
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

    public virtual bool IsSnitchInRange(float range)
    {
        return Vector3.Distance(Snitch.transform.position, transform.position) <= range;
    }

    public virtual bool IsQuaffleInRange(float range)
    {
        //return Quaffle.CanBeTaken(this);
        return Vector3.Distance(Quaffle.transform.position, transform.position) <= range;
    }

    public virtual bool IsQuaffleCloseToMyTeamGoals(float range)
    {
        bool isClose = false;

        foreach (ScoreArea goal in myTeamGoals)
        {
            if (Vector3.Distance(Quaffle.transform.position, goal.transform.position) <= range)
            {
                isClose = true;
                break;
            }
        }

        return isClose;
    }

    public virtual bool IsQuaffleHeldByMyTeam()
    {
        return Quaffle.IsQuaffleHeldByTeam(PlayerTeam);
    }

    public virtual bool IsQuaffleHeldByMe()
    {
        return Quaffle.IsQuaffleHeldByPlayer(this);
    }

    public virtual void CaughtQuaffle()
    {
        speed = speed * 2 / 3;
    }

    public virtual void ResetSpeed()
    {
        speed = startingSpeed;
    }

    public virtual int IsABludgerInRange(float range)
    {
        if (Vector3.Distance(Bludgers[0].transform.position, transform.position) <= range)
            return 0;
        else if (Vector3.Distance(Bludgers[1].transform.position, transform.position) <= range)
            return 1;
        else
            return -1;//NO BLUDGERS IN RANGE

    }

    public virtual void BudgerWasHit(int bludgerIndex)
    {
        // Create a new instance of Random class
        System.Random random = new System.Random();
        // Generate a random index within the bounds of the array
        int randomIndex = random.Next(0, enemies.Length - 1);
        // New bludger target will be the new enemy
        Bludgers[bludgerIndex].GoToChaseAfterBeingHit(enemies[randomIndex].gameObject);
    }

    public virtual void SetGoals(List<ScoreArea> myGoals, List<ScoreArea> enemyGoals)
    {
        myTeamGoals = myGoals;
        enemyTeamGoals = enemyGoals;
    }

    public virtual ScoreArea ChooseTargetGoal()
    {
        int minObstructions = int.MaxValue;
        ScoreArea selectedGoal = null;

        foreach (ScoreArea goal in enemyTeamGoals)
        {
            int obstructions = CountObstructions(goal.transform.position);

            if (obstructions < minObstructions)
            {
                minObstructions = obstructions;
                selectedGoal = goal;
            }
        }

        return selectedGoal;
    }

    private int CountObstructions(Vector3 targetPosition)
    {
        int obstructions = 0;

        foreach (PlayerLogicManager enemy in enemies)
        {
            if (IsObstructed(enemy.transform.position, targetPosition))
            {
                obstructions++;
            }
        }

        return obstructions;
    }

    private bool IsObstructed(Vector3 startPosition, Vector3 targetPosition)
    {
        Vector3 direction = targetPosition - startPosition;
        RaycastHit[] hits = Physics.RaycastAll(startPosition, direction, direction.magnitude);

        foreach (RaycastHit hit in hits)
        {
            if (hit.collider != null && hit.collider.gameObject != gameObject)
            {
                return true; // There's an obstruction
            }
        }

        return false; // No obstructions found
    }


    //public void SetStartingPosition(Vector3 startPos)
    //{
    //    startingPosition = startPos;
    //}

    public virtual Vector3 CreateRelativePositionToBewareOfBludgers()
    {
        int negative = 0;
        int positive = 1;

        Vector3 relativePosition = Vector3.one;

        // Creates a new instance of Random class
        System.Random random = new System.Random();

        // Generates a random index within the bounds of the array
        int value = random.Next(0, 1);

        if (value == negative)
        {
            relativePosition.x = random.Next(-40, -35);
            relativePosition.y = random.Next(-40, -35);
            relativePosition.z = random.Next(-40, -35);
        }

        if (value == positive)
        {
            relativePosition.x = random.Next(-40, -35);
            relativePosition.y = random.Next(-40, -35);
            relativePosition.z = random.Next(-40, -35);
        }
        return relativePosition;
    }

    public virtual Animator GetAnimator()
    {
        return animator;
    }
}

