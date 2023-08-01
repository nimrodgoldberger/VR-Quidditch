using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLogicManager : MonoBehaviour
{
    private Targetable startingTransform;// TODO make return to starting transform.
    public PlayerTeam PlayerTeam;
    public PlayerType PlayerType;
    public PlayerLogicManager[] enemies;
    public PlayerLogicManager[] friends;
    public SnitchLogic Snitch;
    public QuaffleLogicNew Quaffle;
    public BludgerLogic[] Bludgers;
    public Targetable target;
    [SerializeField] private float speed;
    [SerializeField] private float rotationSpeed;

    private void Start()
    {
        startingTransform.transform.position = transform.position;
        startingTransform.transform.rotation = transform.rotation;
    }

    private void FixedUpdate()
    {

    }

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

    public Transform GetStartingTransform()
    {
        return startingTransform.transform;
    }

    public Targetable GetStartingTransformAsTargetable()
    {
        return startingTransform;
    }

    //private IEnumerator MoveAndRotateToTarget()
    public void MoveAndRotateToTarget()
    {
        float totalDistance = Vector3.Distance(transform.position, target.transform.position);
        float travelDuration = totalDistance / speed;

        float elapsedTime = 0f;
        Vector3 initialPosition = transform.position;
        Vector3 targetPosition = target.transform.position;
        Quaternion initialRotation = transform.rotation;
        Quaternion lookRotation = Quaternion.LookRotation(targetPosition - initialPosition);

        while(elapsedTime < travelDuration)
        {
            // Calculate the new position and rotation using Mathf.Lerp
            float t = elapsedTime / travelDuration;
            transform.position = Vector3.Lerp(initialPosition, targetPosition, t);
            transform.rotation = Quaternion.Slerp(initialRotation, lookRotation, t);

            elapsedTime += Time.deltaTime;

            //yield return null; // Wait for the next frame
        }
    }

    public bool IsSnitchInRange(float range)
    {
        return Vector3.Distance(Snitch.transform.position, transform.position) <= range;
    }

    public bool IsQuaffleInRange(float range)
    {
        return Vector3.Distance(Quaffle.transform.position, transform.position) <= range;
    }
}

